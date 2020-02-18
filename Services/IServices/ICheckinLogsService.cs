using Models;
using Resources.Request;
using Resources.Response;
using Services.Communication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface ICheckinLogsService
    {
        Task<SaveCheckinLogsResponse> CreateAsync(InsertCheckinLogsResource resource);
        Task<IEnumerable<ActionsResource>> ReadAllAsync(string action);
    }
}