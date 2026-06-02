using System.ComponentModel.DataAnnotations;

namespace PreparationForExam.DTOs;

public record BedRequest
(
    [Required]
    int BedTypeId,
    [Required]
    int RoomId
);
