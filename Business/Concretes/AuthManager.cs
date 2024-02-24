using Business.Abstracts;
using Business.Validations.Auths;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Security;
using Core.Security.JWT;
using DataAccess.Abstracts;
using Entity.DTOs.Users;

namespace Business.Concretes;

public class AuthManager(IUserService userService,
                         ITokenHelper tokenHelper)
    : IAuthService
{
    [CacheRemoveAspect(Priority = 0)]
    public async Task RegisterAsync(UserAddDto userAddDto)
    {
        await userService.AddAsync(userAddDto);
    }

    [ValidationAspect(typeof(LoginValidations), Priority = 0)]
    public async Task<AccessToken> LoginAsync(UserLoginDto userLoginDto, ValidationReturn vr)
    {
        return await Task.Run(() => tokenHelper.GenerateAccessToken(vr.User));
    }
}
