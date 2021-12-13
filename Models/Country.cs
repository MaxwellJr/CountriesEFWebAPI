using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace CountriesEFWebAPI.Models {
    [Table("Countries")]
    public class Country {
        public Country() {
            ID = 0;
            Name = String.Empty;
            Code = String.Empty;
            Capital = new City();
            Region = new Region();
            Population = 0;
            Area = 0;
        }

        public Country(in string name, in string code, in City capital, in Region region, int population, decimal area) {
            ID = 0;
            Name = name;
            Code = code;
            Capital = capital;
            Region = region;
            Population = population;
            Area = area;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public City Capital { get; set; }
        public Region Region { get; set; }
        public int Population { get; set; }
        public decimal Area { get; set; }
    }
}
