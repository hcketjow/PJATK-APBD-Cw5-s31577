using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PreparationForExam.DTOs;
using PreparationForExam.Exceptions;
using PreparationForExam.Service;

namespace PreparationForExam.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController(IPatientService service) : ControllerBase {
    [HttpGet]
    public async Task<IActionResult> getAll([FromQuery] string? search, CancellationToken cancellationToken)
    {
        return Ok(await service.GetAllAsync(search, cancellationToken));
    }

    // [HttpPost("{pesel}/bedassignments")]
    // public async Task<IActionResult> AssignBed(string pesel, [FromBody] CreateBedAssignment request, CancellationToken cancellationToken)
    // {
    //     try
    //     {
    //         var id = await service.AssignBedAsync(pesel, request, cancellationToken);
    //         return Created($"/api/patients/{pesel}/bedassignments/{id}", new { id });
    //     }
    //     catch (NotFoundException e)
    //     {
    //         return NotFound(e.Message);
    //     }
    // }
}
