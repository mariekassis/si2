using Microsoft.AspNetCore.JsonPatch;
using si2.bll.Dtos.Requests.University;
using si2.bll.Dtos.Results.University; 
using si2.bll.Helpers.PagedList;
using si2.bll.Helpers.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public interface IUniversityService : IServiceBase
    {
        Task<UniversityDto> CreateUniversityAsync(CreateUniversityDto createUniversityDto, CancellationToken ct);
        Task<UniversityDto> UpdateUniversityAsync(Guid id, UpdateUniversityDto updateUniversityDto, CancellationToken ct);
        Task<UniversityDto> PartialUpdateUniversityAsync(Guid id, UpdateUniversityDto patchDoc, CancellationToken ct);
        Task<UpdateUniversityDto> GetUpdateUniversityDto(Guid id, CancellationToken ct);
        Task<UniversityDto> GetUniversityByIdAsync(Guid id, CancellationToken ct);
        Task DeleteUniversityByIdAsync(Guid id, CancellationToken ct);
        Task<PagedList<UniversityDto>> GetUniversitiesAsync(UniversityResourceParameters pagedResourceParameters, CancellationToken ct);
        Task<bool> ExistsAsync(Guid id, CancellationToken ct);
    }
}
