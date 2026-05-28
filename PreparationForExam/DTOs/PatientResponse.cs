using PreparationForExam.Models;

namespace PreparationForExam.DTOs;

public record PatientResponse
(
    string Pesel,
    string FirstName,
    string LastName,
    int Age,
    bool Sex,
    ICollection<Admission> Admissions,
    ICollection<BedAssignment> BedAssignments
);
