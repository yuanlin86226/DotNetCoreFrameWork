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

    public class FunctionNamesService : IFunctionNamesService
    {
        private readonly IFunctionNamesRepository _FunctionNamesRepository;
        private readonly IMapper _mapper;

        public FunctionNamesService(IFunctionNamesRepository FunctionNamesRepository, IMapper mapper)
        {
            this._FunctionNamesRepository = FunctionNamesRepository ??
                throw new ArgumentNullException(nameof(FunctionNamesRepository));
            
            this._mapper = mapper ??
                throw new ArgumentException(nameof(mapper));
        }

        public async Task<SaveFunctionNamesResponse> CreateAsync(InsertFunctionNamesResource resource)
        {
            try
            {
                var FunctionNames = _mapper.Map<InsertFunctionNamesResource, FunctionNamesModels>(resource);
                await _FunctionNamesRepository.CreateAsync(FunctionNames);
                return new SaveFunctionNamesResponse(FunctionNames);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SaveFunctionNamesResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<IEnumerable<FunctionNamesResource>> ReadAllAsync(string function_name)
        {
            var FunctionNames = await _FunctionNamesRepository.ReadAllAsync(function_name);
            var resources = _mapper.Map<IEnumerable<FunctionNamesModels>, IEnumerable<FunctionNamesResource>>(FunctionNames);
            return resources;
        }

        public async Task<FunctionNamesResource> ReadOneAsync(int id)
        {
            var FunctionNames = await _FunctionNamesRepository.ReadOneAsync(id);
            var resources = _mapper.Map<FunctionNamesModels, FunctionNamesResource>(FunctionNames);
            return resources;
        }

        public async Task<SaveFunctionNamesResponse> UpdateAsync(int id, UpdateFunctionNamesResource resource)
        {
            var FunctionNames = _mapper.Map<UpdateFunctionNamesResource, FunctionNamesModels>(resource);
            
            var existingFunctionNames = await _FunctionNamesRepository.ReadOneAsync(id);
            if (existingFunctionNames == null)
                return new SaveFunctionNamesResponse("Category not found.");

            existingFunctionNames.function_name = FunctionNames.function_name;
            existingFunctionNames.update_user_id = FunctionNames.update_user_id;
            existingFunctionNames.update_time = FunctionNames.update_time;

            try
            {
                await _FunctionNamesRepository.UpdateAsync(existingFunctionNames);

                return new SaveFunctionNamesResponse(existingFunctionNames);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SaveFunctionNamesResponse($"An error occurred when updating the category: {ex.Message}");
            }
        }

        public async Task<Boolean> DeleteAsync(int id)
        {
            var FunctionNames = await _FunctionNamesRepository.ReadOneAsync(id);
            if (FunctionNames == null)
                return false;
            return await _FunctionNamesRepository.DeleteAsync(id, FunctionNames);
        }

    }
}