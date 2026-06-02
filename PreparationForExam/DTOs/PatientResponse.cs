namespace PreparationForExam.DTOs;

public record PatientResponse(
    string Pesel,
    string FirstName,
    string LastName,
    int Age,
    string Sex,
    IEnumerable<AdmissionResponse> Admissions,
    IEnumerable<BedAssignmentResponse> BedAssignments
);
