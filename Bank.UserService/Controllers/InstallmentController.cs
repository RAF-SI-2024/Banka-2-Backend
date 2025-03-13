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
public class InstallmentController : ControllerBase
{
    private readonly IInstallmentRepository _installmentRepository;
    private readonly ILoanRepository        _loanRepository;

    public InstallmentController(IInstallmentRepository installmentRepository, ILoanRepository loanRepository)
    {
        _installmentRepository = installmentRepository;
        _loanRepository        = loanRepository;
    }

    [Authorize]
    [HttpGet(Endpoints.Installment.GetAll)]
    public async Task<ActionResult<Page<Installment>>> GetAll([FromQuery] Pageable pageable)
    {
        try
        {
            var installments = await _installmentRepository.FindAllByLoanId(Guid.Empty, pageable);
            return Ok(installments);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [Authorize]
    [HttpGet(Endpoints.Installment.GetOne)]
    public async Task<ActionResult<Installment>> GetOne([FromRoute] Guid id)
    {
        try
        {
            var installment = await _installmentRepository.FindById(id);

            if (installment == null)
                return NotFound($"Installment with ID {id} not found");

            return Ok(installment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost(Endpoints.Installment.Create)]
    [Authorize(Roles = $"{Role.Admin}, {Role.Employee}")]
    public async Task<ActionResult<Installment>> Create([FromBody] Installment installment)
    {
        try
        {
            var loan = await _loanRepository.FindById(installment.LoanId);

            if (loan == null)
                return BadRequest($"Loan with ID {installment.LoanId} not found");

            installment.Id         = Guid.NewGuid();
            installment.CreatedAt  = DateTime.UtcNow;
            installment.ModifiedAt = DateTime.UtcNow;

            var createdInstallment = await _installmentRepository.Add(installment);
            return CreatedAtAction(nameof(GetOne), new { id = createdInstallment.Id }, createdInstallment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut(Endpoints.Installment.Update)]
    [Authorize(Roles = $"{Role.Admin}, {Role.Employee}")]
    public async Task<ActionResult<Installment>> Update([FromBody] Installment installment, [FromRoute] Guid id)
    {
        try
        {
            var existingInstallment = await _installmentRepository.FindById(id);

            if (existingInstallment == null)
                return NotFound($"Installment with ID {id} not found");

            installment.ModifiedAt = DateTime.UtcNow;

            var updatedInstallment = await _installmentRepository.Update(existingInstallment, installment);
            return Ok(updatedInstallment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut(Endpoints.Installment.UpdateStatus)]
    [Authorize(Roles = $"{Role.Admin}, {Role.Employee}")]
    public async Task<ActionResult<Installment>> UpdateStatus([FromBody] InstallmentStatus status, [FromRoute] Guid id)
    {
        try
        {
            var existingInstallment = await _installmentRepository.FindById(id);

            if (existingInstallment == null)
                return NotFound($"Installment with ID {id} not found");

            existingInstallment.Status     = status;
            existingInstallment.ModifiedAt = DateTime.UtcNow;

            var updatedInstallment = await _installmentRepository.Update(existingInstallment, existingInstallment);
            return Ok(updatedInstallment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
