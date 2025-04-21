using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurghadaStore.Core.Entities
{
    public class Car : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string PictureUrl { get; set; }

        //[ForeignKey(nameof(Car.Brand))] // will be handled in Fluent Api (Configuration file).
        public int BrandId { get; set; }

        //[ForeignKey(nameof(Car.Category))]
        public int CategoryId { get; set; }

        //[InverseProperty(nameof(CarBrand.Cars))]
        public CarBrand Brand { get; set; } // Navigation Property (help me to make joins between tables)

        //[InverseProperty(nameof(CarCategory.Cars))]
        public CarCategory Category { get; set; } // Navigation Property

    }
}
