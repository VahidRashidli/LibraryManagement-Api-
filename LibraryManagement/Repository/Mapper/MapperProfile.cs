
using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Models;

namespace Repository.Mapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
        }
    }
}
