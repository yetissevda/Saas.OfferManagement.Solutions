using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saas.Business.Abstract.User;
using Saas.Entities.Dto;

namespace Saas.WebCoreApi.Controllers
{
    /// <summary>
    /// Auth process
    /// </summary>
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;


        /// <summary>
        /// login-token-User process
        /// </summary>
        /// <param name="authService"></param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// async JWT TOKEN
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto user)
        {
            var userToLogin = await Task.Run(() => _authService.Login(user));
            if (!userToLogin.Success)
                return BadRequest(userToLogin.Message);
            var result = await Task.Run(() => _authService.CreateAccessToken(userToLogin.Data));
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }


        /// <summary>
        /// async company user can add with company and localId must be not null
        /// email must be uniq
        /// </summary>
        /// <param name="userForRegisterDto"></param>
        /// <returns></returns>
        //[HttpPost("register")]
        [HttpPost("Register")]
        [MapToApiVersion("2.0")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public async Task<IActionResult> Register(CompanyUserDto userForRegisterDto)
        {
            var userExist = await Task.Run(() => _authService.UserExist(userForRegisterDto.Email));
            if (!userExist.Success)
            {
                return BadRequest(userExist.Message);
            }
            var registerResult = await Task.Run(() => _authService.Register(userForRegisterDto));

            if (registerResult.Success)
            {
                var result = await Task.Run(() => _authService.CreateAccessToken(registerResult.Data));
                if (result.Success)
                {
                    return Ok(result.Data);
                }
                return BadRequest(result.Message);
            }
            return BadRequest(registerResult.Message);
        }

        /// <summary>
        /// async first record of company
        /// companyid - local id null, it will fill after record
        /// </summary>
        /// <param name="userForRegisterDto"></param>
        /// <returns></returns>
        [HttpPost("CompanyFirstRegister")]
        public async Task<IActionResult> CompanyFirstRegister(CompanyFirstRegisterDto userForRegisterDto)
        {
            var registerResult = await Task.Run(() => _authService.RegisterForCompany(userForRegisterDto));
            if (registerResult.Success)
            {
                if (registerResult.Success)
                {
                    return Ok(registerResult);
                }
                return BadRequest(registerResult.Message);
            }
            return BadRequest(registerResult.Message);
        }
    }
}
