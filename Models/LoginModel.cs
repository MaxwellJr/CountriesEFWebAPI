using System.ComponentModel.DataAnnotations;

namespace CountriesEFWebAPI.Models {
    public class LoginModel {
        [Required(ErrorMessage = "You have to input username")]
        [DataType(DataType.Text)]
        public string Login { get; set; }

        [Required(ErrorMessage = "You have to input password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
