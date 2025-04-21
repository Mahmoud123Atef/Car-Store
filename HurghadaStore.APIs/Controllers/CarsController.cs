using AutoMapper;
using HurghadaStore.APIs.DTOs;
using HurghadaStore.APIs.Errors;
using HurghadaStore.Core.Entities;
using HurghadaStore.Core.Repositories.Contract;
using HurghadaStore.Core.Specifications;
using HurghadaStore.Core.Specifications.Cars_Specs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HurghadaStore.APIs.Controllers
{
    public class CarsController : BaseApiController
    {
        private readonly IGenericRepository<Car> _carsRepo;
        private readonly IGenericRepository<CarBrand> _carBrandRepo;
        private readonly IGenericRepository<CarCategory> _carCategoryRepo;
        private readonly IMapper _mapper;

        public CarsController(IGenericRepository<Car> carsRepo,
                              IGenericRepository<CarBrand> carBrandRepo,
                              IGenericRepository<CarCategory> carCategoryRepo,
                              IMapper mapper) 
        // ask CLR to create an object implicitly from any class implementing this interface (DI).
        {
            _carsRepo = carsRepo;
            _carBrandRepo = carBrandRepo;
            _carCategoryRepo = carCategoryRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarToReturnDto>>> GetCars([FromQuery] ProductSpecParams specParams) // Clean Code 
        {
            var spec = new CarWithBrandAndCategorySpecifications(specParams);

            var cars = await _carsRepo.GetAllWithSpecAsync(spec);

            return Ok(_mapper.Map<IEnumerable<Car>, IEnumerable<CarToReturnDto>>(cars));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CarToReturnDto>> GetCar(int id)
        {
            var spec = new CarWithBrandAndCategorySpecifications(id);

            var car = await _carsRepo.GetWithSpecAsync(spec);

            if (car == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper.Map<Car, CarToReturnDto>(car));
        }

        [HttpGet("brands")] // /baseURL/api/products/brands
        public async Task<ActionResult<IEnumerable<CarBrand>>> GetBrands()
        {
            var brands = await _carBrandRepo.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("categories")] // /baseURL/api/products/brands
        public async Task<ActionResult<IEnumerable<CarCategory>>> GetCategories()
        {
            var brands = await _carCategoryRepo.GetAllAsync();
            return Ok(brands);
        }
    }
}
