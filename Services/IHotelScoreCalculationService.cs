using HotelBackend.Models;
using NetTopologySuite.Geometries;

namespace HotelBackend.Services
{
    public interface IHotelScoreCalculationService
    {
        double CalculateHotelScore(Hotel hotel, Point currentLocation);
    }
}