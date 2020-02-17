using Resources.Request;
using Resources.Response;
using Services.Communication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IUsersService
    {
        Task<SaveUsersResponse> CreateAsync(InsertUsersResource Users);
        Task<IEnumerable<UsersResource>> ReadAllAsync(string account_number,string user_name);
        Task<UsersResource> ReadOneAsync(string id);
        Task<SaveUsersResponse> UpdateAsync(string id, UpdateUsersResource Users);
        Task<Boolean> DeleteAsync(string id);
    }
}