using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Context;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.IRepositories;
using Repositories.Persistence;

namespace Repositories
{
    public class CheckinLogsRepository : BaseRepository, ICheckinLogsRepository
    {

        public CheckinLogsRepository(CustomContext context) : base(context)
        {

        }

        public async Task CreateAsync(CheckinLogsModels CheckinLogs)
        {
            await _context.checkin_logs.AddAsync(CheckinLogs);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CheckinLogsModels>> ReadAllAsync(string log)
        {
            var Logs = _context.checkin_logs.Include(p => p.users)
                                .ToListAsync();            
                             
            return await Logs;
        }

        public async Task<UsersModels> FindUserID(string nameid)
        {
            return await _context.users.Where(p => p.account_number == nameid).FirstAsync();
        }
    }
}