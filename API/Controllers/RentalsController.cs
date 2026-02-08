using Microsoft.AspNetCore.Mvc;
using Domain.Services;
using Domain.Models.DTOs;

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
    public ActionResult<RegisterRentalResponse> RegisterRental([FromBody] RegisterRentalRequest request)
    {
        try
        {
            var response = _rentalService.RegisterRental(
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
    public ActionResult<RegisterRentalResponse> RegisterReturn([FromBody] RegisterReturnRequest request)
    {
        try
        {
            var response = _rentalService.RegisterDropoff(
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

public record RegisterRentalRequest(
    string BookingNumber,
    string RegNumber,
    string Ssno
);
public record RegisterReturnRequest(
    string BookingNumber,
    int FinalMileage
);
