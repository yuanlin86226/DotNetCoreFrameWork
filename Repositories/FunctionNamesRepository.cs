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
    public class FunctionNamesRepository : BaseRepository, IFunctionNamesRepository
    {

        public FunctionNamesRepository(CustomContext context) : base(context)
        {

        }

        public async Task CreateAsync(FunctionNamesModels FunctionNames)
        {
            await _context.function_names.AddAsync(FunctionNames);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FunctionNamesModels>> ReadAllAsync(string function_name)
        {
            var FunctionNames = from f in _context.function_names
                                select f;

            if(!string.IsNullOrEmpty(function_name))
                FunctionNames = FunctionNames.Where(f => f.function_name == function_name);
            
            FunctionNames = FunctionNames.Include(c => c.create_user)
                                         .Include(u => u.update_user);

            return await FunctionNames.ToListAsync();;
        }

        public async Task<FunctionNamesModels> ReadOneAsync(int id)
        {
            FunctionNamesModels FunctionNames = await _context.function_names
                .Include(c => c.create_user)
                .Include(u => u.update_user)
                .SingleOrDefaultAsync(a => a.function_name_id == id);

            return FunctionNames;
        }

        public async Task UpdateAsync(FunctionNamesModels FunctionNames)
        {
            _context.function_names.Update(FunctionNames);
            await _context.SaveChangesAsync();
        }

        public async Task<Boolean> DeleteAsync(int id, FunctionNamesModels FunctionNames)
        {
            _context.function_names.Remove(FunctionNames);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}