namespace PreparationForExam.DTOs;

public record BedResponse
(
    int Id,
    RoomResponse Room,
    BedTypeResponse BedType
);
