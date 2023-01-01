using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saas.Business.Abstract;
using Saas.Entities.Models.UserClaims;
using Saas.WebCoreApi.Helpers;


namespace Saas.WebCoreApi.Controllers
{
    /// <summary>
    /// CompanyOperationClaim
    /// </summary>
    [ApiVersion("1.0")] //,Deprecated = true
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CompanyOperationClaimController : ControllerBase
    {
        private IOperationClaimService _operationClaimService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationClaimService"></param>
        public CompanyOperationClaimController(IOperationClaimService operationClaimService)
        {
            _operationClaimService = operationClaimService;
        }

        /// <summary>
        /// getList of CompanyOperationClaim
        /// </summary>
        /// <returns></returns>
        [HttpGet(template: "GetList")]
        [MapToApiVersion("1.0")]
        [Authorize()]
        public IActionResult GetList()
        {
            var result = _operationClaimService.GetList();
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }

        /// <summary>
        /// CompanyOperationClaim getByid
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [HttpGet(template: "GetById/{roleId:Guid}")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public IActionResult GetById(Guid roleId)
        {
            var result = _operationClaimService.GetById(roleId);
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }

        /// <summary>
        ///  add CompanyOperationClaim
        /// </summary>
        /// <param name="userClaim"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public IActionResult Add(CompanyOperationClaim userClaim)
        {

            var token = Request.Headers.Authorization;
            if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
                return Unauthorized();
            string username = TokenHelper.ParseUserNameFromAccessToken(token);
            userClaim.CreatedBy = username;
            var result = _operationClaimService.Add(userClaim);
            if (result.Success)
            {
                var cacheUpdate = Task.Run(() => GetList());//cache yenilemece
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }


        /// <summary>
        ///  update CompanyOperationClaim
        /// </summary>
        /// <param name="userClaim"></param>
        /// <returns></returns>
        [HttpPut(template: "Update")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public IActionResult Update(CompanyOperationClaim userClaim)
        {
            var result = _operationClaimService.Update(userClaim);
            if (result.Success)
            {
                var cacheUpdate = Task.Run(() => GetList());//cache yenilemece
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// delete CompanyOperationClaim
        /// </summary>
        /// <param name="userClaim"></param>
        /// <returns></returns>
        [HttpPost(template: "Delete")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public IActionResult Delete(CompanyOperationClaim userClaim)
        {

            var result = _operationClaimService.Update(PrepareForDelete(userClaim));
            if (result.Success)
            {
                var cacheUpdate = Task.Run(() => GetList());//cache yenilemece async
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet(template: "PrepareForDelete")]
        private CompanyOperationClaim PrepareForDelete(CompanyOperationClaim userClaim)
        {
            if (userClaim.Deleted)
                return userClaim;
            userClaim.Deleted = true;
            return userClaim;
        }

    }
}
