using Api.GRRInnovations.TodoManager.Application.Interfaces;
using Api.GRRInnovations.TodoManager.Domain.Models;
using Api.GRRInnovations.TodoManager.Infrastructure.Repositories;

namespace Api.GRRInnovations.TodoManager.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ICategoryModel> CreateCategoryIfNotExistAsync(string? categoryName)
        {
            if (string.IsNullOrEmpty(categoryName)) return null;

            var category = await _categoryRepository.GetAsync(categoryName);
            if (category == null)
            {
                return await _categoryRepository.CreateAsync(categoryName);
            }

            return category;
        }
    }
}
