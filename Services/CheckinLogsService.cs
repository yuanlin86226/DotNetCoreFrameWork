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

namespace Services
{

    public class CheckinLogsService : ICheckinLogsService
    {
        private readonly ICheckinLogsRepository _CheckinLogsRepository;
        private readonly IMapper _mapper;

        public CheckinLogsService(ICheckinLogsRepository CheckinLogsRepository, IMapper mapper)
        {
            this._CheckinLogsRepository = CheckinLogsRepository ??
                throw new ArgumentNullException(nameof(CheckinLogsRepository));

            this._mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SaveCheckinLogsResponse> CreateAsync(InsertCheckinLogsResource resource)
        {
            try
            {
                var CheckinLogs = _mapper.Map<InsertCheckinLogsResource, CheckinLogsModels>(resource);
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