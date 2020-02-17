using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Repositories.IRepositories
{
    public interface IFunctionNamesRepository
    {
        Task CreateAsync(FunctionNamesModels FunctionNames);
        Task<IEnumerable<FunctionNamesModels>> ReadAllAsync(string function_name);
        Task<FunctionNamesModels> ReadOneAsync(int id);
        Task UpdateAsync(FunctionNamesModels FunctionNames);
        Task<Boolean> DeleteAsync(int id, FunctionNamesModels FunctionNames);

    }
}