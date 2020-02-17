using Models;
using Resources.Request;
using Resources.Response;
using Services.Communication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IFunctionNamesService
    {
        Task<SaveFunctionNamesResponse> CreateAsync(InsertFunctionNamesResource FunctionNames);
        Task<IEnumerable<FunctionNamesResource>> ReadAllAsync(string function_name);
        Task<FunctionNamesResource> ReadOneAsync(int id);
        Task<SaveFunctionNamesResponse> UpdateAsync(int id, UpdateFunctionNamesResource FunctionNames);
        Task<Boolean> DeleteAsync(int id);
    }
}