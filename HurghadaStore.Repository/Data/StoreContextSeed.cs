using HurghadaStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HurghadaStore.Repository.Data
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext _dbContext) // CLR created an object implicitly
        {
            if (_dbContext.CarBrands.Count() == 0) // Count of ROWs
            // to ensure that seeding take place just for once(first time).
            // [using (Count() == 0) instead of (! and Any()) will make the performance much better].
            {
                var brandsdata = File.ReadAllText("../HurghadaStore.Repository/Data/DataSeed/brands.json"); // String Format
                var brands = JsonSerializer.Deserialize<List<CarBrand>>(brandsdata); // Convert to JSON

                if (brands?.Count() > 0) // "?" : Null Propagation Operator
                {
                    brands = brands.Select(b => new CarBrand
                    {
                        Name = b.Name,
                    }).ToList();

                    foreach (var brand in brands)
                    {
                        _dbContext.Set<CarBrand>().Add(brand);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }

            if (_dbContext.CarCategories.Count() == 0)
            {
                var categoriesdata = File.ReadAllText("../HurghadaStore.Repository/Data/DataSeed/categories.json");
                var categories = JsonSerializer.Deserialize<List<CarCategory>>(categoriesdata);

                if (categories?.Count() > 0)
                {
                    categories = categories.Select(c => new CarCategory
                    {
                        Name = c.Name,
                    }).ToList();

                    foreach (var category in categories)
                    {
                        _dbContext.Set<CarCategory>().Add(category);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }

            if (_dbContext.Cars.Count() == 0)
            {
                var carsdata = File.ReadAllText("../HurghadaStore.Repository/Data/DataSeed/cars.json");
                var cars = JsonSerializer.Deserialize<List<Car>>(carsdata);

                if (cars?.Count() > 0)
                {
                    foreach (var car in cars)
                    {
                        _dbContext.Set<Car>().Add(car);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }  
        }
    }
}
