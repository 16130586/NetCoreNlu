using Microsoft.EntityFrameworkCore;
using NetProject.Context;
using NetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.DataAccessor
{
    public class AddressData
    {
        private readonly OurDbContext _dbContext;
        public AddressData(OurDbContext context)
        {
            _dbContext = context;
        }
        public List<City> Cities() {
            return _dbContext.Cities.ToList();
        }
        public List<District> Districts(int cityId)
        {
            return _dbContext.Districts.Where(dt => dt.CityId == cityId).ToList();
        }

        public City GetCity(int select_City)
        {
            return _dbContext.Cities.Where(ct => ct.Id == select_City).FirstOrDefault();
        }

        public District GetDistrict(int select_District)
        {
            return _dbContext.Districts.Where(ct => ct.Id == select_District).FirstOrDefault();
        }
    }
}
