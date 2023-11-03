using Microsoft.AspNetCore.Mvc;

using SimbirGo.Contracts.Commands.SimbirGoCommands;
using SimbirGo.Contracts.Interfaces;
using SimbirGo.Contracts.DTO;

namespace SimbirGo.App.Controllers;

[ApiController]
[Route("api/rentals")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ActionResult))]
public class RentalController : ControllerBase
{
    private readonly IRentalService _rentalService;

    public RentalController(IRentalService rentalService)
    {
        _rentalService = rentalService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRental([FromBody] RentalCreateCommand rental)
    {
        string rentalID = Ulid.NewUlid().ToString();

        try
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;

            await _rentalService.CreateRental(rentalID, rental, cancellationToken);

            return CreatedAtAction(nameof(GetRentalById), new { rentalID }, null);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{rentalID}")]
    public async Task<ActionResult<RentalDTO>> GetRentalById(string rentalID)
    {
        try
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;

            var rental = await _rentalService.GetRentalById(rentalID, cancellationToken);

            return Ok(rental);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<RentalDTO>>> GetAllRentals()
    {
        var cancellationToken = HttpContext?.RequestAborted ?? default;

        var rentals = await _rentalService.GetAllRentals(cancellationToken);

        return Ok(rentals);
    }

    [HttpPut("{rentalID}")]
    public async Task<IActionResult> UpdateRental(string rentalID, [FromBody] RentalUpdateCommand rental)
    {
        try
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;

            await _rentalService.UpdateRental(rentalID, rental, cancellationToken);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{rentalID}")]
    public async Task<IActionResult> DeleteRental(string rentalID)
    {
        try
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;

            await _rentalService.DeleteRental(rentalID, cancellationToken);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }
}