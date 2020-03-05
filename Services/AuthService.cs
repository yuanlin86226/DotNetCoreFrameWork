using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories.IRepositories;
using Resources.Request;
using Resources.Response;
using Services.IServices;
using Utils;

namespace Services
{

    public class AuthService : IAuthService
    {
        private readonly IUsersRepository _UsersRepository;
        private readonly IRolePermissionsRepository _RolePermissionsRepository;
        private readonly IConfiguration _config;

        public AuthService(IUsersRepository UsersRepository, IRolePermissionsRepository RolePermissionsRepository, IConfiguration configuration)
        {
            this._UsersRepository = UsersRepository ??
                throw new ArgumentNullException(nameof(UsersRepository));

            this._RolePermissionsRepository = RolePermissionsRepository ??
                throw new ArgumentNullException(nameof(RolePermissionsRepository));

            this._config = configuration ??
                throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<LoginOutputResource> LoginAsync(LoginResource resource)
        {
            LoginOutputResource Result = new LoginOutputResource();

            //將密碼加密
            MD5HashUtils MD5 = new MD5HashUtils();
            string Md5Password = MD5.MD5Hash(resource.password);

            //比對這組帳號密碼是否有人存在
            var Users = await _UsersRepository.ReadOneAsync(resource.account_number, Md5Password);

            if (Users != null)
            {
                //撈取該帳號的權限
                var RolePermissions = await _RolePermissionsRepository.ReadAllAsync(Users.roles.role);

                var FunctionNamesCount = (from r in RolePermissions
                                          where (r.role_id == Users.role_id)
                                          select new { r.permissions.function_names }
                                         ).Distinct().ToList();

                Result.user_id = Users.user_id;
                Result.user_name = Users.user_name;
                Result.role = Users.roles.role;

                Result.Permissions = new PermissionsResource[FunctionNamesCount.Count()];
                int count = 0;

                foreach (var f in FunctionNamesCount)
                {
                    Result.Permissions[count] = new PermissionsResource();
                    Result.Permissions[count].function_names = new FunctionNamesResource();
                    Result.Permissions[count].actions = new List<ActionsResource>();

                    Result.Permissions[count].function_names.function_name_id = f.function_names.function_name_id;
                    Result.Permissions[count].function_names.function_name = f.function_names.function_name;
                    Result.Permissions[count].function_names.function_name_chinese = f.function_names.function_name_chinese;

                    var ActionsList = (from r in RolePermissions
                                       where (r.role_id == Users.role_id && r.permissions.function_names.function_name == f.function_names.function_name.ToString())
                                       select new { r.permissions.actions }
                                      ).Distinct().ToList();

                    foreach (var a in ActionsList)
                    {
                        Result.Permissions[count].actions.Add(new ActionsResource()
                        {
                            action_id = a.actions.action_id,
                            action = a.actions.action
                        });
                    }


                    count++;
                }

                var userClaims = new ClaimsIdentity(new[] {
                    //使用者識別碼
                    new Claim(JwtRegisteredClaimNames.NameId, resource.account_number),
                    //JWT的唯一ID，防止JWT重複使用
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("Role", Users.roles.role)
                });

                // 取得對稱式加密 JWT Signature 的金鑰
                // 這部分是選用，但此範例在 Startup.cs 中有設定 ValidateIssuerSign ingKey = true 所以這裡必填
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                //建立 JWT TokenHandler 以及用於描述 JWT 的 TokenDescriptor
                var tokenHandler = new JwtSecurityTokenHandler();


                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _config["Jwt:Issuer"],
                    Audience = _config["Jwt:Issuer"],
                    Subject = userClaims,
                    // Expires = DateTime.Now.AddMinutes(30), //到期時間
                    SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                };


                // 產出所需要的 JWT Token 物件
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                // 產出序列化的 JWT Token 字串
                var serializeToken = tokenHandler.WriteToken(securityToken);

                Result.JWTKey = serializeToken;
            }

            return Result;
        }


    }
}