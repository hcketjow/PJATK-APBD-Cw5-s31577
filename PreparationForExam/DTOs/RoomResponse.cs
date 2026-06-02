namespace PreparationForExam.DTOs;

public record RoomResponse(
    int Id,
    bool HasTv,
    WardResponse Ward
);
