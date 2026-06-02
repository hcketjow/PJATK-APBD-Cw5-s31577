namespace PreparationForExam.DTOs;

public class BedResponse(int id, RoomResponse room, BedTypeResponse bedType)
{
    public int Id { get; set; } = id;
    public RoomResponse Room { get; set; } = room;
    public BedTypeResponse BedType { get; set; } = bedType;
}
