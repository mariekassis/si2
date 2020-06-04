using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using si2.bll.Services;
using si2.bll.Dtos.Requests.Category;
using si2.bll.Dtos.Results.Category;
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

namespace si2.api.Controllers
{
    [ApiController]
    [Route("api/categories")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]

    public class CategoriesController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<CategoriesController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(LinkGenerator linkGenerator, ILogger<CategoriesController> logger, ICategoryService categoryService)
        {
            _linkGenerator = linkGenerator;
            _logger = logger;
            _categoryService = categoryService;
        }


        [HttpGet("{id}", Name = "GetCategory")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetCategory(Guid id, CancellationToken ct)
        {
            var categoryDto = await _categoryService.GetCategoryByIdAsync(id, ct);

            if (categoryDto == null)
                return NotFound();

            return Ok(categoryDto);

            //return Ok("Reached");
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CategoryDto))]
        public async Task<ActionResult> CreateCategory([FromBody] CreateCategoryDto createCategoryDto, CancellationToken ct)
        {
            var categoryToReturn = await _categoryService.CreateCategoryAsync(createCategoryDto, ct);
            if (categoryToReturn == null)
                return BadRequest();

            return CreatedAtRoute("GetCategory", new { id = categoryToReturn.Id }, categoryToReturn);
        }

    }
}
