
using System.ComponentModel.DataAnnotations;

namespace DomainModels.Dtos
{
    public class BookDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
    }
}
