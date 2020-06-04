using AutoMapper;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.Book;
using si2.bll.Dtos.Results.Book;
using si2.dal.Entities;
using si2.dal.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public class BookService : ServiceBase, IBookService
    {
        public BookService(IUnitOfWork uow, IMapper mapper, ILogger<BookService> logger) : base(uow, mapper, logger)
        {
        }

        public async Task<BookDto> CreateBookAsync(CreateBookDto createBookDto, CancellationToken ct)
        {
            BookDto bookDto = null;
            try
            {
                var bookEntity = _mapper.Map<Book>(createBookDto);
                await _uow.Books.AddAsync(bookEntity, ct);
                await _uow.SaveChangesAsync(ct);
                bookDto = _mapper.Map<BookDto>(bookEntity);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, string.Empty);
            }
            return bookDto;
        }

        public async Task<BookDto> GetBookByIdAsync(Guid id, CancellationToken ct)
        {
            BookDto bookDto = null;

            if (_uow != null)
            {
                if (_uow.Books != null)
                {
                    var bookEntity = await _uow.Books.GetAsync(id, ct);
                    if (bookEntity != null)
                    {
                        bookDto = _mapper.Map<BookDto>(bookEntity);
                    }
                }
            }

            return bookDto;
        }
    }
}
