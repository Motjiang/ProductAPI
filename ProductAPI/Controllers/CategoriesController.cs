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
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var data = _categoryRepository.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")] // api/category/getbyid/1
        public IActionResult GetById(int id)
        {
            var data = _categoryRepository.GetById(id);
            return Ok(data);
        }


        [HttpPost]
        public IActionResult AddUpdate(Category model)
        {
            var status = new StatusDto();
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Validatation failed";
            }
            var result = _categoryRepository.AddUpdate(model);

            status.StatusCode = result ? 1 : 0;
            status.Message = result ? "Saved successfully" : "Error has occured";
            return Ok(status);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _categoryRepository.Delete(id);
            var status = new StatusDto
            {
                StatusCode = result ? 1 : 0,
                Message = result ? "deleted successfully" : "Error has occured"
            };
            return Ok(status);
        }
    }
}
