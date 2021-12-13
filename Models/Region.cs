using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CountriesEFWebAPI.Models {
    [Table("Regions")]
    public class Region {
        public Region() {
            ID = 0;
            Name = String.Empty;
        }

        public Region(int regionID, in string regionName) {
            ID = regionID;
            Name = regionName;
        }

        [Column("ID")]
        public int ID { get; set; }

        [Column("Name")]
        public string Name { get; set; }
    }
}
