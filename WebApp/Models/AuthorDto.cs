using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class AuthorDto
    {
        [Display(Name = "First name")]
        [Required, StringLength(20)]
        public string FirstName { get; set; }

        [Required, StringLength(20)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
    }
}