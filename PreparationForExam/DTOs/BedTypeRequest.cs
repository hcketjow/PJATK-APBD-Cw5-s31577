using System.ComponentModel.DataAnnotations;

namespace PreparationForExam.DTOs;

public record BedTypeRequest(
    [MaxLength(300)]
    string Name,
    string? Description
);
