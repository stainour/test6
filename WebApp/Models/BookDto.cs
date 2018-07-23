using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class BookDto
    {
        [Required]
        [MinLength(1)]
        public List<IdentityAuthorDto> Authors { get; set; }

        public string Image { get; set; }

        [RegularExpression("(ISBN[-]*(1[03])*[ ]*(: ){0,1})*(([0-9Xx][- ]*){13}|([0-9Xx][- ]*){10})", ErrorMessage = "Available ISBN formats are: 0-19-852663-6 or 978-1-56619-909-4")]
        public string ISBN { get; set; }

        public DateTime? IssueDate { get; set; }

        [Range(1, 10000)]
        [Display(Name = "Page count")]
        public int PageCount { get; set; }

        public string Publisher { get; set; }

        [Required, StringLength(30)]
        public string Title { get; set; }
    }
}