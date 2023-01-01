using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saas.Business.Abstract.User;
using Saas.Entities.Models.UserClaims;
using Saas.WebCoreApi.Helpers;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Saas.WebCoreApi.Controllers
{
    /// <summary>
    /// CompanyUserOperationClaim
    /// </summary>
    [ApiVersion("1.0")] //,Deprecated = true
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CompanyUserOperationClaimController : ControllerBase
    {
        private ICompanyOperationUserClaimService _companyOperationUserClaimService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyOperationUserClaimService"></param>
        public CompanyUserOperationClaimController(ICompanyOperationUserClaimService companyOperationUserClaimService)
        {
            _companyOperationUserClaimService = companyOperationUserClaimService;
        }


        /// <summary>
        ///  GetList CompanyUserOperationClaim
        /// </summary>
        /// <returns></returns>
        [HttpGet(template: "GetList")]
        [MapToApiVersion("1.0")]
        [Authorize()]
        public IActionResult GetList()
        {
            var result = _companyOperationUserClaimService.GetList();
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }


        /// <summary>
        ///   GetById  CompanyUserOperationClaim
        /// </summary>
        /// <returns></returns>
        [HttpGet(template: "GetById/{roleId:Guid}")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public IActionResult GetById(Guid roleId)
        {
            var result = _companyOperationUserClaimService.GetByRoleId(roleId);
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }

        /// <summary>
        /// CompanyUserOperationClaim add
        /// </summary>
        /// <param name="userClaim"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public IActionResult Add(CompanyOperationUserClaim userClaim)
        {
            var token = Request.Headers.Authorization;
            if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
                return Unauthorized();
            string username = TokenHelper.ParseUserNameFromAccessToken(token);
            userClaim.CreatedBy = username;
            var result = _companyOperationUserClaimService.Add(userClaim);
            if (result.Success)
            {
                var cacheUpdate = GetList();
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }


        /// <summary>
        /// CompanyUserOperationClaim update
        /// </summary>
        /// <param name="userClaim"></param>
        /// <returns></returns>
        [HttpPut(template: "Update")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public IActionResult Update(CompanyOperationUserClaim userClaim)
        {
            var result = _companyOperationUserClaimService.Update(userClaim);
            if (result.Success)
            {
                var cacheUpdate = GetList();
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// CompanyUserOperationClaim delete
        /// </summary>
        /// <param name="userClaim"></param>
        /// <returns></returns>
        [HttpPost(template: "Delete")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public IActionResult Delete(CompanyOperationUserClaim userClaim)
        {
            var result = _companyOperationUserClaimService.Update(PrepareForDelete(userClaim));
            if (result.Success)
            {
                var cacheUpdate = GetList();
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet(template: "PrepareForDelete")]
        private CompanyOperationUserClaim PrepareForDelete(CompanyOperationUserClaim userClaim)
        {
            if (userClaim.Deleted)
                return userClaim;
            userClaim.Deleted = true;
            return userClaim;
        }



    }
}
