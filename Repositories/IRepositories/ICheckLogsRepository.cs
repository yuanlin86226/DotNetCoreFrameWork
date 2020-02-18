using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Repositories.IRepositories {
    public interface ICheckLogsRepository {
        Task CreateAsync (CheckinLogsModels CheckinLogs);
        Task<IEnumerable<ActionsModels>> ReadAllAsync (string actions);

    }
}