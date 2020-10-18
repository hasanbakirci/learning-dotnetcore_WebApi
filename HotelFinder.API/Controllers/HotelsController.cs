using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelFinder.Business.Abstract;
using HotelFinder.Business.Concrete;
using HotelFinder.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private IHotelService _hotelService;
        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        // Swagger için /// yapınca algılar

        /// <summary>
        /// Get All Hotels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var hotels = await _hotelService.GetAllHotels();
            return Ok(hotels); // 200 döndür + data
        }

        /// <summary> 
        /// Get Hotel By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel = await _hotelService.GetHotelById(id);
            if(hotel != null)
            {
                return Ok(hotel); // 200 +data
            }
            return NotFound(); // 404
            
        }

        [HttpGet]
        [Route("[action]/{name}")]
        public async Task<IActionResult> GetHotelByName(string name)
        {
            var hotel =await _hotelService.GetHotelByName(name);
            if (hotel != null)
            {
                return Ok(hotel); //200 + data
            }
            return NotFound();

        }

        //[HttpGet]
        //[Route("[action]")] //   api/hotels/getHotelByIdAndName?id=2&name=alara
        //public Task<IActionResult> GetHotelByIdAndName(int id, string name)
        //{
          //  return Ok();
        //}

        /// <summary>
        /// Create Hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")] //     api/hotels/CreateHotel/
        public async Task<IActionResult> CreateHotel([FromBody]Hotel hotel)
        {
            var createHotel = await _hotelService.CreateHotel(hotel);
            return CreatedAtAction("Get", new { id = createHotel.id }, createHotel); // 201 + data
        }

        /// <summary>
        /// Update Hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")] //     api/hotels/UpdateHotel/
        public async Task<IActionResult> UpdateHotel([FromBody] Hotel hotel)
        {
            if(await _hotelService.GetHotelById(hotel.id)!= null)
            {
                return Ok(await _hotelService.UpdateHotel(hotel)); // 200 +data
            }
            return NotFound();
        }

        /// <summary>
        /// Delete Hotel
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("[action]/{id}")] //     api/hotels/DeleteHotel/1
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (await _hotelService.GetHotelById(id) != null)
            {
                await _hotelService.DeleteHotel(id);
                return Ok(); // 200 + data
            }
            return NotFound();
        }
    }
}
