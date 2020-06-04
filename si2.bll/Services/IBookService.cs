using si2.bll.Dtos.Requests.Book;
using si2.bll.Dtos.Results.Book;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public interface IBookService : IServiceBase
    {
        Task<BookDto> CreateBookAsync(CreateBookDto createBookDto, CancellationToken ct);
        Task<BookDto> GetBookByIdAsync(Guid id, CancellationToken ct);
        
    }
}
