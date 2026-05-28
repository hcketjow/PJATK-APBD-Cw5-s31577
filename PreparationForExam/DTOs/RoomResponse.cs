namespace PreparationForExam.DTOs;

public record RoomResponse(
    string Id,
    bool HasTv,
    WardResponse Ward
);
