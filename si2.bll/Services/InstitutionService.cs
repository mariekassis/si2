using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.Institution;
using si2.bll.Dtos.Results.Institution;
using si2.bll.Helpers.PagedList;
using si2.bll.ResourceParameters;
using si2.dal.Entities;
using si2.dal.UnitOfWork;

namespace si2.bll.Services
{
    public class InstitutionService : ServiceBase, IInstitutionService
    {
        
        public InstitutionService(IUnitOfWork uow, IMapper mapper, ILogger<InstitutionService> logger) : base(uow, mapper, logger)
        {
        }

        public async Task<InstitutionDto> CreateInstitutionAsync(Guid universityId, CreateInstitutionDto createInstitutionDto, CancellationToken ct)
        {
            InstitutionDto institutionDto = null;
            try
            {
                var institutionEntity = _mapper.Map<Institution>(createInstitutionDto);
                institutionEntity.universityId = universityId;
                await _uow.Institutions.AddAsync(institutionEntity, ct);
                await _uow.SaveChangesAsync(ct);
                institutionDto = _mapper.Map<InstitutionDto>(institutionEntity);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, string.Empty);
            }
            return institutionDto;
        }

        public async Task<InstitutionDto> UpdateInstitutionAsync(Guid id, UpdateInstitutionDto updateInstitutionDto, CancellationToken ct)
        {
            InstitutionDto institutionDto = null;

            var updatedEntity = _mapper.Map<Institution>(updateInstitutionDto);
            updatedEntity.Id = id;
            await _uow.Institutions.UpdateAsync(updatedEntity, id, ct, updatedEntity.RowVersion);
            await _uow.SaveChangesAsync(ct);
            var institutionEntity = await _uow.Institutions.GetAsync(id, ct);
            institutionDto = _mapper.Map<InstitutionDto>(institutionEntity);

            return institutionDto;
        }

        public async Task<InstitutionDto> PartialUpdateInstitutionAsync(Guid id, UpdateInstitutionDto updateInstitutionDto, CancellationToken ct)
        {
            var institutionEntity = await _uow.Institutions.GetAsync(id, ct);

            _mapper.Map(updateInstitutionDto, institutionEntity);

            await _uow.Institutions.UpdateAsync(institutionEntity, id, ct, institutionEntity.RowVersion);
            await _uow.SaveChangesAsync(ct);

            institutionEntity = await _uow.Institutions.GetAsync(id, ct);
            var institutionDto = _mapper.Map<InstitutionDto>(institutionEntity);

            return institutionDto;
        }

        public async Task<UpdateInstitutionDto> GetUpdateInstitutionDto(Guid id, CancellationToken ct)
        {
            var institutionEntity = await _uow.Institutions.GetAsync(id, ct);
            var updateInstitutionDto = _mapper.Map<UpdateInstitutionDto>(institutionEntity);
            return updateInstitutionDto;
        }

        public async Task<InstitutionDto> GetInstitutionByIdAsync(Guid id, CancellationToken ct)
        {
            InstitutionDto institutionDto = null;

            var institutionEntity = await _uow.Institutions.GetAsync(id, ct);
            if (institutionEntity != null)
            {
                institutionDto = _mapper.Map<InstitutionDto>(institutionEntity);
            }

            return institutionDto;
        }

        public async Task DeleteInstitutionByIdAsync(Guid id, CancellationToken ct)
        {
            try
            {
                var institutionEntity = await _uow.Institutions.FirstAsync(c => c.Id == id, ct);
                await _uow.Institutions.DeleteAsync(institutionEntity, ct);
                await _uow.SaveChangesAsync(ct);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError(e, string.Empty);
            }
        }

        public async Task<PagedList<InstitutionDto>> GetInstitutionsAsync(InstitutionResourceParameters resourceParameters, CancellationToken ct)
        {
            var institutionEntities = _uow.Institutions.GetAll();

            /*if (!string.IsNullOrEmpty(resourceParameters.Name))
            {
                if (Enum.TryParse(resourceParameters.Name, true, out University name))
                {
                    institutionEntities = institutionEntities.Where(a => a.Name == name);
                }
            }*/

            if (!string.IsNullOrEmpty(resourceParameters.SearchQuery))
            {
                var searchQueryForWhereClause = resourceParameters.SearchQuery.Trim().ToLowerInvariant();
                institutionEntities = institutionEntities
                    .Where(a => a.Name.ToLowerInvariant().Contains(searchQueryForWhereClause)
                            );
            }

            var pagedListEntities = await PagedList<Institution>.CreateAsync(institutionEntities,
                resourceParameters.PageNumber, resourceParameters.PageSize, ct);

            var result = _mapper.Map<PagedList<InstitutionDto>>(pagedListEntities);
            result.TotalCount = pagedListEntities.TotalCount;
            result.TotalPages = pagedListEntities.TotalPages;
            result.CurrentPage = pagedListEntities.CurrentPage;
            result.PageSize = pagedListEntities.PageSize;

            return result;
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken ct)
        {
            if (await _uow.Institutions.GetAsync(id, ct) != null)
                return true;

            return false;
        }
    }
    
}
