using Microsoft.AspNetCore.Mvc;

using SimbirGo.Contracts.Commands.SimbirGoCommands;
using SimbirGo.Contracts.Interfaces;
using SimbirGo.Contracts.DTO;

namespace SimbirGo.App.Controllers;

[ApiController]
[Route("api/payments")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ActionResult))]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentCreateCommand payment)
    {
        string paymentID = Ulid.NewUlid().ToString();

        try
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;

            await _paymentService.CreatePayment(paymentID, payment, cancellationToken);

            return CreatedAtAction(nameof(GetPaymentById), new { paymentID }, null);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{paymentID}")]
    public async Task<ActionResult<PaymentDTO>> GetPaymentById(string paymentID)
    {
        try
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;
            
            var payment = await _paymentService.GetPaymentById(paymentID, cancellationToken);

            return Ok(payment);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<PaymentDTO>>> GetAllPayments()
    {
        var cancellationToken = HttpContext?.RequestAborted ?? default;
        
        var payments = await _paymentService.GetAllPayments(cancellationToken);

        return Ok(payments);
    }

    [HttpPut("{paymentID}")]
    public async Task<IActionResult> UpdatePayment(string paymentID, [FromBody] PaymentUpdateCommand payment)
    {
        try
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;
        
            await _paymentService.UpdatePayment(paymentID, payment, cancellationToken);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{paymentID}")]
    public async Task<IActionResult> DeletePayment(string paymentID)
    {
        try
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;

            await _paymentService.DeletePayment(paymentID, cancellationToken);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }
}