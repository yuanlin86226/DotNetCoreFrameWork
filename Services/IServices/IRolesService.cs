using Models;
using Resources.Request;
using Resources.Response;
using Services.Communication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IRolesService
    {
        Task<SaveRolesResponse> CreateAsync(InsertRolesResource resource);
        Task<IEnumerable<RolesResource>> ReadAllAsync(string role);
        Task<IEnumerable<RolesResource>> ReadOneAsync(int id);
        Task<SaveRolesResponse> UpdateAsync(int id, UpdateRolesResource Roles);
        Task<Boolean> DeleteAsync(int id);
    }
}