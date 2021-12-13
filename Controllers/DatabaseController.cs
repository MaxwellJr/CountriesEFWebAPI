using CountriesEFWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesEFWebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase {
        private readonly CountriesContext context;

        public DatabaseController(CountriesContext dbContext) {
            context = dbContext;
        }

        [HttpGet]
        public async Task<List<CountryView>> GetDataTable() {
            List<CountryView> countries = await context.Countries.Include(c => c.Capital).Include(r => r.Region)
                .Select(model => new CountryView(model.Name, model.Code, model.Capital.Name, model.Region.Name, model.Population, model.Area)).ToListAsync();
            return countries;
        }

        [HttpPost]
        public async Task<ActionResult> AddCountry(CountryView countryView) {
            City capital = await context.Cities.SingleOrDefaultAsync(c => c.Name == countryView.Capital);
            if (capital == null) {
                capital = new City(0, countryView.Capital);
                await context.Cities.AddAsync(capital);
            }
            Region region = await context.Regions.SingleOrDefaultAsync(r => r.Name == countryView.Region);
            if (region == null) {
                region = new Region(0, countryView.Region);
                await context.Regions.AddAsync(region);
            }
            Country country = await context.Countries.SingleOrDefaultAsync(c => c.Code == countryView.Code);
            if (country == null) {
                country = new Country(countryView.Name, countryView.Code, capital, region, countryView.Population, countryView.Area);
                await context.Countries.AddAsync(country);
            } else {
                country.Population = countryView.Population;
                country.Area = countryView.Area;
                capital.Name = countryView.Capital;
                region.Name = countryView.Region;
            }
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{cca2}")]
        public async Task<ActionResult> DeleteCountry(string cca2) {
            Country country = await context.Countries.SingleAsync(c => c.Code == cca2);
            context.Countries.Remove(country);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
