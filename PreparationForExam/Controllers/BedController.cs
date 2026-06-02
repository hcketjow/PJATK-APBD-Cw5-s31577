using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PreparationForExam.Service;

namespace PreparationForExam.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BedController(IBedService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> getAll([FromQuery] string? serach, CancellationToken cancellationToken)
    {
        return Ok(await service.GetAllAsync(serach, cancellationToken));
    }
}
