using GameStore.Dtos.Category;
using GameStore.Interfaces;
using GameStore.Interfaces.IServices;
using GameStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _categoryService.GetCategoriesAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        return Ok(await _categoryService.GetCategoryAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] AddCategoryDto categoryDto)
    {
        var createCategory = await _categoryService.AddCategoryAsync(categoryDto);
        return CreatedAtAction(nameof(GetById), new { });
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryDto updateCategoryDto)
    {
        var updateCategory = _categoryService.UpdateCategoryAsync(id, updateCategoryDto);

        if (updateCategory == null)
            return NotFound();

        return Ok(updateCategory);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromBody] int id)
    {
        var delCategory = await _categoryService.DeleteCategoryAsync(id);

        if (delCategory == false)
            return BadRequest("Not game remove!");

        return Ok("Game remove!");
    }
}
