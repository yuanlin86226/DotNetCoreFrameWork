using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Models;
using Repositories.IRepositories;
using Resources.Response;
using Resources.Request;
using Services.Communication;
using Services.IServices;
using Utils;

namespace Services
{

    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _UsersRepository;
        private readonly IMapper _mapper;
        public UsersService(IUsersRepository UsersRepository, IMapper mapper)
        {
            this._UsersRepository = UsersRepository ??
                throw new ArgumentNullException(nameof(UsersRepository));
            this._mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SaveUsersResponse> CreateAsync(InsertUsersResource resource)
        {
            try
            {
                var Users = _mapper.Map<InsertUsersResource, UsersModels>(resource);
                //生成GUID
                Guid UUID = Guid.NewGuid();
                while (await _UsersRepository.ReadOneAsync(UUID.ToString()) != null)
                {
                    UUID = Guid.NewGuid();
                }

                //將密碼加密
                MD5HashUtils MD5 = new MD5HashUtils();
                string Md5Password = MD5.MD5Hash(Users.password);

                var NewUsers = new UsersModels
                {
                    user_id = UUID.ToString(),
                    account_number = Users.account_number,
                    password = Md5Password,
                    user_name = Users.user_name,
                    role_id = Users.role_id,
                    phone = Users.phone,
                    email = Users.email,
                    gender = Users.gender,
                    due_date = Users.due_date,
                    create_date = DateTime.Now
                };

                //將處理完的ID值儲存起來，並將其傳送至Repository儲存置資料庫
                await _UsersRepository.CreateAsync(NewUsers);
                return new SaveUsersResponse(NewUsers);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SaveUsersResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<IEnumerable<UsersResource>> ReadAllAsync(string account_number, string user_name)
        {
            var Users = await _UsersRepository.ReadAllAsync(account_number, null, user_name);

            return _mapper.Map<IEnumerable<UsersModels>, IEnumerable<UsersResource>>(Users);
        }

        public async Task<UsersResource> ReadOneAsync(string id)
        {
            var Users = await _UsersRepository.ReadOneAsync(id);

            return _mapper.Map<UsersModels, UsersResource>(Users); ;
        }

        public async Task<SaveUsersResponse> UpdateAsync(string id, UpdateUsersResource resource)
        {
            var Users = _mapper.Map<UpdateUsersResource, UsersModels>(resource);

            var existingUsers = await _UsersRepository.ReadOneAsync(id);
            if (existingUsers == null)
                return new SaveUsersResponse("Category not found.");

            //判斷是否要更改密碼
            if (resource.UpdatePasswordChecked == true)
            {
                //將密碼加密
                MD5HashUtils MD5 = new MD5HashUtils();
                string Md5Password = MD5.MD5Hash(Users.password);

                existingUsers.password = Md5Password;
            }else{
                existingUsers.password = existingUsers.password;
            }

            //將要儲存要更新的值
            existingUsers.user_name = Users.user_name;
            existingUsers.role_id = Users.role_id;
            existingUsers.phone = Users.phone;
            existingUsers.email = Users.email;
            existingUsers.gender = Users.gender;
            existingUsers.due_date = Users.due_date;
            existingUsers.resignation_date = Users.resignation_date;

            try
            {
                await _UsersRepository.UpdateAsync(existingUsers);

                return new SaveUsersResponse(existingUsers);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SaveUsersResponse($"An error occurred when updating the category: {ex.Message}");
            }
        }

        public async Task<Boolean> DeleteAsync(string id)
        {
            var Users = await _UsersRepository.ReadOneAsync(id);
            if (Users == null)
                return false;
            return await _UsersRepository.DeleteAsync(Users);
        }

    }
}