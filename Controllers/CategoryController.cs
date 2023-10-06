using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using complainSystem.models;
using ComplainSystem.models;
using ComplainSystem.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;

namespace ComplainSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;

        }
        [HttpGet]
        [Route("GetAllCategories")]

        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            return Ok(await _categoryService.GetAllCategories());
        }

    }
}