using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegister, string password);

        IDataResult<User> Login(UserForLoginDto userForLogin);
        IResult Exists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);

    }
}
