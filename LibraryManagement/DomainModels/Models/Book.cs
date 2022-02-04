using DomainModels.Models.Base;

namespace DomainModels.Models
{
    public class Book:Entity
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }
}
