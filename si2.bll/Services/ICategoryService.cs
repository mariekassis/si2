using si2.bll.Dtos.Requests.Category;
using si2.bll.Dtos.Results.Category;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public interface ICategoryService : IServiceBase
    {
        Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto, CancellationToken ct);
        Task<CategoryDto> GetCategoryByIdAsync(Guid id, CancellationToken ct);

    }
}
