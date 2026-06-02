using System.ComponentModel.DataAnnotations;

namespace PreparationForExam.DTOs;

public record PatientRequest
(
    [Required, MaxLength(11), MinLength(11)]
    string Pesel,
    [Required, MaxLength(50)]
    string FirstName,
    [Required, MaxLength(100)]
    string LastName,
    int Age,
    string Sex
);
