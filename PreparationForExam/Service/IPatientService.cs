using PreparationForExam.DTOs;

namespace PreparationForExam.Service;

public interface IPatientService
{
    Task<List<PatientResponse>> GetAllAsync(CancellationToken cancellationToken);
    Task<PatientResponse?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<PatientResponse> CreateAsync(BookRequest request, CancellationToken cancellationToken);
    Task<PatientResponse?> UpdateAsync(int id, Patien request, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}
