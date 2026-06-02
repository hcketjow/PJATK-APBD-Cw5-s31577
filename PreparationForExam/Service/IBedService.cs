using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PreparationForExam.DTOs;

namespace PreparationForExam.Service;

public interface IBedService
{
    Task<List<BedResponse>> GetAllAsync(string? serach, CancellationToken cancellationToken);
    Task<BedResponse?> CreateAsync(BedRequest request);
}
