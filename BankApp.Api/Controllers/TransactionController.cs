using BankAccountCore;
using BankApp.Api.DTOs;
using BankApp.Api.Exceptions;
using BankApp.Api.Interfaces;
using BankApp.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/BankAccount/{bankAccountId}/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(int bankAccountId, [FromBody] CreateTransactionDto dto)
        {
            TransactionDto result;
            if (dto.TransactionType == TransactionType.Deposit)
            {
                try
                {
                    result = await _transactionService.DepositAsync(dto.Amount, bankAccountId);
                }
                catch (ArgumentException ex)
                {
                    return NotFound(ex.Message);
                }
            }

            else
            {
                try
                {
                    result = await _transactionService.WithdrawalAsync(dto.Amount, bankAccountId);
                }
                catch (InsufficientFundsException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return CreatedAtAction(nameof(GetAllByAccountId), new { bankAccountId = bankAccountId }, result);
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer(int bankAccountId, [FromBody] TransferDto dto)
        {
            TransactionDto result;
            try
            {
                result = await _transactionService.TransferAsync(dto.Amount, bankAccountId, dto.RecipientAccountNumber);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InsufficientFundsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (TransferToOneselfException ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction(nameof(GetAllByAccountId), new { bankAccountId = bankAccountId }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByAccountId(int bankAccountId)
        {
            var getAll = await _transactionService.GetAllByAccountIdAsync(bankAccountId);
            return Ok(getAll);
        }
    }
}

