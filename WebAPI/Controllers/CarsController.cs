using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _carService.GetAll();
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("getCarsDetail")]
        public IActionResult GetCarsDetail()
        {
            var result = _carService.GetCarsDetail();
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("getCarDetailCarId")]
        public IActionResult GetCarDetail(int carId)
        {
            var result = _carService.GetCarDetailAndImagesDto(carId);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("getByBrandId")]
        public IActionResult GetByBrand(int brandId)
        {
            var result = _carService.GetCarsDetailByBrandId(brandId);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("getByColorId")]
        public IActionResult GetByColor(int colorId)
        {
            var result = _carService.GetCarsDetailByColorId(colorId);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("getByBrandIdAndColorId")]
        public IActionResult GetByBrandAndColor(int brandId, int colorId)
        {
            var result = _carService.GetCarsDetailByBrandIdAndColorId(brandId, colorId);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);

        }
        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = _carService.GetById(id);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Car car)
        {
            var result = _carService.Add(car);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

        //[HttpPost("addtransactiontest")]
        //public IActionResult AddTransactionTest(Car car)
        //{
        //    var result = _carService.AddTransactionTest(car);
        //    if (result.Success)
        //        return Ok(result);
        //    else
        //        return BadRequest(result);
        //}
    }
}