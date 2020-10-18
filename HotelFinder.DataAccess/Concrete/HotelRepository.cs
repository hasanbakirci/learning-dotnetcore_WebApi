using HotelFinder.DataAccess.Abstract;
using HotelFinder.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinder.DataAccess.Concrete
{
    public class HotelRepository : IHotelRepository
    {
        public async Task<Hotel> CreateHotel(Hotel hotel)
        {
            using (var hotelDbcontext = new HotelDbContext())
            {
                hotelDbcontext.hotels.Add(hotel);
                await hotelDbcontext.SaveChangesAsync();
                return hotel;
            }
        }

        public async Task DeleteHotel(int id)
        {
            using (var hotelDbcontext = new HotelDbContext())
            {
                var deletedHotel = await GetHotelById(id);
                hotelDbcontext.hotels.Remove(deletedHotel);
                await hotelDbcontext.SaveChangesAsync();
            }
        }

        public async Task<List<Hotel>> GetAllHotels()
        {
            using (var hotelDbcontext = new HotelDbContext())
            {
                return await hotelDbcontext.hotels.ToListAsync();
            }
        }

        public async Task<Hotel> GetHotelById(int id)
        {
            using (var hotelDbcontext = new HotelDbContext())
            {
                return await hotelDbcontext.hotels.FindAsync(id);
            }
        }

        public async Task<Hotel> GetHotelByName(string name)
        {
            using (var hotelDbcontext = new HotelDbContext())
            {
                return await hotelDbcontext.hotels.FirstOrDefaultAsync(x => x.Name.ToLower()==name.ToLower());
            }
        }

        public async Task<Hotel> UpdateHotel(Hotel hotel)
        {
            using (var hotelDbcontext = new HotelDbContext())
            {
                hotelDbcontext.hotels.Update(hotel);
                await hotelDbcontext.SaveChangesAsync();
                return hotel;
            }
        }
    }
}
