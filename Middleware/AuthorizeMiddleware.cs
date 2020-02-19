using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Middleware
{
    public class AuthorizeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public AuthorizeMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            this._next = next ??
                throw new ArgumentNullException(nameof(next));

            this._config = configuration ??
                throw new ArgumentNullException(nameof(configuration));
        }

        //  {
        //    "nameid": "admin", 帳號
        //    "jti": "2069aa29-d18f-4e1d-8893-8f910a2c656b", jwt的唯一身份標識，主要用來作為一次性token,從而迴避重放攻擊
        //    "ROle": "管理者", 角色
        //    "nbf": 1575292627, 定義在什麼時間之前，該jwt都是不可用的
        //    "exp": 1575294427, 到期時間
        //    "iat": 1575292627, 簽發時間
        //    "iss": "http://www.facebook.com",
        //    "aud": "http://www.facebook.com"
        //  }
        public async Task InvokeAsync(HttpContext context, CustomContext DBcontext)
        {
            //設定HttpRequest與HttpResponse
            var Request = context.Request;
            var Response = context.Response;

            //抓取路徑
            string Path = Request.Path.Value;

            //抓取傳送方式，ex：GET、POST、DELETE、PUT
            string URLMethod = Request.Method;

            //抓取Token 
            string Token = Request.Headers["Authorization"];

            //加密金鑰
            string secret = this._config["Jwt:Key"];
            var hs256 = new HMACSHA256(Encoding.ASCII.GetBytes(secret));

            //判斷路徑是否需要做Token驗證
            if (RouteChecked(Path, URLMethod))
            {
                //判斷Token是否存在
                if (Token == "" || string.IsNullOrEmpty(Token))
                {
                    await BadResponse(Response);
                }
                else
                {
                    try
                    {
                        //讀取Token的Header與PayLoad
                        var jwtArr = Token.Split('.');
                        var Header = JsonConvert.DeserializeObject<Dictionary<string, object>>(Base64UrlEncoder.Decode(jwtArr[0]));
                        var PayLoad = JsonConvert.DeserializeObject<Dictionary<string, object>>(Base64UrlEncoder.Decode(jwtArr[1]));
                        Boolean success = true;
                        success = success && string.Equals(jwtArr[2], Base64UrlEncoder.Encode(hs256.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(jwtArr[0], ".", jwtArr[1])))));
                        //驗證Token安全金鑰的值是否正確
                        if (!success)
                        {
                            await BadResponse(Response);
                        }
                        else
                        {
                            //驗證Token是否過期
                            if (!TimeChecked(PayLoad["iat"].ToString(), PayLoad["exp"].ToString()))
                            {
                                await BadResponse(Response);
                            }
                            else
                            {
                                //驗證使用者是否存在
                                if (!await UserChecked(PayLoad["nameid"].ToString(), DBcontext))
                                {
                                    await BadResponse(Response);
                                }
                                // else
                                // {
                                //     // string Role = PayLoad["Role"]..ToString();
                                //     //驗證角色權限
                                //     if (!await RolePermissionsChecked(PayLoad["Role"].ToString(), Path, URLMethod, DBcontext))
                                //         await BadResponse(Response);
                                // }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        context.Response.ContentType = "text/html; charset = utf-8";
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("權限驗證失敗" + "\r\n" + e.ToString(), Encoding.GetEncoding("utf-8"));
                        await Task.CompletedTask;
                        return;
                    }

                }
            }

            await this._next(context);
        }

        public Boolean RouteChecked(string Path, string URLMethod)
        {
            //判斷Request路徑是不是登入，如果是登入，就執行Token驗證
            if (Path == "/api/Auth/Login" && URLMethod == "POST")
                return false;

            return true;
        }

        public Boolean TimeChecked(string StartTime, string EndTime)
        {
            //將現在時間轉換成Unix時間戳
            Int32 DateNowUnixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            // && DateNowUnixTimestamp <= Int32.Parse(EndTime)
            if (DateNowUnixTimestamp >= Int32.Parse(StartTime))
                return true;

            return false;
        }

        public async Task<Boolean> UserChecked(string Account, CustomContext DBcontext)
        {
            //判斷該帳號是否存在
            var Users = await DBcontext.users.SingleOrDefaultAsync(u => u.account_number == Account);
            if (Users == null)
                return false;

            return true;
        }

        public async Task<Boolean> RolePermissionsChecked(string Role, string Path, string URLMethod, CustomContext DBcontext)
        {
            string[] PathDetails = Path.Split("/");
            string FunctionNames = "";
            string Actions = "";

            //確認Request路徑
            #region  人事管理權限
            // 10
            if ((PathDetails[2] == "Permissions" && URLMethod == "GET") ||
                (PathDetails[2] == "FunctionNames" && URLMethod == "GET") ||
                (PathDetails[2] == "Actions" && URLMethod == "GET") ||
                (PathDetails[2] == "Roles" && URLMethod == "GET") ||
                (PathDetails[2] == "Users" && URLMethod == "GET"))
            {
                FunctionNames = "HRManage";
                Actions = "查詢";
            }
            // 5
            if ((PathDetails[2] == "Permissions" && URLMethod == "POST") ||
                (PathDetails[2] == "FunctionNames" && URLMethod == "POST") ||
                (PathDetails[2] == "Actions" && URLMethod == "POST") ||
                (PathDetails[2] == "Roles" && URLMethod == "POST") ||
                (PathDetails[2] == "Users" && URLMethod == "POST"))
            {
                FunctionNames = "HRManage";
                Actions = "新增";
            }
            // 5
            if ((PathDetails[2] == "Permissions" && URLMethod == "PUT") ||
                (PathDetails[2] == "FunctionNames" && URLMethod == "PUT") ||
                (PathDetails[2] == "Actions" && URLMethod == "PUT") ||
                (PathDetails[2] == "Roles" && URLMethod == "PUT") ||
                (PathDetails[2] == "Users" && URLMethod == "PUT"))
            {
                FunctionNames = "HRManage";
                Actions = "修改";
            }
            // 5
            if ((PathDetails[2] == "Permissions" && URLMethod == "DELETE") ||
                (PathDetails[2] == "FunctionNames" && URLMethod == "DELETE") ||
                (PathDetails[2] == "Actions" && URLMethod == "DELETE") ||
                (PathDetails[2] == "Roles" && URLMethod == "DELETE") ||
                (PathDetails[2] == "Users" && URLMethod == "DELETE"))
            {
                FunctionNames = "HRManage";
                Actions = "刪除";
            }
            #endregion

            //確認角色權限是否存在
            if (!await PermissionsChecked(Role, FunctionNames, Actions, DBcontext))
                return false;

            return true;
        }

        public async Task BadResponse(HttpResponse Response)
        {
            Response.ContentType = "text/html; charset = utf-8";
            Response.StatusCode = 401;
            await Response.WriteAsync("權限驗證失敗", Encoding.GetEncoding("utf-8"));
            await Task.CompletedTask;
        }

        public async Task<Boolean> PermissionsChecked(string Role, string FunctionNames, string Actions, CustomContext DBcontext)
        {
            var ROlePermissions = await DBcontext.role_permissions
                                                 .Include(r => r.roles)
                                                 .Include(p => p.permissions)
                                                     .ThenInclude(permissions => permissions.function_names)
                                                 .Include(p => p.permissions)
                                                     .ThenInclude(permissions => permissions.actions)
                                                 .Where(r => r.roles.role == Role && r.permissions.function_names.function_name == FunctionNames && r.permissions.actions.action == Actions)
                                                 .ToListAsync();

            if (ROlePermissions.Count() == 0)
                return false;

            return true;
        }
    }
}