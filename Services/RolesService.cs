using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Models;
using Repositories.IRepositories;
using Resources.Request;
using Resources.Response;
using Services.Communication;
using Services.IServices;
using Utils;

namespace Services
{

    public class RolesService : IRolesService
    {
        private readonly IRolesRepository _RolesRepository;

        private readonly IRolePermissionsRepository _RolePermissionsRepository;
        private readonly IMapper _mapper;
        public RolesService(IRolesRepository RolesRepository, IRolePermissionsRepository RolePermissionsRepository, IMapper mapper)
        {
            this._RolesRepository = RolesRepository ??
                throw new ArgumentNullException(nameof(RolesRepository));

            this._RolePermissionsRepository = RolePermissionsRepository ??
                throw new ArgumentNullException(nameof(RolePermissionsRepository));

            this._mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SaveRolesResponse> CreateAsync(InsertRolesResource resource)
        {
            try
            {
                RolesModels Roles = _mapper.Map<InsertRolesResource, RolesModels>(resource);
                await _RolesRepository.CreateAsync(Roles);

                List<RolePermissionsModels> RolePermissions = new List<RolePermissionsModels>();

                for (int i = 1; i <= 4; i++)
                {
                    RolePermissions.Add(new RolePermissionsModels()
                    {
                        role_id = Roles.role_id,
                        permission_id = i,
                        create_user_id = Roles.create_user_id,
                        update_user_id = Roles.update_user_id,
                        create_time = DateTime.Now,
                        update_time = DateTime.Now
                    });
                }

                for (int i = 0; i < resource.permission_id.Length; i++)
                {
                    RolePermissions.Add(new RolePermissionsModels()
                    {
                        role_id = Roles.role_id,
                        permission_id = resource.permission_id[i],
                        create_time = DateTime.Now,
                        update_time = DateTime.Now
                    });
                }

                await _RolePermissionsRepository.CreateAsync(RolePermissions);

                return new SaveRolesResponse(true);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SaveRolesResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<IEnumerable<RolesResource>> ReadAllAsync(string role)
        {

            var RolePermissions = await _RolePermissionsRepository.ReadAllAsync(role);
            var Roles = await _RolesRepository.ReadAllAsync(role);
            RolesViewsUtils RolesFormat = new RolesViewsUtils();
            var RolesView = RolesFormat.RolestViews(Roles, RolePermissions);
            return RolesView;
        }


        public async Task<IEnumerable<RolesResource>> ReadOneAsync(int id)
        {
            var Roles = await _RolesRepository.ReadOneAsync(id);
            List<RolesModels> RolesList = new List<RolesModels>();
            RolesList.Add(new RolesModels()
            {
                role_id = Roles.role_id,
                role = Roles.role
            });

            var RolePermissions = await _RolePermissionsRepository.ReadAllAsync(Roles.role);
            RolesViewsUtils RolesFormat = new RolesViewsUtils();
            var RolesView = RolesFormat.RolestViews(RolesList, RolePermissions);
            return RolesView;
        }

        public async Task<SaveRolesResponse> UpdateAsync(int id, UpdateRolesResource resource)
        {
            try
            {
                var existingRoles = await _RolesRepository.ReadOneAsync(id);
                if (existingRoles == null)
                    return new SaveRolesResponse("Category not found.");

                var RolePermissions = await _RolePermissionsRepository.ReadAllAsync(existingRoles.role);

                existingRoles.role = resource.role;
                existingRoles.update_time = resource.update_time;
                existingRoles.update_user_id = resource.update_user_id;
                await _RolesRepository.UpdateAsync(existingRoles);

                await _RolePermissionsRepository.DeleteAsync(RolePermissions);

                List<RolePermissionsModels> NewRolePermissions = new List<RolePermissionsModels>();

                for (int i = 0; i < resource.permission_id.Length; i++)
                {
                    NewRolePermissions.Add(new RolePermissionsModels()
                    {
                        role_id = id,
                        permission_id = resource.permission_id[i],
                        create_user_id = resource.update_user_id,
                        update_user_id = resource.update_user_id,
                        create_time = DateTime.Now,
                        update_time = DateTime.Now
                    });
                }

                await _RolePermissionsRepository.CreateAsync(NewRolePermissions);

                return new SaveRolesResponse(true);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SaveRolesResponse($"An error occurred when updating the category: {ex.Message}");
            }
        }

        public async Task<Boolean> DeleteAsync(int id)
        {
            try
            {
                var Roles = await _RolesRepository.ReadOneAsync(id);

                if (Roles == null)
                    return false;

                var RolePermissions = await _RolePermissionsRepository.ReadAllAsync(Roles.role);

                Boolean RolePermissionsDeleteChecked = await _RolePermissionsRepository.DeleteAsync(RolePermissions);
                Boolean RolesDeleteChecked = await _RolesRepository.DeleteAsync(Roles);

                if (RolePermissionsDeleteChecked == true && RolesDeleteChecked == true)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

    }
}