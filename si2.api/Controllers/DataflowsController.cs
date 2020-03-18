using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using si2.bll.Dtos.Requests.Dataflow;
using si2.bll.Dtos.Results.Dataflow;
using si2.bll.Helpers.ResourceParameters;
using si2.bll.Services;
using si2.common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace si2.api.Controllers
{
    [ApiController]
    [Route("api/dataflows")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]

    public class DataflowsController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<DataflowsController> _logger;
        private readonly IDataflowService _dataflowService;

        public DataflowsController(LinkGenerator linkGenerator, ILogger<DataflowsController> logger, IDataflowService dataflowService)
        {
            _linkGenerator = linkGenerator;
            _logger = logger;
            _dataflowService = dataflowService;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DataflowDto))]
        public async Task<ActionResult> CreateDataflow([FromBody] CreateDataflowDto createDataflowDto, CancellationToken ct)
        {
            var dataflowToReturn = await _dataflowService.CreateDataflowAsync(createDataflowDto, ct);
            if(dataflowToReturn == null)
                return BadRequest();

            return CreatedAtRoute("GetDataflow", new { id = dataflowToReturn.Id }, dataflowToReturn);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteDataflow(Guid id, CancellationToken ct)
        {
            await _dataflowService.DeleteDataflowByIdAsync(id, ct);
          
            return NoContent();
        }

        [HttpGet("{id}", Name = "GetDataflow")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataflowDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetDataflow(Guid id, CancellationToken ct)
        {
            var dataflowDto = await _dataflowService.GetDataflowByIdAsync(id, ct);

            if (dataflowDto == null)
                return NotFound();

            return Ok(dataflowDto);
        }

        [HttpGet(Name = "GetDataflows")]
        public async Task<ActionResult> GetDataflows([FromQuery]DataflowResourceParameters pagedResourceParameters, CancellationToken ct)
        {
            var dataflowDtos = await _dataflowService.GetDataflowsAsync(pagedResourceParameters, ct);

            var previousPageLink = dataflowDtos.HasPrevious ? CreateDataflowsResourceUri(pagedResourceParameters, Enums.ResourceUriType.PreviousPage) : null;
            var nextPageLink = dataflowDtos.HasNext ? CreateDataflowsResourceUri(pagedResourceParameters, Enums.ResourceUriType.NextPage) : null;

            var paginationMetadata = new
            {
                totalCount = dataflowDtos.TotalCount,
                pageSize = dataflowDtos.PageSize,
                currentPage = dataflowDtos.CurrentPage,
                totalPages = dataflowDtos.TotalPages,
                previousPageLink,
                nextPageLink
            };

            if (dataflowDtos == null)
                return NotFound();

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));
            return Ok(dataflowDtos);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataflowDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateDataflow([FromRoute]Guid id, [FromBody] UpdateDataflowDto updateDataflowDto, CancellationToken ct)
        {
            if (! await _dataflowService.ExistsAsync(id, ct))
                return NotFound();

            var dataflowToReturn = await _dataflowService.UpdateDataflowAsync(id, updateDataflowDto, ct);
            if (dataflowToReturn == null)
                return BadRequest();

            return Ok(dataflowToReturn);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateDataflow([FromRoute]Guid id, [FromBody] JsonPatchDocument<UpdateDataflowDto> patchDoc, CancellationToken ct)
        {
            if (! await _dataflowService.ExistsAsync(id, ct))
                return NotFound();

            var dataflowToPatch = await _dataflowService.GetUpdateDataFlowDto(id, ct);
            patchDoc.ApplyTo(dataflowToPatch, ModelState);

            TryValidateModel(dataflowToPatch);

            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);

            var dataflowToReturn = await _dataflowService.PartialUpdateDataflowAsync(id, dataflowToPatch, ct);
            if (dataflowToReturn == null)
                return BadRequest();

            return Ok(dataflowToReturn);
        }


        private string CreateDataflowsResourceUri(DataflowResourceParameters pagedResourceParameters, Enums.ResourceUriType type)
        {
            switch (type)
            {
                case Enums.ResourceUriType.PreviousPage:
                    return _linkGenerator.GetUriByName(this.HttpContext, "GetDataflows",
                        new
                        {
                            status = pagedResourceParameters.Status,
                            searchQuery = pagedResourceParameters.SearchQuery,
                            pageNumber = pagedResourceParameters.PageNumber - 1,
                            pageSize = pagedResourceParameters.PageSize
                        }); // TODO get the aboslute path 
                case Enums.ResourceUriType.NextPage:
                    return _linkGenerator.GetUriByName(this.HttpContext, "GetDataflows", 
                        new
                        {
                            status = pagedResourceParameters.Status,
                            searchQuery = pagedResourceParameters.SearchQuery,
                            pageNumber = pagedResourceParameters.PageNumber + 1,
                            pageSize = pagedResourceParameters.PageSize
                        });
                default:
                    return _linkGenerator.GetUriByName(this.HttpContext, "GetDataflows",
                       new
                       {
                           status = pagedResourceParameters.Status,
                           searchQuery = pagedResourceParameters.SearchQuery,
                           pageNumber = pagedResourceParameters.PageNumber,
                           pageSize = pagedResourceParameters.PageSize
                       });
            }
        }
    }
}
