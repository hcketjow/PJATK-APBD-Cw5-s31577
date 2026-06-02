using System.ComponentModel.DataAnnotations;

namespace PreparationForExam.DTOs;

public record WardRequest(
    [Required, MaxLength(300)]
    string Name,
    string? Description
);
