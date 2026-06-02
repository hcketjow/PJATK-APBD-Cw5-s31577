namespace PreparationForExam.DTOs;

public record PatientSimpleResponse
(
    string Pesel,
    string FirstName,
    string LastName,
    int Age,
    string Sex
);
