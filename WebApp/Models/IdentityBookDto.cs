using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class IdentityBookDto : BookDto
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
    }
}