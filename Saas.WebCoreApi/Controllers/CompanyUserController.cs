using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using Saas.Business.Abstract.User;
using Saas.Entities.Dto;
using Saas.Entities.Models.User;
using Saas.WebCoreApi.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Saas.WebCoreApi.Controllers
{
    /// <summary>
    /// CompanyUser
    /// </summary>
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CompanyUserController : ControllerBase
    {
        private ICompanyUserService _companyUserService;
        private readonly IAuthService _authService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyUserService"></param>
        public CompanyUserController(ICompanyUserService companyUserService, IAuthService authService)
        {
            _companyUserService = companyUserService;
            _authService = authService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet(template: "GetList")]
        [MapToApiVersion("1.0")]
        [Authorize()]
        public IActionResult GetList()
        {
            var result = _companyUserService.GetUserList();
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);

        }

        /// <summary>
        ///  GetById CompanyUser
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet(template: "GetById/{userID:Guid}")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public IActionResult GetById(Guid userID)
        {
            var result = _companyUserService.GetUserById(userID);
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }


        /// <summary>
        /// CompanyUser add
        /// </summary>
        /// <param name="usr"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public IActionResult Add(CompanyUserDto usr)
        {
            var token = Request.Headers.Authorization;
            var username = TokenHelper.ParseUserNameFromAccessToken(token);
            usr.CreatedBy = username;
            var result = _companyUserService.Register(usr);
            if (result.Success)
            {
                var cacheUpdate = Task.Run(() => GetList());//cache tazelen'yor
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        /// <summary>
        ///  CompanyUser update
        /// </summary>
        /// <param name="usr"></param>
        /// <returns></returns>
        [HttpPut(template: "Update")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public IActionResult Update(CompanyUserUpdateDto usr)
        {
            var result = _companyUserService.UpdateUser(usr);
            if (result.Success)
            {
                var cacheUpdate = Task.Run(() => GetList());//cache tazelen'yor
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        ///  CompanyUser delete
        /// </summary>
        /// <param name="usr"></param>
        /// <returns></returns>
        [HttpPost(template: "Delete")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public IActionResult Delete(Guid Id)
        {
            var usr = _companyUserService.GetUserById(Id);
            if (usr == null || !usr.Success)
                return NotFound();
            var result = _companyUserService.Update(PrepareForDelete(usr.Data));
            if (result.Success)
            {
                var cacheUpdate = Task.Run(() => GetList());//cache tazelen'yor
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [HttpGet(template: "PrepareForDelete")]
        private CompanyUser PrepareForDelete(CompanyUser usr)
        {
            if (usr.Deleted)
                return usr;
            usr.Deleted = true;
            return usr;
        }

    }
}
