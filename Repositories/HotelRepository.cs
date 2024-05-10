using HotelBackend.Models;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries; 

namespace HotelBackend.Repositories
{
  public class HotelRepository : IHotelRepository
  {
      private readonly MainDbContext _context;

      public HotelRepository(MainDbContext context)
      {
           _context = context;
      }

      public async Task CreateHotel(Hotel hotel)
      {
          _context.Hotels.Add(hotel);
          await _context.SaveChangesAsync();
      }

      public async Task<Hotel?> GetHotelById(int id)
      {
          return await _context.Hotels.FindAsync(id);
      }

      public async Task<IEnumerable<Hotel>> GetAllHotels()
      {
          return await _context.Hotels.ToListAsync();
      }

      public async Task<bool> HotelExists(int id)
      {
          return await _context.Hotels.AnyAsync(e => e.Id == id);
      }

      public async Task UpdateHotel(Hotel hotel)
      {
          _context.Entry(hotel).State = EntityState.Modified;
          await _context.SaveChangesAsync();
      }

      public async Task DeleteHotel(Hotel hotel)
      {
          _context.Hotels.Remove(hotel);
          await _context.SaveChangesAsync();
      }

      public async Task<IEnumerable<Hotel>> SearchHotels(Point currentLocation, double radius, int limit)
      {
        // The .OrderBy is done here to make the dataset a partially sorted set.
        // That will sped up noticeably the sorting of the final results if LINQ chooses to use quicksort.
        // The scoring should be done in the database, but I have no time for that.
        var query = _context.Hotels
          .Where(h => h.GeoLocation.IsWithinDistance(currentLocation, radius))
          .OrderBy(h => h.GeoLocation.Distance(currentLocation))
          .Take(limit);
        
        return await query.ToListAsync();
      }
  }
}
