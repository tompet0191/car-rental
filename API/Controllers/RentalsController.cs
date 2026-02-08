using API.Requests;
using Microsoft.AspNetCore.Mvc;
using Domain.Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentalsController : ControllerBase
{
    private readonly RentalService _rentalService;

    public RentalsController(RentalService rentalService)
    {
        _rentalService = rentalService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterRental([FromBody] RegisterRentalRequest request)
    {
        try
        {
            var response = await _rentalService.RegisterRental(
                request.BookingNumber,
                request.RegNumber,
                request.Ssno);

            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("return")]
    public async Task<IActionResult> RegisterReturn([FromBody] RegisterReturnRequest request)
    {
        try
        {
            var response = await _rentalService.RegisterDropoff(
                request.BookingNumber,
                request.FinalMileage
            );

            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
