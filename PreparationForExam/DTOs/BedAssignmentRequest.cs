using System.ComponentModel.DataAnnotations;
using PreparationForExam.Models;

namespace PreparationForExam.DTOs;

public record BedAssignmentRequest
(
    [Required, MaxLength(11), MinLength(11)]
    string PatientPesel,
    DateTime From,
    DateTime? To,
    Bed BedId,
    Patient PatientId
);
