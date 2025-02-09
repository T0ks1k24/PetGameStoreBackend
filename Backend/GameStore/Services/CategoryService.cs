using GameStore.Dtos.Category;
using GameStore.Interfaces;
using GameStore.Interfaces.IRepository;
using GameStore.Interfaces.IServices;
using GameStore.Models;

namespace GameStore.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name,
        });
    }

    public async Task<CategoryDto> GetCategoryAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null) return null;

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
        };
    }

    public async Task<bool> AddCategoryAsync(AddCategoryDto categoryDto)
    {
        var category = new Category
        {
            Name = categoryDto.Name,
        };
        return await _categoryRepository.CreateAsync(category);
    }

    public async Task<bool> UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null) return false;

        category.Name = categoryDto.Name;
        return await _categoryRepository.UpdateAsync(category);
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        return await _categoryRepository.DeleteAsync(id);
    }

    
}
