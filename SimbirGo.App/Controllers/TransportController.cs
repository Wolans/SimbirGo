using Microsoft.AspNetCore.Mvc;

using SimbirGo.Contracts.Commands.SimbirGoCommands;
using SimbirGo.Contracts.Interfaces;
using SimbirGo.Contracts.DTO;

namespace SimbirGo.App.Controllers;

[ApiController]
[Route("api/transports")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ActionResult))]
public class TransportController : ControllerBase
{
    private readonly ITransportService _transportService;

    public TransportController(ITransportService transportService)
    {
        _transportService = transportService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransport([FromBody] TransportCreateCommand transport)
    {
        string transportID = Ulid.NewUlid().ToString();

        try
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;

            await _transportService.CreateTransport(transportID, transport, cancellationToken);

            return CreatedAtAction(nameof(GetTransportById), new { transportID }, null);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{transportID}")]
    public async Task<ActionResult<TransportDTO>> GetTransportById(string transportID)
    {
        try
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;

            var transport = await _transportService.GetTransportById(transportID, cancellationToken);

            return Ok(transport);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<TransportDTO>>> GetAllTransports()
    {
        var cancellationToken = HttpContext?.RequestAborted ?? default;

        var transports = await _transportService.GetAllTransports(cancellationToken);

        return Ok(transports);
    }

    [HttpPut("{transportID}")]
    public async Task<IActionResult> UpdateTransport(string transportID, [FromBody] TransportUpdateCommand transport)
    {
        try
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;

            await _transportService.UpdateTransport(transportID, transport, cancellationToken);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{transportID}")]
    public async Task<IActionResult> DeleteTransport(string transportID)
    {
        try
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;

            await _transportService.DeleteTransport(transportID, cancellationToken);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }
}