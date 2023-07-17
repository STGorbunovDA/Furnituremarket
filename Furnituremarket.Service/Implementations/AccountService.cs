using Furnituremarket.DAL.Interfaces;
using Furnituremarket.Domain.Enum;
using Furnituremarket.Domain.Helpers;
using Furnituremarket.Domain.Model;
using Furnituremarket.Domain.Response;
using Furnituremarket.Domain.ViewModels.Account;
using Furnituremarket.Service.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace Furnituremarket.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            try
            {
                var user = await _userRepository.GetByName(model.Name);

                if (user.Name != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = $"Такой пользователь уже есть {model.Name}",
                    };
                }

                user = new User()
                {
                    Name = model.Name,
                    Role = Role.User,
                    Password = HashPasswordHelper.HashPassowrd(model.Password),
                };

                await _userRepository.Create(user);
                var result = Authenticate(user);


                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = $"Пользователь {model.Name} зарегестрировался",
                    CodeStatus = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "[Register] ||: " + ex + " ||: " + ex.Message,
                    CodeStatus = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var user = await _userRepository.GetByName(model.Name);
                if (user.Name == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = $"Пользователь {model.Name} не найден"
                    };
                }

                if (user.Password != HashPasswordHelper.HashPassowrd(model.Password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = $"Неверный пароль пользователя {model.Name}"
                    };
                }
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = $"Пользователь {model.Name} авторизовался",
                    CodeStatus = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "[Login] ||: " + ex + " ||: " + ex.Message,
                    CodeStatus = StatusCode.InternalServerError
                };
            }
        }

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
