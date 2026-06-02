using System.ComponentModel.DataAnnotations;
using PreparationForExam.Models;

namespace PreparationForExam.DTOs;

public record AdmissionRequest
(
    DateTime AdmissionDate,
    DateTime? DischargeDate,
    [Required]
    Ward WardId,
    [Required, MaxLength(11), MinLength(11)]
    string PatientPesel
);
