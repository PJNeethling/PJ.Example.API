﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PJ.Example.Abstractions.Exceptions;
using PJ.Example.Abstractions.Models;
using PJ.Example.Abstractions.Repositories;
using PJ.Example.Database.Abstractions;
using PJ.Example.Database.Abstractions.Models;
using PJ.Example.Database.Abstractions.ProcedureParamaters;
using PJ.Example.Domain.Abstractions.Models;
using PJ.Example.Domain.Abstractions.Models.Account;
using PJ.Example.Repository.Password_Manager.Infrastructure;
using System.Net;

namespace PJ.Example.Repository
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        private readonly IDatabase _database;
        private readonly IPassword _password;
        private readonly EncryptionOptions _options;

        public AccountRepository(IDatabase database, IPassword password, IOptions<EncryptionOptions> options)
        {
            _database = database;
            _password = password;
            _options = options.Value;
        }

        public async Task<AllUsers> GetAllUsers(int? statusId)
        {
            try
            {
                var users = await _database.GetAllUsers(statusId, _options.Passphrase);

                var result = new AllUsers { Users = new List<UserWithDates>() };

                if (users[0].Uuid != null)
                {
                    //add mapper
                    foreach (var user in users)
                    {
                        result.Users.Add(new UserWithDates
                        {
                            Uuid = user.Uuid,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            StatusId = user.StatusId,
                            CreatedDate = user.CreatedDate,
                            ModifiedDate = user.ModifiedDate
                        });
                    }
                }
                result.TotalItems = users[0].TotalItems;
                return result;
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);
            }
        }

        public async Task<UserDetails> GetUserDetails(string uUid)
        {
            try
            {
                var userDetails = await _database.GetUserDetails(uUid, _options.Passphrase);

                //add mapper
                var result = new UserDetails
                {
                    FirstName = userDetails.FirstName,
                    LastName = userDetails.LastName,
                    UserName = userDetails.UserName,
                    Email = userDetails.Email,
                    Number = userDetails.Number,
                    StatusId = userDetails.StatusId
                };

                if (userDetails.Roles != null)
                {
                    result.Roles = JsonConvert.DeserializeObject<List<Role>>(userDetails.Roles);
                }

                return result;
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);
            }
        }

        public async Task<UuidResponse> UpsertUser(UserModel userDetails, string uUid = null)
        {
            //var password = string.IsNullOrEmpty(userDetails.Password) ? Guid.NewGuid().ToString() : userDetails.Password;

            //implement automapper
            var parameters = new UserParams
            {
                Uuid = uUid != null ? Guid.Parse(uUid) : null,
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName,
                Email = userDetails.Email,
                UserName = userDetails.UserName,
                Number = userDetails.Number,
                StatusId = userDetails.StatusId
            };
            parameters.PassPhrase = _options.Passphrase;

            if (uUid is null)
            {
                var password = string.IsNullOrEmpty(userDetails.Password) ? Guid.NewGuid().ToString() : userDetails.Password;
                parameters.Password = _password.HashPassword(password);
            }
            else
            {
                if (!string.IsNullOrEmpty(userDetails.Password))
                {
                    parameters.Password = _password.HashPassword(userDetails.Password);
                }
            }

            try
            {
                var result = await _database.UpsertUser(parameters);

                return new UuidResponse { Uuid = result.Uuid };
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);
            }
        }

        public async Task AssignRolesToUser(string uUid, IdList roleIds)
        {
            //implement automapper
            try
            {
                await _database.AssignRolesToUser(Guid.Parse(uUid), roleIds);
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);
            }
        }

        public async Task<List<Role>> GetAllRoles()
        {
            try
            {
                var roles = await _database.GetAllRoles();

                var result = new List<Role> { };

                if (roles[0].Id != null)
                {
                    //add mapper
                    foreach (var role in roles)
                    {
                        result.Add(new Role
                        {
                            Id = role.Id.Value,
                            Name = role.Name
                        });
                    }
                }
                return result;
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);
            }
        }

        public async Task<Role> GetRoleDetails(int id)
        {
            try
            {
                var roleDetails = await _database.GetRoleDetails(id);

                var result = new Role();

                if (roleDetails.Id != 0)
                {
                    //add mapper
                    result.Id = roleDetails.Id.Value;
                    result.Name = roleDetails.Name;
                    result.Description = roleDetails.Description;
                    result.IsActive = roleDetails.IsActive.Value;
                    result.CreatedDate = roleDetails.CreatedDate.Value;
                    result.ModifiedDate = roleDetails.ModifiedDate.Value;
                }
                return result;
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);
            }
        }

        public async Task<IdResponse> UpsertRole(Role request)
        {
            try
            {
                //var roleRequest = _mapper.Map<RoleQuery>(request);
                //use mapper for the below

                var result = new Role();
                if (request.Id != 0)
                {
                    var role = await _database.GetRoleDetails(request.Id.Value);
                    if (role == null)
                    {
                        throw new ApiException(HttpStatusCode.NotFound, "Role not found");
                    }
                    role.Name = request.Name;
                    role.Description = request.Description;
                    role.IsActive = request.IsActive;
                    role.ModifiedDate = DateTime.UtcNow;

                    await _database.UpdateRole(role);
                    return new IdResponse { Id = role.Id.Value };
                }
                else
                {
                    var roleRequest = new RoleQuery
                    {
                        Name = request.Name,
                        Description = request.Description,
                        IsActive = request.IsActive
                    };

                    var response = await _database.CreateRole(roleRequest);

                    return new IdResponse { Id = response.Id };
                }
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);
            }
        }
    }
}