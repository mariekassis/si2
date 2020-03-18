using Microsoft.AspNetCore.JsonPatch;
using si2.bll.Dtos.Requests.Dataflow;
using si2.bll.Dtos.Results.Dataflow;
using si2.bll.Helpers.PagedList;
using si2.bll.Helpers.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public interface IDataflowService : IServiceBase
    {
        Task<DataflowDto> CreateDataflowAsync(CreateDataflowDto createDataflowDto, CancellationToken ct);
        Task<DataflowDto> UpdateDataflowAsync(Guid id, UpdateDataflowDto updateDataflowDto, CancellationToken ct);
        Task<DataflowDto> PartialUpdateDataflowAsync(Guid id, UpdateDataflowDto patchDoc, CancellationToken ct);
        Task<UpdateDataflowDto> GetUpdateDataFlowDto(Guid id, CancellationToken ct);
        Task<DataflowDto> GetDataflowByIdAsync(Guid id, CancellationToken ct);
        Task DeleteDataflowByIdAsync(Guid id, CancellationToken ct);
        Task<PagedList<DataflowDto>> GetDataflowsAsync (DataflowResourceParameters pagedResourceParameters, CancellationToken ct);
        Task<bool> ExistsAsync(Guid id, CancellationToken ct);
    }
}
