using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CountriesEFWebAPI.Models {
    [Table("Cities")]
    public class City
    {
        public City() {
            ID = 0;
            Name = String.Empty;
        }

        public City(int cityID, in string cityName) {
            ID = cityID;
            Name = cityName;
        }

        [Column("ID")]
        public int ID { get; set; }

        [Column("Name")]
        public string Name { get; set; }
    }
}
