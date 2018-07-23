using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class IdentityAuthorDto : AuthorDto
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
    }
}