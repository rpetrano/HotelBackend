using Microsoft.AspNetCore.Mvc;
using HotelBackend.Services;
using NetTopologySuite.Geometries;

namespace HotelBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public SearchController(IHotelService hotelService) 
        {
            _hotelService = hotelService;
        }

        [HttpGet]
        // Page starts from 0
        public async Task<IActionResult> SearchHotels(double lat, double lon, int page)
        {
            var currentLocation = new Point(lon, lat) { SRID = 4326 };
            var searchResults = await _hotelService.SearchHotels(currentLocation, page);

            return Ok(searchResults);
        } 
    }
}
