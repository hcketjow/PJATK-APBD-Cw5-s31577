using Microsoft.EntityFrameworkCore;
using PreparationForExam.DTOs;
using PreparationForExam.Infrastructre;
using PreparationForExam.Models;

namespace PreparationForExam.Service;

public class PatientService(ApbdContext context) : IPatientService
{
    public async Task<List<PatientResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Patients.Select(patient => new PatientResponse(
            patient.Pesel,
            patient.FirstName,
            patient.LastName,
            patient.Age,
        patient.Sex ? "male" : "female",
            patient.Admissions.Select(admission => new AdmissionResponse(
                admission.Id,
                admission.AdmissionDate,
                admission.DischargeDate,
                new WardResponse(admission.Ward.Id, admission.Ward.Name, admission.Ward.Description),
                admission.PatientPesel
            )),
            patient.BedAssignments.Select(bedAssign => new BedAssignmentResponse(
                bedAssign.Id,
                bedAssign.PatientPesel,
                bedAssign.From,
                bedAssign.To,
                new BedResponse(
                    bedAssign.Bed.Id,
                    new RoomResponse(
                        bedAssign.Bed.Room.Id,
                        bedAssign.Bed.Room.HasTv,
                        new WardResponse(
                            bedAssign.Bed.Room.Ward.Id,
                            bedAssign.Bed.Room.Ward.Name,
                            bedAssign.Bed.Room.Ward.Description)
                    ),
                    new BedTypeResponse(
                        bedAssign.Bed.BedType.Id,
                        bedAssign.Bed.BedType.Name,
                        bedAssign.Bed.BedType.Description
                    )
                )
            )).ToList()
        )).ToListAsync(cancellationToken);
    }

    public async Task<PatientResponse?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var patient = await context.Patients
            .Include(patient => patient.BedAssignments)
            .Include(patient => patient.Admissions)
            .FirstOrDefaultAsync(cancellationToken);
        if (patient is null)
            return null;
        return new PatientResponse(
            patient.Pesel,
            patient.FirstName,
            patient.LastName,
            patient.Age,
            patient.Sex ? "male" : "female",
            patient.Admissions.Select(admission => new AdmissionResponse(
                admission.Id,
                admission.AdmissionDate,
                admission.DischargeDate,
                new WardResponse(admission.Ward.Id, admission.Ward.Name, admission.Ward.Description),
                admission.PatientPesel
            )),
            patient.BedAssignments.Select(bedAssign => new BedAssignmentResponse(
                bedAssign.Id,
                bedAssign.PatientPesel,
                bedAssign.From,
                bedAssign.To,
                new BedResponse(
                    bedAssign.Bed.Id,
                    new RoomResponse(
                        bedAssign.Bed.Room.Id,
                        bedAssign.Bed.Room.HasTv,
                        new WardResponse(
                            bedAssign.Bed.Room.Ward.Id,
                            bedAssign.Bed.Room.Ward.Name,
                            bedAssign.Bed.Room.Ward.Description)
                    ),
                    new BedTypeResponse(
                        bedAssign.Bed.BedType.Id,
                        bedAssign.Bed.BedType.Name,
                        bedAssign.Bed.BedType.Description
                    )
                )
            )
        ));
    }

    public async Task<PatientResponse> CreateAsync(PatientRequest request, CancellationToken cancellationToken)
    {
        var patient = new Patient
        {
            Pesel = request.Pesel,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Age = request.Age,
            Sex = false
        };
        context.Patients.Add(patient);
        await context.SaveChangesAsync(cancellationToken);
        return new PatientResponse(
        patient.Pesel,
        patient.FirstName,
        patient.LastName,
        patient.Age,
        patient.Sex ? "male" : "female",
        patient.Admissions.Select(admission => new AdmissionResponse(
            admission.Id,
            admission.AdmissionDate,
            admission.DischargeDate,
            new WardResponse(admission.Ward.Id, admission.Ward.Name, admission.Ward.Description),
            admission.PatientPesel
        )),
        patient.BedAssignments.Select(bedAssign => new BedAssignmentResponse(
                bedAssign.Id,
                bedAssign.PatientPesel,
                bedAssign.From,
                bedAssign.To,
                new BedResponse(
                    bedAssign.Bed.Id,
                    new RoomResponse(
                        bedAssign.Bed.Room.Id,
                        bedAssign.Bed.Room.HasTv,
                        new WardResponse(
                            bedAssign.Bed.Room.Ward.Id,
                            bedAssign.Bed.Room.Ward.Name,
                            bedAssign.Bed.Room.Ward.Description)
                    ),
                    new BedTypeResponse(
                        bedAssign.Bed.BedType.Id,
                        bedAssign.Bed.BedType.Name,
                        bedAssign.Bed.BedType.Description
                    )
                )
            )
        ));
    }

    public async Task<PatientResponse?> UpdateAsync(int age, PatientRequest request, CancellationToken cancellationToken)
    {
        var patient = await context.Patients.FirstOrDefaultAsync(patient => patient.Age == age, cancellationToken);
        if (patient is null)
            return null;
        patient.Pesel = request.Pesel;
        patient.FirstName = request.FirstName;
        patient.LastName = request.LastName;
        patient.Age = request.Age;
        patient.Sex = false;
        await context.SaveChangesAsync(cancellationToken);
        return new PatientResponse(
        patient.Pesel,
        patient.FirstName,
        patient.LastName,
        patient.Age,
        patient.Sex ? "male" : "female",
        patient.Admissions.Select(admission => new AdmissionResponse(
            admission.Id,
            admission.AdmissionDate,
            admission.DischargeDate,
            new WardResponse(admission.Ward.Id, admission.Ward.Name, admission.Ward.Description),
            admission.PatientPesel
        )),
        patient.BedAssignments.Select(bedAssign => new BedAssignmentResponse(
                bedAssign.Id,
                bedAssign.PatientPesel,
                bedAssign.From,
                bedAssign.To,
                new BedResponse(
                    bedAssign.Bed.Id,
                    new RoomResponse(
                        bedAssign.Bed.Room.Id,
                        bedAssign.Bed.Room.HasTv,
                        new WardResponse(
                            bedAssign.Bed.Room.Ward.Id,
                            bedAssign.Bed.Room.Ward.Name,
                            bedAssign.Bed.Room.Ward.Description)
                    ),
                    new BedTypeResponse(
                        bedAssign.Bed.BedType.Id,
                        bedAssign.Bed.BedType.Name,
                        bedAssign.Bed.BedType.Description
                    )
                )
            )
        ));
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var patient = await context.Patients.FindAsync(id, cancellationToken);
        if (patient is null)
            return false;
        context.Patients.Remove(patient);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
