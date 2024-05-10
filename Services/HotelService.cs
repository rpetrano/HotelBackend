using HotelBackend.Models;
using HotelBackend.Repositories;
using Microsoft.Extensions.Options;
using NetTopologySuite.Geometries;

namespace HotelBackend.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IHotelScoreCalculationService _scoreCalculator;
        private readonly IOptions<SearchOptions> _options;

        public HotelService(
            IHotelRepository hotelRepository,
            IHotelScoreCalculationService scoreCalculator,
            IOptions<SearchOptions> options
        ) {
            _hotelRepository = hotelRepository;
            _scoreCalculator = scoreCalculator;
            _options = options;
        }

        public async Task CreateHotel(Hotel hotel)
        {
            await _hotelRepository.CreateHotel(hotel);
        }

        public async Task<Hotel?> GetHotelById(int id)
        {
            return await _hotelRepository.GetHotelById(id);
        }

        public async Task<IEnumerable<Hotel>> GetAllHotels()
        {
            return await _hotelRepository.GetAllHotels();
        }

        public async Task<bool> HotelExists(int id)
        {
            return await _hotelRepository.HotelExists(id);
        }

        public async Task UpdateHotel(Hotel hotel)
        {
            await _hotelRepository.UpdateHotel(hotel);
        }

        public async Task DeleteHotel(Hotel hotel)
        {
            await _hotelRepository.DeleteHotel(hotel);
        }

        public async Task<IEnumerable<HotelSearchResult>> SearchHotels(Point currentLocation, int page)
        {
            var offset = page * _options.Value.PageSize;
            var hotels = await _hotelRepository.SearchHotels(currentLocation, _options.Value.MaxDistance, _options.Value.Limit);

            return hotels.Select(h => new HotelSearchResult
            {
                Hotel = h,
                Score = _scoreCalculator.CalculateHotelScore(h, currentLocation)
            })
            .OrderByDescending(result => result.Score)
            .Skip(offset)
            .Take(_options.Value.PageSize);
        }
    }
}
