using PreparationForExam.Models;

namespace PreparationForExam.DTOs;

public record RoomRequest(
    bool HasTv,
    Ward WardId
);
