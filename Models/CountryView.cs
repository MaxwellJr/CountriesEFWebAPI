using System;
using System.Text.Json.Serialization;

namespace CountriesEFWebAPI.Models {
    public class CountryView {
        public CountryView() {
            Name = String.Empty;
            Code = String.Empty;
            Capital = String.Empty;
            Region = String.Empty;
            Population = 0;
            Area = 0;
        }

        public CountryView(in string name, in string code, in string capital, in string region, int population, decimal area) {
            Name = name;
            Code = code;
            Capital = capital;
            Region = region;
            Population = population;
            Area = area;
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("cca2")]
        public string Code { get; set; }

        [JsonPropertyName("capital")]
        public string Capital { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("population")]
        public int Population { get; set; }

        [JsonPropertyName("area")]
        public decimal Area { get; set; }
    }
}
