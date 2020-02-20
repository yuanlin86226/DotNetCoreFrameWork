using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Models;
using Repositories.IRepositories;
using Resources.Request;
using Resources.Response;
using Services.Communication;
using Services.IServices;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace Services
{

    public class CheckinLogsService : ICheckinLogsService
    {
        private readonly ICheckinLogsRepository _CheckinLogsRepository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _accessor;

        public CheckinLogsService(ICheckinLogsRepository CheckinLogsRepository, IMapper mapper, IHttpContextAccessor accessor)
        {
            this._CheckinLogsRepository = CheckinLogsRepository ??
                throw new ArgumentNullException(nameof(CheckinLogsRepository));

            this._mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

            this._accessor = accessor ??
                throw new ArgumentNullException(nameof(accessor));
        }

        public async Task<SaveCheckinLogsResponse> CreateAsync()
        {
            try
            {
                var CheckinLogs = new CheckinLogsModels();

                // Get User ID
                var Request = _accessor.HttpContext.Request;
                string Token = Request.Headers["Authorization"];
                var jwtArr = Token.Split('.');
                var PayLoad = JsonConvert.DeserializeObject<Dictionary<string, object>>(Base64UrlEncoder.Decode(jwtArr[1]));
                var user = await _CheckinLogsRepository.FindUserID(PayLoad["nameid"].ToString());
                CheckinLogs.user_id = user.user_id;

                // Get Client IP
                // string IP4Address = String.Empty;
                // foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
                // {
                //     if (IPA.AddressFamily.ToString() == "InterNetwork")
                //     {
                //         IP4Address = IPA.ToString();
                //         break;
                //     }
                // }
                // CheckinLogs.ip = IP4Address;

                WebClient webClient = new WebClient();
                CheckinLogs.ip = webClient.DownloadString("https://api.ipify.org");

                await _CheckinLogsRepository.CreateAsync(CheckinLogs);
                return new SaveCheckinLogsResponse(CheckinLogs);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SaveCheckinLogsResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<IEnumerable<CheckinLogsResource>> ReadAllAsync(string log)
        {
            var CheckinLogs = await _CheckinLogsRepository.ReadAllAsync(log);
            var resources = _mapper.Map<IEnumerable<CheckinLogsModels>, IEnumerable<CheckinLogsResource>>(CheckinLogs);
            return resources;
        }
    }
}