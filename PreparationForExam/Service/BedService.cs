using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PreparationForExam.DTOs;
using PreparationForExam.Infrastructre;
using PreparationForExam.Models;

namespace PreparationForExam.Service;

public class BedService(ApbdContext context) : IBedService
{
    public async Task<List<BedResponse>> GetAllAsync(string? serach, CancellationToken cancellationToken)
    {
        var seraches = context.Beds.AsQueryable();
        if (!string.IsNullOrWhiteSpace(serach))
        {
            var SerachToLower =  serach.ToLower();
            seraches = seraches.Where(bed => bed.BedType.Name.ToLower().Contains(SerachToLower));
        }
        return await seraches.Select(bed => new BedResponse(
            bed.Id,
            new BedTypeResponse(bed.BedType.Id, bed.BedType.Name, bed.BedType.Description),
            new RoomResponse(bed.Room.Id, bed.Room.HasTv,
                new WardResponse(bed.Room.Ward.Id, bed.Room.Ward.Name, bed.Room.Ward.Description
            ))
        )).ToListAsync(cancellationToken);
    }

    public async Task<BedResponse?> CreateAsync(BedRequest request)
    {
        var roomExists = await context.Rooms.AnyAsync(r => r.Id == request.RoomId);
        if (!roomExists) return null!;
        var bedTypeExists = await context.Rooms.AnyAsync(t => t.Id == request.BedTypeId);
        if (!bedTypeExists) return null!;
        var bed = new Bed
        {
            RoomId = request.RoomId,
            BedTypeId = request.BedTypeId
        };
        context.Beds.Add(bed);
        await context.SaveChangesAsync();
        await context.Entry(bed).Reference(bed => bed.BedType).LoadAsync();
        await context.Entry(bed).Reference(room => room.Room).LoadAsync();

        return new BedResponse(
            bed.Id,
            new RoomResponse(bed.Room.Id, bed.Room.HasTv,
                new WardResponse(bed.Room.Ward.Id, bed.Room.Ward.Name, bed.Room.Ward.Description)
            ),
            new BedTypeResponse(bed.BedType.Id, bed.BedType.Name, bed.BedType.Description)
        );
    }
}
