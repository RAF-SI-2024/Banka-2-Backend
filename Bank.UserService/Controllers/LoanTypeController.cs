using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.UserService.Configurations;
using Bank.UserService.Models;
using Bank.UserService.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.UserService.Controllers;

using Role = Configuration.Policy.Role;

[ApiController]
public class LoanTypeController : ControllerBase
{
    private readonly ILoanTypeRepository _loanTypeRepository;

    public LoanTypeController(ILoanTypeRepository loanTypeRepository)
    {
        _loanTypeRepository = loanTypeRepository;
    }

    [Authorize]
    [HttpGet(Endpoints.LoanType.GetAll)]
    public async Task<ActionResult<Page<LoanType>>> GetAll([FromQuery] Pageable pageable)
    {
        try
        {
            var loanTypes = await _loanTypeRepository.FindAll(pageable);
            return Ok(loanTypes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [Authorize]
    [HttpGet(Endpoints.LoanType.GetOne)]
    public async Task<ActionResult<LoanType>> GetOne([FromRoute] Guid id)
    {
        try
        {
            var loanType = await _loanTypeRepository.FindById(id);

            if (loanType == null)
                return NotFound($"Loan type with ID {id} not found");

            return Ok(loanType);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost(Endpoints.LoanType.Create)]
    [Authorize(Roles = $"{Role.Admin}")]
    public async Task<ActionResult<LoanType>> Create([FromBody] LoanType loanType)
    {
        try
        {
            loanType.Id         = Guid.NewGuid();
            loanType.CreatedAt  = DateTime.UtcNow;
            loanType.ModifiedAt = DateTime.UtcNow;

            var createdLoanType = await _loanTypeRepository.Add(loanType);
            return CreatedAtAction(nameof(GetOne), new { id = createdLoanType.Id }, createdLoanType);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut(Endpoints.LoanType.Update)]
    [Authorize(Roles = $"{Role.Admin}")]
    public async Task<ActionResult<LoanType>> Update([FromBody] LoanType loanType, [FromRoute] Guid id)
    {
        try
        {
            var existingLoanType = await _loanTypeRepository.FindById(id);

            if (existingLoanType == null)
                return NotFound($"Loan type with ID {id} not found");

            loanType.ModifiedAt = DateTime.UtcNow;

            var updatedLoanType = await _loanTypeRepository.Update(existingLoanType, loanType);
            return Ok(updatedLoanType);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
