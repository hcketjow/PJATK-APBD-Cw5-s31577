using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PreparationForExam.DTOs;

namespace PreparationForExam.Service;

public interface IPatientService
{
    Task<IEnumerable<PatientResponse>> GetAllAsync(string? serach, CancellationToken cancellationToken);
    // Task<int> AssignBedAsync(string pesel, CreateBedAssignment request, CancellationToken cancellationToken);
}
