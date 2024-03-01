using Core.Abstracts;
using Core.Validations.Auths;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Security.JWT;
using Core.Utilities;
using Core.Utilities.ByPass;
using Entity.DTOs.Users;
using Core.CrossCuttingConcerns.Security;
using Business.Concretes.Common;

namespace Core.Concretes;

public class AuthManager(IUserService userService,
                         ITokenHelper tokenHelper)
    : ManagerBase, IAuthService
{

    [WithoutAuthorization]
    public async Task RegisterAsync(UserAddDto userAddDto)
    {
        ServicesTool.GetService<AuthorizationByPass>().ByPass = true;

        await userService.AddAsync(userAddDto);
    }

    [WithoutAuthorization]
    [ValidationAspect(typeof(LoginValidations), Priority = 0)]
    public async Task<AccessToken> LoginAsync(UserLoginDto userLoginDto)
    {
        return await Task.Run(() => tokenHelper.GenerateAccessToken(ValidationReturn.Entity));
    }
}
