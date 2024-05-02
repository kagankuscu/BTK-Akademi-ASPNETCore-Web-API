using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Dtos;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public CategoryController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCAtegoriesAsync()
        {
            return Ok(await _manager.CategoryService.GetAllCategoriesAsync(false));
        }
        
        [HttpGet("{id:int}")]
        // [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetOneCategory([FromRoute] int id)
        {
            var category = await _manager.CategoryService.GetOneCategoryAsync(id, false);

            if (category is null)
                throw new CategoryNotFoundException(id);

            return Ok(category);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateOneCategory([FromBody] CategoryDtoForInsertion categoryDto)
        {
            _manager.CategoryService.CreateOneCategoryAsync(categoryDto);
            return StatusCode(201);
        }

        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateOneCategoryAsync([FromRoute(Name = "id")] int id, [FromBody] CategoryDtoForUpdate categoryDto)
        {          
            await _manager.CategoryService.UpdateOneCategoryAsync(id, categoryDto, false);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneCategoryAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.CategoryService.DeleteOneCategoryAsync(id, false);
            return NoContent();
        }
    }
}