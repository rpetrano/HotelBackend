using Microsoft.AspNetCore.Mvc;
using HotelBackend.Services;
using HotelBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using HotelBackend.Attributes;

namespace HotelBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelsController(IHotelService hotelService) 
        {
            _hotelService = hotelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels() 
        {
            var hotels = await _hotelService.GetAllHotels();
            return Ok(hotels);
        }

        [HttpPost]
        [ApiKeyAuth]
        public async Task<IActionResult> CreateHotel([FromBody] Hotel hotel)
        {
            try 
            {
                await _hotelService.CreateHotel(hotel);
                return CreatedAtAction(nameof(GetHotelById), new { id = hotel.Id }, hotel);
            } 
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel = await _hotelService.GetHotelById(id);
            if (hotel == null) 
            {
                return NotFound();
            }
            return Ok(hotel); 
        }

        [HttpPut("{id}")]
        [ApiKeyAuth]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] Hotel hotel)
        {
            if (id != hotel.Id) 
            {
                return BadRequest();
            }

            try
            {
                await _hotelService.UpdateHotel(hotel);  
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpDelete("{id}")]
        [ApiKeyAuth]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotelService.GetHotelById(id);
            if (hotel == null)
            {
                return NotFound();
            }

            await _hotelService.DeleteHotel(hotel);
            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _hotelService.HotelExists(id); 
        }
    }

}
