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
    }
}
