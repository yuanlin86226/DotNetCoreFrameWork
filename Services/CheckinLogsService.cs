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
        private readonly ICheckLogsRepository _CheckLogsRepository;
        private readonly IMapper _mapper;

        public CheckinLogsService(ICheckLogsRepository CheckLogsRepository, IMapper mapper)
        {
            this._CheckLogsRepository = CheckLogsRepository ??
                throw new ArgumentNullException(nameof(CheckLogsRepository));

            this._mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SaveCheckinLogsResponse> CreateAsync(InsertCheckinLogsResource resource)
        {
            try
            {
                var CheckinLogs = _mapper.Map<InsertCheckinLogsResource, CheckinLogsModels>(resource);
                await _CheckLogsRepository.CreateAsync(CheckinLogs);
                return new SaveCheckinLogsResponse(CheckinLogs);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SaveCheckinLogsResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<IEnumerable<ActionsResource>> ReadAllAsync(string action)
        {
            var Actions = await _CheckLogsRepository.ReadAllAsync(action);
            var resources = _mapper.Map<IEnumerable<ActionsModels>, IEnumerable<ActionsResource>>(Actions);
            return resources;
        }
    }
}