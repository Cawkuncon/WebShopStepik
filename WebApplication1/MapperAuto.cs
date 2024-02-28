using AutoMapper;
using OnlineShop.DB.Models;
using WebApplication1.Models;

namespace WebApplication1
{
    public class MapperAuto: Profile
    {
        public MapperAuto()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(x => x.Count, opt => opt.MapFrom(pr => 0));    
        }
    }
}
