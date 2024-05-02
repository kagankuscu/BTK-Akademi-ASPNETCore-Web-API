using AutoMapper;
using Entities.Dtos;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class CategoryManager : ICategoryService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public CategoryManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task DeleteOneCategoryAsync(int id, bool trackChanges)
        {
            var category = await _manager.Category.GetOneCategoryByIdAsync(id, trackChanges);
            _manager.Category.DeleteOneCategory(category);
            await _manager.SaveAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges) =>await _manager.Category.GetAllCategoriesAsync(trackChanges);

        public async Task<Category> GetOneCategoryAsync(int id, bool trackChanges) => await _manager.Category.GetOneCategoryByIdAsync(id, trackChanges);

        public async Task CreateOneCategoryAsync(CategoryDtoForInsertion categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            _manager.Category.CreateOneCategory(category);
            await _manager.SaveAsync();
        }

        public async Task UpdateOneCategoryAsync(int id, CategoryDtoForUpdate categoryDto, bool trackChanges)
        {
            var entity = await GetOneCategoryByIdAndCheckExists(id, trackChanges);

            if (entity is null)
                throw new CategoryNotFoundException(id);

            entity = _mapper.Map<Category>(categoryDto);

            _manager.Category.UpdateOneCategory(entity);
            await _manager.SaveAsync();
        }

        private async Task<Category> GetOneCategoryByIdAndCheckExists(int id, bool trackChanges)
        {
            var entity = await _manager.Category.GetOneCategoryByIdAsync(id, trackChanges);
            if (entity is null)
                throw new CategoryNotFoundException(id);

            return entity;
        }
    }
}