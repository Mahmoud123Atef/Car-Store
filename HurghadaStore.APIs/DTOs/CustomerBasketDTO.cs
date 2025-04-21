using HurghadaStore.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace HurghadaStore.APIs.DTOs
{
    public class CustomerBasketDTO
    {
        [Required] // Data Annotation
        public string Id { get; set; }
        public List<BasketItemDTO> Items { get; set; }
    }
}
