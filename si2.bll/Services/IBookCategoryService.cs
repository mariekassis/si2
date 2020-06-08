using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public interface IBookCategoryService : IServiceBase
    {
        Task<BookCategory> CreateBookCategoryAsync(Guid bookId, Guid categoryId, CancellationToken ct);
        Task<BookCategory> GetBookCategoryByIdAsync(Guid bookId, CancellationToken ct);
    }
}
