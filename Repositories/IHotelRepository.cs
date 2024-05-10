using HotelBackend.Models;
using NetTopologySuite.Geometries;

namespace HotelBackend.Repositories
{
    public interface IHotelRepository
    {
        Task CreateHotel(Hotel hotel);
        Task DeleteHotel(Hotel hotel);
        Task<IEnumerable<Hotel>> GetAllHotels();
        Task<Hotel?> GetHotelById(int id);
        Task<bool> HotelExists(int id);
        Task UpdateHotel(Hotel hotel);
        Task<IEnumerable<Hotel>> SearchHotels(Point currentLocation, double radius, int limit);
    }
}
