using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using si2.bll.Dtos.Requests.Institution;
using si2.bll.Dtos.Results.Institution;
using si2.bll.Helpers.PagedList;
using si2.bll.ResourceParameters;

namespace si2.bll.Services
{
    public interface IInstitutionService : IServiceBase
    {
        Task<InstitutionDto> CreateInstitutionAsync(Guid universityId, CreateInstitutionDto createInstitutionDto, CancellationToken ct);
        Task<InstitutionDto> UpdateInstitutionAsync(Guid id, UpdateInstitutionDto updateInstitutionDto, CancellationToken ct);
        Task<InstitutionDto> PartialUpdateInstitutionAsync(Guid id, UpdateInstitutionDto patchDoc, CancellationToken ct);
        Task<UpdateInstitutionDto> GetUpdateInstitutionDto(Guid id, CancellationToken ct);
        Task<InstitutionDto> GetInstitutionByIdAsync(Guid id, CancellationToken ct);
        Task DeleteInstitutionByIdAsync(Guid id, CancellationToken ct);
        Task<PagedList<InstitutionDto>> GetInstitutionsAsync(InstitutionResourceParameters pagedResourceParameters, CancellationToken ct);
        Task<bool> ExistsAsync(Guid id, CancellationToken ct);
    }
}
