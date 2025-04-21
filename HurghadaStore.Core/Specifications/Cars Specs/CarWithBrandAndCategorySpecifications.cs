using HurghadaStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurghadaStore.Core.Specifications.Cars_Specs
{
    // Inner Join of Brand and Category Tables with Car Table
    public class CarWithBrandAndCategorySpecifications : BaseSpecifications<Car>
    {
        public CarWithBrandAndCategorySpecifications(ProductSpecParams specParams)
            : base(P =>
                        // Left side must be TRUE so we can check the right one using (&&)
                        (!specParams.BrandId.HasValue || P.BrandId == specParams.BrandId.Value) && 
                        (!specParams.CategoryId.HasValue || P.CategoryId == specParams.CategoryId.Value)
                  ) 
        {
            Includes.Add(C => C.Brand);
            Includes.Add(C => C.Category);

            // totalCars = 10
            // pageSize = 5
            // pageIndex = 2

            ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize , specParams.PageSize);
            
        }

        public CarWithBrandAndCategorySpecifications(int id)
            : base(C => C.Id == id)
        {
            Includes.Add(C => C.Brand);
            Includes.Add(C => C.Category);
        }
       
    }
}
