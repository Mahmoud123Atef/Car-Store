using AutoMapper;
using HurghadaStore.APIs.DTOs;
using HurghadaStore.Core.Entities;

namespace HurghadaStore.APIs.Helper
{
    public class CarPictureUrlResolver : IValueResolver<Car, CarToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public CarPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Car source, CarToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["ApiBaseUrl"]}/{source.PictureUrl}";
            }

            return string.Empty;
        }
    }
}
