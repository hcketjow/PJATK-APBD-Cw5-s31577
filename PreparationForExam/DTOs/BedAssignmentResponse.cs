using PreparationForExam.Models;

namespace PreparationForExam.DTOs;

public record BedAssignmentResponse(
    int Id,
    DateTime From,
    DateTime? To,
    BedResponse Bed
);

