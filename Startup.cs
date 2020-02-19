using System;
using System.IO;
using System.Reflection;
using System.Text;
using AutoMapper;
using Context;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Repositories;
using Repositories.IRepositories;
using Services;
using Services.IServices;
using Swashbuckle.AspNetCore.Swagger;


namespace api
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) //ConfigureServices 是用來將服務註冊到 DI 容器用的。
        {
            //注入跨域問題
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyOrigin() //允許所有網域連線
                          .AllowAnyHeader() //允許所有表頭呼叫API
                          .AllowAnyMethod() //允許任何 HTTP 方法 GET、POST、PUT、DELETE
                          .AllowCredentials(); //認證需要在 CORS 要求的特殊處理。 根據預設，瀏覽器不會傳送具有跨原始要求的認證。 認證包含 cookie 與 HTTP 驗證配置。
                });
            });

            //注入JWT驗證
            // 檢查 HTTP Header 的 Authorization 是否有 JWT Bearer Token    
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                // 設定 JWT Bearer Token 的檢查選項
                options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true, //發行者驗證
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidateAudience = true, //接收者驗證
                        ValidAudience = Configuration["Jwt:Issuer"],
                        // ValidateLifetime = true, // 存活時間驗證
                        ValidateIssuerSigningKey = true, //金鑰驗證
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                }
            );

            //注入SQL Server連線
            services.AddDbContext<CustomContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            });
            // services.AddEntityFrameworkSqlServer().AddDbContext<CustomContext>(options =>
            // {
            //     options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            // }, ServiceLifetime.Scoped);
            // services.AddTransient<CustomContext>();


            #region  注入Services的DI
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRolesService,RolesService>();
            services.AddScoped<IFunctionNamesService,FunctionNamesService>();
            services.AddScoped<IActionsService,ActionsService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<ICheckinLogsService, CheckinLogsService>();
            #endregion

            #region 注入Repositories的DI
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IRolePermissionsRepository, RolePermissionsRepository>();
            services.AddScoped<IFunctionNamesRepository, FunctionNamesRepository>();
            services.AddScoped<IActionsRepository, ActionsRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<ICheckinLogsRepository, CheckinLogsRepository>();
            #endregion

            //加入MVC架構
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(
                options =>
                {
                    // options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; //輸出時自動忽略Null欄位
                    options.SerializerSettings.Formatting = Formatting.Indented; //輸出時按照JSON格式自動縮排
                }
            );

            //注入AutoMapper
            services.AddAutoMapper();
            //注入HttpClient
            services.AddHttpClient();

            //AddSwaggerGen：Swagger 產生器是負責取得 API 的規格並產生 SwaggerDocument 物件。
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    // name: 攸關 SwaggerDocument 的 URL 位置。
                    name: "v1",
                    // info: 是用於 SwaggerDocument 版本資訊的顯示(內容非必填)。
                    info: new Info
                    {
                        Title = "RESTful API",
                        Version = "1.0.0",
                        Description = "This is ASP.NET Core RESTful API Sample.",
                        TermsOfService = "None",
                        Contact = new Contact
                        {
                            Name = "WinHome"
                        },
                        License = new License
                        {
                            Name = "CC BY-NC-SA 4.0",
                            Url = "https://creativecommons.org/licenses/by-nc-sa/4.0/"
                        }
                    }
                );

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CustomContext dbContext)
        {
            //Configure 方法的參數並不固定，參數的實例都是從 WebHost 注入進來，可依需求增減需要的參數。
            //IApplicationBuilder 是最重要的參數也是必要的參數，Request 進出的 Pipeline 都是透過 ApplicationBuilder 來設定。
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // UseSwagger
            // Swagger Middleware 負責路由，提供 SwaggerDocument 物件。
            // 可以從 URL 查看 Swagger 產生器產生的 SwaggerDocument 物件。
            // http://localhost:5000/swagger/v1/swagger.json
            app.UseSwagger();

            //UseSwaggerUI
            //SwaggerUI 是負責將 SwaggerDocument 物件變成漂亮的介面。
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    // url: 需配合 SwaggerDoc 的 name。 "/swagger/{SwaggerDoc name}/swagger.json"
                    url: "/swagger/v1/swagger.json",
                    // description: 用於 Swagger UI 右上角選擇不同版本的 SwaggerDocument 顯示名稱使用。
                    name: "RESTful API v1.0.0"
                );
                c.RoutePrefix = string.Empty;
            });

            //設定跨域權限
            app.UseCors("CorsPolicy");
            //建立資料庫連線
            dbContext.Database.EnsureCreated();
            app.UseHttpsRedirection();
            app.UseCustomMiddleware(0);
            app.UseMvc();
            app.UseMvcWithDefaultRoute();
        }
    }
}