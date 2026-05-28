namespace PreparationForExam.DTOs;

public record CreateBedAssignment(
    int BedTypeId,
    int WardId,
    DateTime From,
    DateTime To
);
