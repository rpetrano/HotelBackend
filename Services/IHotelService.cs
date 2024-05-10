using HotelBackend.Models;
using NetTopologySuite.Geometries;

namespace HotelBackend.Services 
{
    public interface IHotelService
    {
        Task<IEnumerable<Hotel>> GetAllHotels();
        Task<Hotel?> GetHotelById(int id);
        Task CreateHotel(Hotel hotel);
        Task UpdateHotel(Hotel hotel);
        Task DeleteHotel(Hotel hotel);
        Task<bool> HotelExists(int id);
        Task<IEnumerable<HotelSearchResult>> SearchHotels(Point currentLocation, int page);
    }
}