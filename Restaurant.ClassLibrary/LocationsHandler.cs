using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ClassLibrary
{
    public class LocationsHandler
    {
        public List<Country> GetCountries()
        {
            using(PakClassifiedContext context=new PakClassifiedContext())
            {
                return (from c in context.Countries
                 select c).ToList();
            }
        }


        public List<Province> GetProvinces(Country country)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from p in context.Provinces
                        where p.Country.Id == country.Id
                        select p).ToList();
            }
        }

        public List<City> GetCities(Province province)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from c in context.Cities
                                   .Include("Province.Country")
                        where c.Province.Id == province.Id
                        select c).ToList();
            }           
        }

        public List<City> GetCities(Country country)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from c in context.Cities
                                   .Include("Province.Country")
                        where c.Province.Country.Id == country.Id
                        select c).ToList();
            }
        }

        public City GetCityById(int id)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from c in context.Cities
                       .Include(a => a.Province)
                       .Include(a => a.Province.Country)

                        where c.Id == id
                        select c).FirstOrDefault();
            }
        }
    }
}
