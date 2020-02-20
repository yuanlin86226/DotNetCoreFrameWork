using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Repositories.IRepositories {
    public interface ICheckinLogsRepository {
        Task CreateAsync (CheckinLogsModels CheckinLogs);
        Task<IEnumerable<CheckinLogsModels>> ReadAllAsync (string log);
        Task<UsersModels> FindUserID (string nameid);

    }
}