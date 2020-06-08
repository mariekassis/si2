using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using si2.bll.Services;
using si2.bll.Dtos.Requests.Book;
using si2.bll.Dtos.Results.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper;
using si2.dal.Entities;
using si2.bll.ResourceParameters;
using si2.common;
using Newtonsoft.Json;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json.Linq;

namespace si2.api.Controllers
{
    [ApiController]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]

    public class BooksController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<BooksController> _logger;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private readonly IBookCategoryService _bookCategoryService;

        public BooksController(LinkGenerator linkGenerator, ILogger<BooksController> logger, IBookService bookService, IBookCategoryService bookCategoryService)
        {
            _linkGenerator = linkGenerator;
            _logger = logger;
            _bookService = bookService;
            _bookCategoryService = bookCategoryService;
        }

        [Route("api/books")]
        [HttpGet("{id}", Name = "GetBook")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetBook(Guid id, CancellationToken ct)
        {
            var bookDto = await _bookService.GetBookByIdAsync(id, ct);

            if (bookDto == null)
                return NotFound();

            return Ok(bookDto);

            //return Ok("Reached");
            //return Ok("Reached");
        }

        [Route("api/books")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookDto))]
        public async Task<ActionResult> CreateBook([FromBody] CreateBookDto createBookDto, CancellationToken ct)
        {
            var bookToReturn = await _bookService.CreateBookAsync(createBookDto, ct);
            if (bookToReturn == null)
                return BadRequest();

            return CreatedAtRoute("GetBook", new { id = bookToReturn.Id }, bookToReturn);
        }

        [Route("api/books/{bookId}/categories")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookDto))]
        public async Task<ActionResult> AddCategoriesToBook(Guid bookId, [FromBody] JArray categories, CancellationToken ct)
        {
            foreach (JObject categoryToAdd in categories)
            {
                var bookToReturn = await _bookCategoryService.CreateBookCategoryAsync(bookId, new Guid(categoryToAdd.GetValue("categoryId").ToString()), ct);
                if (bookToReturn == null)
                    return BadRequest();
            }

            return Ok();
        }

        [Route("api/books/{bookId}/categories")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookDto))]
        public async Task<ActionResult> GetCategoriesOfBook(Guid bookId, CancellationToken ct)
        {
            await _bookCategoryService.GetBookCategoryByIdAsync(bookId, ct);

            return Ok();
        }

    }
}
