using Models;
using Resources.Request;
using Resources.Response;
using Services.Communication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IActionsService
    {
        Task<SaveActionsResponse> CreateAsync(InsertActionsResource resource);
        Task<IEnumerable<ActionsResource>> ReadAllAsync(string action);
        Task<ActionsResource> ReadOneAsync(int id);
        Task<SaveActionsResponse> UpdateAsync(int id, UpdateActionsResource Actions);
        Task<Boolean> DeleteAsync(int id);
    }
}