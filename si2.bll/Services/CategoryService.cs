using AutoMapper;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.Category;
using si2.bll.Dtos.Results.Category;
using si2.dal.Entities;
using si2.dal.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public class CategoryService : ServiceBase, ICategoryService
    {
        public CategoryService(IUnitOfWork uow, IMapper mapper, ILogger<CategoryService> logger) : base(uow, mapper, logger)
        {
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto, CancellationToken ct)
        {
            CategoryDto categoryDto = null;
            try
            {
                var categoryEntity = _mapper.Map<Category>(createCategoryDto);
                await _uow.Categories.AddAsync(categoryEntity, ct);
                await _uow.SaveChangesAsync(ct);
                categoryDto = _mapper.Map<CategoryDto>(categoryEntity);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, string.Empty);
            }
            return categoryDto;
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(Guid id, CancellationToken ct)
        {
            CategoryDto categoryDto = null;

            if (_uow != null)
            {
                if (_uow.Categories != null)
                {
                    var categoryEntity = await _uow.Categories.GetAsync(id, ct);
                    if (categoryEntity != null)
                    {
                        categoryDto = _mapper.Map<CategoryDto>(categoryEntity);
                    }
                }
            }

            return categoryDto;
        }
    }
}
