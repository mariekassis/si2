using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using si2.bll.Dtos.Requests.University;
using si2.bll.Dtos.Results.University;
using si2.bll.Helpers.ResourceParameters;
using si2.bll.Services;
using si2.common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace si2.api.Controllers
{
	[ApiController]
	[Route("api/universities")]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]

	public class UniversitiesController : ControllerBase
	{
		private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<UniversitiesController> _logger;
        private readonly IUniversityService _universityService;

        public UniversitiesController(LinkGenerator linkGenerator, ILogger<UniversitiesController> logger, IUniversityService universityService)
		{
            _linkGenerator = linkGenerator;
            _logger = logger;
            _universityService = universityService;
		}

        /*[HttpGet("{id}", Name = "GetUniversity")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UniversityDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetUniversity(Guid id, CancellationToken ct)
        {
            var universityDto = await _universityService.GetUniversityByIdAsync(id, ct);

            if (universityDto == null)
                return NotFound();

            return Ok(universityDto);

        }*/

        [HttpGet("{id}", Name = "GetUniversity")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UniversityDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetUniversity(Guid id, CancellationToken ct)
        {
            var universityDto = await _universityService.GetUniversityByIdAsync(id, ct);

            if (universityDto == null)
                return NotFound();

            return Ok(universityDto);

            //return Ok("Reached");
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UniversityDto))]
        public async Task<ActionResult> CreateUniversity([FromBody] CreateUniversityDto createUniversityDto, CancellationToken ct)
        {
            var universityToReturn = await _universityService.CreateUniversityAsync(createUniversityDto, ct);
            if (universityToReturn == null)
                return BadRequest();

            return CreatedAtRoute("GetUniversity", new { id = universityToReturn.Id }, universityToReturn);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteUniversity(Guid id, CancellationToken ct)
        {
            await _universityService.DeleteUniversityByIdAsync(id, ct);

            return NoContent();
        }


        [HttpGet(Name = "GetUniversities")]
        public async Task<ActionResult> GetUniversities([FromQuery]UniversityResourceParameters pagedResourceParameters, CancellationToken ct)
        {
            var universityDtos = await _universityService.GetUniversitiesAsync(pagedResourceParameters, ct);

            var previousPageLink = universityDtos.HasPrevious ? CreateUniversitiesResourceUri(pagedResourceParameters, Enums.ResourceUriType.PreviousPage) : null;
            var nextPageLink = universityDtos.HasNext ? CreateUniversitiesResourceUri(pagedResourceParameters, Enums.ResourceUriType.NextPage) : null;

            var paginationMetadata = new
            {
                totalCount = universityDtos.TotalCount,
                pageSize = universityDtos.PageSize,
                currentPage = universityDtos.CurrentPage,
                totalPages = universityDtos.TotalPages,
                previousPageLink,
                nextPageLink
            };

            if (universityDtos == null)
                return NotFound();

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));
            return Ok(universityDtos);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UniversityDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateUniversity([FromRoute]Guid id, [FromBody] UpdateUniversityDto updateUniversityDto, CancellationToken ct)
        {
            if (!await _universityService.ExistsAsync(id, ct))
                return NotFound();

            var universityToReturn = await _universityService.UpdateUniversityAsync(id, updateUniversityDto, ct);
            if (universityToReturn == null)
                return BadRequest();

            return Ok(universityToReturn);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateUniversity([FromRoute]Guid id, [FromBody] JsonPatchDocument<UpdateUniversityDto> patchDoc, CancellationToken ct)
        {
            if (!await _universityService.ExistsAsync(id, ct))
                return NotFound();

            var universityToPatch = await _universityService.GetUpdateUniversityDto(id, ct);
            patchDoc.ApplyTo(universityToPatch, ModelState);

            TryValidateModel(universityToPatch);

            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);

            var universityToReturn = await _universityService.PartialUpdateUniversityAsync(id, universityToPatch, ct);
            if (universityToReturn == null)
                return BadRequest();

            return Ok(universityToReturn);
        }


        private string CreateUniversitiesResourceUri(UniversityResourceParameters pagedResourceParameters, Enums.ResourceUriType type)
        {
            switch (type)
            {
                case Enums.ResourceUriType.PreviousPage:
                    return _linkGenerator.GetUriByName(this.HttpContext, "GetUniversities",
                        new
                        {
                            /*name = pagedResourceParameters.Name*/
                            searchQuery = pagedResourceParameters.SearchQuery,
                            pageNumber = pagedResourceParameters.PageNumber - 1,
                            pageSize = pagedResourceParameters.PageSize
                        }); // TODO get the aboslute path 
                case Enums.ResourceUriType.NextPage:
                    return _linkGenerator.GetUriByName(this.HttpContext, "GetUniversities",
                        new
                        {
                            /*name = pagedResourceParameters.Name,*/
                            searchQuery = pagedResourceParameters.SearchQuery,
                            pageNumber = pagedResourceParameters.PageNumber + 1,
                            pageSize = pagedResourceParameters.PageSize
                        });
                default:
                    return _linkGenerator.GetUriByName(this.HttpContext, "GetUniversities",
                       new
                       {
                           /*name = pagedResourceParameters.Name,*/
                           searchQuery = pagedResourceParameters.SearchQuery,
                           pageNumber = pagedResourceParameters.PageNumber,
                           pageSize = pagedResourceParameters.PageSize
                       });
            }
        }

}
}
