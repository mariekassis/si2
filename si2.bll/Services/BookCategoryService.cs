using AutoMapper;
using Microsoft.Extensions.Logging;
using si2.dal.Entities;
using si2.dal.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public class BookCategoryService : ServiceBase, IBookCategoryService
    {
        public BookCategoryService(IUnitOfWork uow, IMapper mapper, ILogger<BookCategoryService> logger) : base(uow, mapper, logger)
        {
        }

        public async Task<BookCategory> CreateBookCategoryAsync(Guid bookId, Guid categoryId, CancellationToken ct)
        {
            var bookCategoryEntity = new BookCategory();

            try
            {
                bookCategoryEntity.BookId = bookId;
                bookCategoryEntity.CategoryId = categoryId;
                await _uow.BookCategories.AddAsync(bookCategoryEntity, ct);
                await _uow.SaveChangesAsync(ct);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, string.Empty);
            }
            return bookCategoryEntity;
        }

    }
}
