using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.UserService.Models;
using Bank.UserService.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Role = Bank.UserService.Configurations.Configuration.Policy.Role;

namespace Bank.UserService.Controllers;

[ApiController]
public class LoanController : ControllerBase
{
    private readonly ILoanRepository _loanRepository;

    public LoanController(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    [Authorize]
    [HttpGet(Endpoints.Loan.GetAll)]
    public async Task<ActionResult<Page<Loan>>> GetAll([FromQuery] LoanFilterQuery loanFilterQuery, [FromQuery] Pageable pageable)
    {
        try
        {
            var loans = await _loanRepository.FindAll(loanFilterQuery, pageable);
            return Ok(loans);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [Authorize]
    [HttpGet(Endpoints.Loan.GetOne)]
    public async Task<ActionResult<Loan>> GetOne([FromRoute] Guid id)
    {
        try
        {
            var loan = await _loanRepository.FindById(id);

            if (loan == null)
                return NotFound($"Loan with ID {id} not found");

            return Ok(loan);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [Authorize]
    [HttpGet(Endpoints.Loan.GetByAccount)]
    public async Task<ActionResult<Page<Loan>>> GetByAccount([FromRoute] Guid accountId, [FromQuery] Pageable pageable)
    {
        try
        {
            var loanFilterQuery = new LoanFilterQuery { AccountNumber = accountId.ToString() };
            var loans           = await _loanRepository.FindAll(loanFilterQuery, pageable);
            return Ok(loans);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost(Endpoints.Loan.Create)]
    [Authorize(Roles = $"{Role.Admin}, {Role.Employee}")]
    public async Task<ActionResult<Loan>> Create([FromBody] Loan loan)
    {
        try
        {
            loan.Id         = Guid.NewGuid();
            loan.CreatedAt  = DateTime.UtcNow;
            loan.ModifiedAt = DateTime.UtcNow;

            var createdLoan = await _loanRepository.Add(loan);
            return CreatedAtAction(nameof(GetOne), new { id = createdLoan.Id }, createdLoan);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut(Endpoints.Loan.Update)]
    [Authorize(Roles = $"{Role.Admin}, {Role.Employee}")]
    public async Task<ActionResult<Loan>> Update([FromBody] Loan loan, [FromRoute] Guid id)
    {
        try
        {
            var existingLoan = await _loanRepository.FindById(id);

            if (existingLoan == null)
                return NotFound($"Loan with ID {id} not found");

            loan.ModifiedAt = DateTime.UtcNow;

            var updatedLoan = await _loanRepository.Update(existingLoan, loan);
            return Ok(updatedLoan);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
