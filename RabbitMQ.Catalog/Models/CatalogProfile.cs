using AutoMapper;

namespace RabbitMQ.Catalog.Models
{
    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        { 
            CreateMap<CatalogItem, CatalogReadDto>();
            CreateMap<CatalogReadDto, CatalogItem>();
            CreateMap<CatalogCreateDto, CatalogItem>();
        }
    }
}
