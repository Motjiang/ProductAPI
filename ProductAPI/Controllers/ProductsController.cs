using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models.Domain;
using ProductAPI.Models.DTOs;
using ProductAPI.Repositories.Abstract;
using System.Net.NetworkInformation;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetAll(string term = "")
        {
            var data = _productRepository.GetAll(term);
            return Ok(data);
        }

        [HttpGet("{id}")] // api/product/getbyid/1
        public IActionResult GetById(int id)
        {
            var data = _productRepository.GetById(id);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult AddUpdate(Product model)
        {
            var status = new StatusDto();
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Validatation failed";
            }
            var result = _productRepository.AddUpdate(model);

            status.StatusCode = result ? 1 : 0;
            status.Message = result ? "Saved successfully" : "Error has occured";
            return Ok(status);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _productRepository.Delete(id);
            var status = new StatusDto
            {
                StatusCode = result ? 1 : 0,
                Message = result ? "deleted successfully" : "Error has occured"
            };
            return Ok(status);
        }
    }
}
