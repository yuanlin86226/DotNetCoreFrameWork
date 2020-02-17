using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Resources.Response;

namespace Utils
{
    class RolesViewsUtils
    {
        /// <summary>
        /// 角色格式定義
        /// </summary>
        /// <param name="Roles"></param>
        /// <param name="RolePermissions"></param>
        /// <returns></returns>
        public List<RolesResource> RolestViews(IEnumerable<RolesModels> Roles, IEnumerable<RolePermissionsModels> RolePermissions)
        {
            List<RolesResource> RolesView = new List<RolesResource>();
            PermissionsResource[] PermissionsView;

            foreach (var r in Roles)
            {
                var FunctionNamesCount = (from p in RolePermissions
                                          where (p.role_id == r.role_id)
                                          select new { p.permissions.function_names }
                                         ).Distinct().ToList();

                PermissionsView = new PermissionsResource[FunctionNamesCount.Count()];

                int count = 0;

                foreach (var f in FunctionNamesCount)
                {
                    PermissionsView[count] = new PermissionsResource();
                    PermissionsView[count].actions = new List<ActionsResource>();

                    PermissionsView[count].function_names = new FunctionNamesResource();
                    PermissionsView[count].function_names.function_name_id = f.function_names.function_name_id;
                    PermissionsView[count].function_names.function_name = f.function_names.function_name;
                    PermissionsView[count].function_names.function_name_chinese = f.function_names.function_name_chinese;

                    var ActionsList = (from p in RolePermissions
                                       where (p.role_id == r.role_id && p.permissions.function_names.function_name == f.function_names.function_name.ToString())
                                       select new { p.permissions.actions }
                                      ).Distinct().ToList();

                    foreach (var a in ActionsList)
                    {
                        PermissionsView[count].actions.Add(new ActionsResource()
                        {
                            action_id = a.actions.action_id,
                            action = a.actions.action
                        });
                    }

                    count++;
                }

                RolesView.Add(new RolesResource()
                {
                    role_id = r.role_id,
                    role = r.role,
                    Permissions = PermissionsView,
                    create_time = r.create_time,
                    update_time = r.update_time
                });
            }

            return RolesView;
        }
    }
}