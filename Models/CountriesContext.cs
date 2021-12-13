using Microsoft.EntityFrameworkCore;


namespace CountriesEFWebAPI.Models {
    public class CountriesContext : DbContext {
        public CountriesContext() { }

        public CountriesContext(DbContextOptions<CountriesContext> options) : base(options) { }

        public DbSet<City> Cities { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
