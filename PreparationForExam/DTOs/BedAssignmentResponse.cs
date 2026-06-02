namespace PreparationForExam.DTOs;

public record BedAssignmentResponse(
    int Id,
    string PatientPesel,
    DateTime From,
    DateTime? To,
    BedResponse Bed,
    PatientResponse Patient
);
