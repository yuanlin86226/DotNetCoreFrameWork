using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Repositories.IRepositories;
using Services.Communication;
using Services.IServices;

namespace Services
{

    public class PermissionsService : IPermissionsService
    {
        private readonly IPermissionsRepository _PermissionsRepository;

        public PermissionsService(IPermissionsRepository PermissionsRepository)
        {
            this._PermissionsRepository = PermissionsRepository ??
                throw new ArgumentNullException(nameof(PermissionsRepository));
        }

        public async Task<SavePermissionsResponse> CreateAsync(PermissionsModels Permissions)
        {
            try
            {
                await _PermissionsRepository.CreateAsync(Permissions);
                return new SavePermissionsResponse(Permissions);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SavePermissionsResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<IEnumerable<PermissionsModels>> ReadAllAsync(string function_name)
        {
            return await _PermissionsRepository.ReadAllAsync(function_name);
        }

        public async Task<PermissionsModels> ReadOneAsync(int id)
        {
            var Permissions = await _PermissionsRepository.ReadOneAsync(id);

            return (PermissionsModels)Permissions;
        }

        public async Task<SavePermissionsResponse> UpdateAsync(int id, PermissionsModels Permissions)
        {
            var existingPermissions = await _PermissionsRepository.ReadOneAsync(id);
            if (existingPermissions == null)
                return new SavePermissionsResponse("Category not found.");

            existingPermissions.function_name_id = Permissions.function_name_id;
            existingPermissions.action_id = Permissions.action_id;
            existingPermissions.update_user_id = Permissions.update_user_id;
            existingPermissions.update_time = Permissions.update_time;

            try
            {
                await _PermissionsRepository.UpdateAsync(existingPermissions);

                return new SavePermissionsResponse(existingPermissions);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SavePermissionsResponse($"An error occurred when updating the category: {ex.Message}");
            }
        }

        public async Task<Boolean> DeleteAsync(int id)
        {
            var Permissions = await _PermissionsRepository.ReadOneAsync(id);
            if (Permissions == null)
                return false;
            return await _PermissionsRepository.DeleteAsync(id, Permissions);
        }

    }
}