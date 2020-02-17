using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Repositories.IRepositories
{

    public interface IUsersRepository
    {
        Task CreateAsync(UsersModels Users);
        Task<IEnumerable<UsersModels>> ReadAllAsync(string account_number, string password, string user_name);
        Task<UsersModels> ReadOneAsync(string id);
        Task<UsersModels> ReadOneAsync(string account_number, string password);
        Task UpdateAsync(UsersModels TableNumbers);
        Task<Boolean> DeleteAsync(UsersModels TableNumbers);
    }
}