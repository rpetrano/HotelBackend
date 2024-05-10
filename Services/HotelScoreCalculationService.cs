using HotelBackend.Models;
using NetTopologySuite.Geometries;

namespace HotelBackend.Services {
  class HotelScoreCalculationService : IHotelScoreCalculationService
  {
    public double CalculateHotelScore(Hotel hotel, Point currentLocation)
    {
      var distance = hotel.GeoLocation.Distance(currentLocation);
      var price = (double) hotel.Price;
      // TODO: better price formula
      return 1 / (distance * price);
    }
  }
}