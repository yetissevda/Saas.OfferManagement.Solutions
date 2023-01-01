using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saas.Business.Abstract.User;
using Saas.Core.Aspect.Autofac.Logging;
using Saas.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Saas.Entities.Dto;
using Saas.Entities.Models.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Saas.WebCoreApi.Controllers
{
    /// <summary>
    /// CompanyUserBranches
    /// </summary>
    [ApiVersion("1.0")] //,Deprecated = true
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CompanyUserBranchesController : ControllerBase
    {
        private ICompanyUserBranchesService _companyUserBranchesService;
        private IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyUserBranchesService"></param>
        public CompanyUserBranchesController(ICompanyUserBranchesService companyUserBranchesService, IMapper mapper)
        {
            _companyUserBranchesService = companyUserBranchesService;
            _mapper = mapper;
        }

        /// <summary>
        ///  GetListAsync  CompanyUserBranches
        /// </summary>
        /// <returns></returns>
        [HttpGet(template: "GetListAsync")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public async Task<IActionResult> GetListAsync()
        {
            var result = await _companyUserBranchesService.GetCompanyUserBranchesList();
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }

        /// <summary>
        ///  GetListAsync  CompanyUserBranches
        /// </summary>
        /// <returns></returns>
        [HttpGet(template: "GetUserBranchWithUserId/{userId:Guid}")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public async Task<IActionResult> GetUserBranchWithUserId(Guid userId)
        {
            var result = await _companyUserBranchesService.GetCompanyUserBranchesList();
            if (result.Success)
            {
                var lstforFiltered = result.Data.Where(x => x.CompanyUserId == userId).ToList();
                return Ok(lstforFiltered);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// GetAsyncById CompanyUserBranches
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        [HttpGet(template: "GetAsyncById/{ID:Guid}")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public async Task<IActionResult> GetAsyncById(Guid ID)
        {
            var result = await _companyUserBranchesService.GetCompanyUserBranchesById(ID);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        /// <summary>
        /// CompanyUserBranches add
        /// </summary>
        /// <param name="compUserBranch"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public async Task<IActionResult> Post(CompanyUserBranchDto compUserBranch)
        {
            var realBranch = _mapper.Map<CompanyUserBranches>(compUserBranch);
            var result = await _companyUserBranchesService.Add(realBranch);
            if (result.Success)
            {
                GetListAsync();
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        /// <summary>
        /// CompanyUserBranches update
        /// </summary>
        /// <param name="compUserBranch"></param>
        /// <returns></returns>
        [HttpPut(template: "Update")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public async Task<IActionResult> Put(CompanyUserBranchUpdateDto compUserBranch)
        {
            var realBranch = _mapper.Map<CompanyUserBranches>(compUserBranch);
            if (realBranch.BranchId == Guid.Empty)
                return BadRequest("Id Can not be blank");
            var control = await _companyUserBranchesService.GetCompanyUserBranchesById(compUserBranch.ID);
            if (!control.Success)
                return BadRequest("Branch Cannot find");

            var result = await _companyUserBranchesService.Update(realBranch);
            if (result.Success)
            {
                GetListAsync();//cache yenilemece
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        /// <summary>
        /// CompanyUserBranches delete
        /// </summary>
        /// <param name="compUserBranch"></param>
        /// <returns></returns>
        [HttpPost(template: "Delete")]
        [MapToApiVersion("1.0")]
        [Authorize]
        [LogAspect(typeof(DatabaseLogger))]
        public async Task<IActionResult> Delete(CompanyUserBranchUpdateDto compUserBranch)
        {
            var realBranch = _mapper.Map<CompanyUserBranches>(compUserBranch);

            if (realBranch is not null)
            {
                var beforeUpdate = await _companyUserBranchesService.GetCompanyUserBranchesById(realBranch.ID);
                if (beforeUpdate.Success)
                {
                    var result = await _companyUserBranchesService.Update(PrepareForDelete(beforeUpdate.Data));
                    if (result.Success)
                    {
                        GetListAsync(); //cache yenilemece
                        return Ok(result.Data);
                    }
                }
                return BadRequest(beforeUpdate.Message);
            }
            return BadRequest("Not Mapped");
        }

        private CompanyUserBranches PrepareForDelete(CompanyUserBranches compUserBranch)
        {
            if (compUserBranch.Deleted)
                return compUserBranch;
            compUserBranch.Deleted = true;
            return compUserBranch;
        }
    }
}
