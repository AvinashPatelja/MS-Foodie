using AutoMapper;
using Foodie.Services.CouponAPI.Model;
using Foodie.Services.CouponAPI.Model.Dto;

namespace Foodie.Services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig= new MapperConfiguration(config =>
            {
                config.CreateMap<Coupon,CouponDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
