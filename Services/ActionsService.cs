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

    public class ActionsService : IActionsService
    {
        private readonly IActionsRepository _ActionsRepository;
        private readonly IMapper _mapper;

        public ActionsService(IActionsRepository ActionsRepository, IMapper mapper)
        {
            this._ActionsRepository = ActionsRepository ??
                throw new ArgumentNullException(nameof(ActionsRepository));

            this._mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SaveActionsResponse> CreateAsync(InsertActionsResource resource)
        {
            try
            {
                var Actions = _mapper.Map<InsertActionsResource, ActionsModels>(resource);
                await _ActionsRepository.CreateAsync(Actions);
                return new SaveActionsResponse(Actions);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SaveActionsResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<IEnumerable<ActionsResource>> ReadAllAsync(string action)
        {
            var Actions = await _ActionsRepository.ReadAllAsync(action);
            var resources = _mapper.Map<IEnumerable<ActionsModels>, IEnumerable<ActionsResource>>(Actions);
            return resources;
        }

        public async Task<ActionsResource> ReadOneAsync(int id)
        {
            var Actions = await _ActionsRepository.ReadOneAsync(id);
            var resources = _mapper.Map<ActionsModels, ActionsResource>(Actions);

            return resources;
        }

        public async Task<SaveActionsResponse> UpdateAsync(int id, UpdateActionsResource resource)
        {
            var Actions = _mapper.Map<UpdateActionsResource, ActionsModels>(resource);
            var existingActions = await _ActionsRepository.ReadOneAsync(id);
            if (existingActions == null)
                return new SaveActionsResponse("Category not found.");

            existingActions.action = Actions.action;
            existingActions.update_user_id = Actions.update_user_id;
            existingActions.update_time = Actions.update_time;

            try
            {
                await _ActionsRepository.UpdateAsync(existingActions);

                return new SaveActionsResponse(existingActions);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SaveActionsResponse($"An error occurred when updating the category: {ex.Message}");
            }
        }

        public async Task<Boolean> DeleteAsync(int id)
        {
            var Actions = await _ActionsRepository.ReadOneAsync(id);
            if (Actions == null)
                return false;
            return await _ActionsRepository.DeleteAsync(id, Actions);
        }

    }
}