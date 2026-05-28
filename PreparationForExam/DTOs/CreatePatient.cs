using System.ComponentModel.DataAnnotations;

namespace PreparationForExam.DTOs;

public record CreatePatient (
    [MaxLength(11), MinLength(11)] string Pesel,
    [MaxLength(50)] string FirstName,
    [MaxLength(100)] string LastName,
    int Age,
    bool Sex
);
