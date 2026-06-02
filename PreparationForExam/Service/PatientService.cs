using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PreparationForExam.DTOs;
using PreparationForExam.Exceptions;
using PreparationForExam.Infrastructre;
using PreparationForExam.Models;

namespace PreparationForExam.Service;

public class PatientService(ApbdContext context) : IPatientService
{
    public async Task<IEnumerable<PatientResponse>> GetAllAsync(string? serach, CancellationToken cancellationToken)
    {
        var query = context.Patients.AsQueryable();
        if (!string.IsNullOrWhiteSpace(serach))
        {
            var SerachToLower =  serach.ToLower();
            query = query.Where(patient =>
                patient.FirstName.ToLower().Contains(SerachToLower) ||
                patient.LastName.ToLower().Contains(SerachToLower));
        }
        
        return await query.Select(patient => new PatientResponse(
            patient.Pesel,
            patient.FirstName,
            patient.LastName,
            patient.Age,
            patient.Sex ? "Male" : "Female",
            patient.Admissions.Select(admission => new AdmissionResponse(
                admission.Id,
                admission.AdmissionDate,
                admission.DischargeDate, 
                new WardResponse(
                    admission.Ward.Id,
                    admission.Ward.Name,
                    admission.Ward.Description
                )
            )),
            patient.BedAssignments.Select(assignment => new BedAssignmentResponse(
                assignment.Id,
                assignment.From,
                assignment.To,
                new BedResponse(
                   assignment.Bed.Id,
                   new BedTypeResponse(
                       assignment.Bed.BedType.Id,
                       assignment.Bed.BedType.Name,
                       assignment.Bed.BedType.Description
                   ),
                   new RoomResponse(
                       assignment.Bed.Room.Id,
                       assignment.Bed.Room.HasTv,
                       new WardResponse(
                           assignment.Bed.Room.Ward.Id,
                           assignment.Bed.Room.Ward.Name,
                           assignment.Bed.Room.Ward.Description
                       )
                   )
                )
            ))
        )).ToListAsync(cancellationToken);
    }

    // public async Task<int> AssignBedAsync(string pesel, CreateBedAssignment request, CancellationToken cancellationToken)
    // {
    //     var patientExists = await context.Patients.AnyAsync(p => p.Pesel == pesel, cancellationToken);
    //     if (!patientExists)
    //         throw new NotFoundException($"Patient with PESEL '{pesel}' not found.");
    //     var wardExists = await context.Wards.AnyAsync(w => w.Id == request.WardId, cancellationToken);
    //     if (!wardExists)
    //         throw new NotFoundException($"Ward with id '{request.WardId}' not found.");
    //     var bedTypeExists = await context.BedTypes.AnyAsync(bt => bt.Id == request.BedTypeId, cancellationToken);
    //     if (!bedTypeExists)
    //         throw new NotFoundException($"Bed type with id '{request.BedTypeId}' not found.");
    //     var bed = await context.Beds.Where(b => b.BedTypeId == request.BedTypeId && b.Room.WardId == request.WardId)
    //         .Where(b => !b.BedAssignments.Any(ba => ba.From < request.To && (ba.To == null || ba.To > request.From)))
    //         .FirstOrDefaultAsync(cancellationToken);
    //     if (bed is null)
    //         throw new NotFoundException(
    //             $"No free bed of type '{request.BedTypeId}' available in ward '{request.WardId}' for the requested period.");
    //     var assignment = new BedAssignment
    //     {
    //         PatientPesel = pesel,
    //         BedId = bed.Id,
    //         From = bed.From,
    //         To = bed.To
    //     };
    //     context.BedAssignments.Add(assignment);
    //     await context.SaveChangesAsync(cancellationToken);
    //     return assignment.Id;
    // }
}
