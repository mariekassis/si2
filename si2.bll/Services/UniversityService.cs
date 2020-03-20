using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.University;
using si2.bll.Dtos.Results.University;
using si2.bll.Helpers.PagedList;
using si2.bll.Helpers.ResourceParameters;
using si2.dal.Entities;
using si2.dal.UnitOfWork;
using Si2.common.Exceptions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static si2.common.Enums;

namespace si2.bll.Services
{
    public class UniversityService : ServiceBase, IUniversityService
    {
        public UniversityService(IUnitOfWork uow, IMapper mapper, ILogger<UniversityService> logger) : base(uow, mapper, logger)
        {
        }

        public async Task<UniversityDto> CreateUniversityAsync(CreateUniversityDto createUniversityDto, CancellationToken ct)
        {
            UniversityDto universityDto = null;
            try
            {
                var universityEntity = _mapper.Map<University>(createUniversityDto);
                await _uow.Universities.AddAsync(universityEntity, ct);
                await _uow.SaveChangesAsync(ct);
                universityDto = _mapper.Map<UniversityDto>(universityEntity);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, string.Empty);
            }
            return universityDto;
        }

        public async Task<UniversityDto> UpdateUniversityAsync(Guid id, UpdateUniversityDto updateUniversityDto, CancellationToken ct)
        {
            UniversityDto universityDto = null;
            
            var updatedEntity = _mapper.Map<University>(updateUniversityDto);
            updatedEntity.Id = id;
            await _uow.Universities.UpdateAsync(updatedEntity, id, ct, updatedEntity.RowVersion);
            await _uow.SaveChangesAsync(ct);
            var universityEntity = await _uow.Universities.GetAsync(id, ct);
            universityDto = _mapper.Map<UniversityDto>(universityEntity);
            
            return universityDto;
        }

        public async Task<UniversityDto> PartialUpdateUniversityAsync(Guid id, UpdateUniversityDto updateUniversityDto, CancellationToken ct)
        {
            var universityEntity = await _uow.Universities.GetAsync(id, ct);

            _mapper.Map(updateUniversityDto, universityEntity);

            await _uow.Universities.UpdateAsync(universityEntity, id, ct, universityEntity.RowVersion);
            await _uow.SaveChangesAsync(ct);

            universityEntity = await _uow.Universities.GetAsync(id, ct);
            var universityDto = _mapper.Map<UniversityDto>(universityEntity);

            return universityDto;
        }

        public async Task<UpdateUniversityDto> GetUpdateUniversityDto(Guid id, CancellationToken ct)
        {
            var universityEntity = await _uow.Universities.GetAsync(id, ct);
            var updateUniversityDto = _mapper.Map<UpdateUniversityDto>(universityEntity);
            return updateUniversityDto;
        }

        public async Task<UniversityDto> GetUniversityByIdAsync(Guid id, CancellationToken ct)
        {
            UniversityDto universityDto = null;

            var universityEntity = await _uow.Universities.GetAsync(id, ct);
            if (universityEntity != null)
            {
                universityDto = _mapper.Map<UniversityDto>(universityEntity);
            }

            return universityDto;
        }

        public async Task DeleteUniversityByIdAsync(Guid id, CancellationToken ct)
        {
            try
            {
                var universityEntity = await _uow.Universities.FirstAsync(c => c.Id == id, ct);
                await _uow.Universities.DeleteAsync(universityEntity, ct);
                await _uow.SaveChangesAsync(ct);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError(e, string.Empty);
            }
        }

        public async Task<PagedList<UniversityDto>> GetUniversitiesAsync(UniversityResourceParameters resourceParameters, CancellationToken ct)
        {
            var universityEntities = _uow.Universities.GetAll();

            /*if (!string.IsNullOrEmpty(resourceParameters.Name))
            {
                if (Enum.TryParse(resourceParameters.Name, true, out UniversityName name))
                {
                    universityEntities = universityEntities.Where(a => a.Name == name);
                }
            }*/

            if (!string.IsNullOrEmpty(resourceParameters.SearchQuery))
            {
                var searchQueryForWhereClause = resourceParameters.SearchQuery.Trim().ToLowerInvariant();
                universityEntities = universityEntities
                    .Where(a => a.Name.ToLowerInvariant().Contains(searchQueryForWhereClause)
                         );
            }

            var pagedListEntities = await PagedList<University>.CreateAsync(universityEntities,
                resourceParameters.PageNumber, resourceParameters.PageSize, ct);

            var result = _mapper.Map<PagedList<UniversityDto>>(pagedListEntities);
            result.TotalCount = pagedListEntities.TotalCount;
            result.TotalPages = pagedListEntities.TotalPages;
            result.CurrentPage = pagedListEntities.CurrentPage;
            result.PageSize = pagedListEntities.PageSize;

            return result;
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken ct)
        {
            if (await _uow.Universities.GetAsync(id, ct) != null)
                return true;
            
            return false;
        }
    }
}
