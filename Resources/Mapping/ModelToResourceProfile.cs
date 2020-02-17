using AutoMapper;
using Models;
using Resources.Request;
using Resources.Response;

namespace Resources.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            #region  Actions
            CreateMap<ActionsModels, ActionsResource>();
            CreateMap<InsertActionsResource, ActionsModels>();
            CreateMap<UpdateActionsResource, ActionsModels>();
            #endregion

            #region  FunctionNames
            CreateMap<FunctionNamesModels, FunctionNamesResource>();
            CreateMap<InsertFunctionNamesResource, FunctionNamesModels>();
            CreateMap<UpdateFunctionNamesResource, FunctionNamesModels>();
            #endregion


            #region Permissions
            CreateMap<PermissionsModels, PermissionsResource>();
            CreateMap<InsertPermissionsResource, PermissionsModels>();
            CreateMap<UpdatePermissionsResource, PermissionsModels>();
            #endregion

            #region Roles
            CreateMap<RolesModels, RolesResource>();
            CreateMap<InsertRolesResource, RolesModels>();
            CreateMap<UpdateRolesResource, RolesModels>();
            #endregion

            #region Users
            CreateMap<UsersModels, UsersResource>();
            CreateMap<InsertUsersResource, UsersModels>();
            CreateMap<UpdateUsersResource, UsersModels>();
            #endregion
        }
    }
}