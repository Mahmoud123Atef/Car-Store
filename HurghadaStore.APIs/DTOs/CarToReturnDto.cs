using HurghadaStore.Core.Entities;

namespace HurghadaStore.APIs.DTOs
{
    public class CarToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string PictureUrl { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public string Brand { get; set; } 
        public string Category { get; set; }
    }
}
