using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saas.Business.Abstract.Branch;
using Saas.Business.Abstract.Companies;
using Saas.Entities.Dto;
using Saas.Entities.Models;
using Saas.Entities.Models.Branch;

namespace Saas.WebCoreApi.Controllers
{
    /// <summary>
    /// CompanyBranch
    /// </summary>
    [ApiVersion("1.0")] //,Deprecated = true
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CompanyBranchesController : ControllerBase
    {
        private readonly ICompanyBranchesService _companyBranchesService;
        private readonly ICompanyService _companyService;
        private IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyBranchesService"></param>
        /// <param name="mapper"></param>
        public CompanyBranchesController(ICompanyBranchesService companyBranchesService, IMapper mapper, ICompanyService companyService)
        {
            _mapper = mapper;
            _companyService = companyService;
            _companyBranchesService = companyBranchesService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyBranchDto"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public IActionResult Add(CompanyBranchDto companyBranchDto)
        {
            var real = _mapper.Map<CompanyBranch>(companyBranchDto);
            var result = _companyBranchesService.Add(real);
            var cacheUpdate = Task.Run(() => GetList());
            if (result.Success)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }
        /// <summary>
        /// delete CompanyBranch
        /// </summary>
        /// <param name="companybranch"></param>
        /// <returns></returns>
        [HttpPut(template: "Delete/{branchID:Guid}")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public IActionResult Delete(Guid branchID)
        {
            var companybranch = _companyBranchesService.CompanyBranchById(branchID);
            if (companybranch.Success)
            {
                var result = _companyBranchesService.Update(PrepareForDelete(companybranch.Data));
                var cacheUpdate = GetList();//cache yenilemece
                if (result.Success)
                    return Ok(result.Message);
            }
            return BadRequest(companybranch.Message);
        }
        /// <summary>
        ///  getbyId CompanyBranch
        /// </summary>
        /// <param name="companybranchID">branchID</param>
        /// <returns></returns>
        [HttpGet(template: "GetById/{branchID:Guid}")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public IActionResult GetById(Guid branchID)
        {
            var result = _companyBranchesService.CompanyBranchById(branchID);
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }
        /// <summary>
        ///  GetList of CompanyBranch
        /// </summary>
        /// <returns></returns>
        [HttpGet(template: "GetList")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public IActionResult GetList()
        {
            var result = _companyBranchesService.CompanyBranchesList();
            if (result.Success)
            {
                result.Data.ForEach(x => x.Company = _companyService.GetCompanyById(x.CompanyId).Data ?? new Company());
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///[HttpGet(template: "PrepareForDelete")]
        private CompanyBranch PrepareForDelete(CompanyBranch companybranch)
        {
            companybranch.Deleted = true;
            return companybranch;
        }

        /// <summary>
        /// CompanyBranch update
        /// </summary>
        /// <param name="companybranch"></param>
        /// <returns></returns>
        [HttpPut(template: "Update")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public IActionResult Update(CompanyBranchUpdateDto companybranch)
        {
            var real = _mapper.Map<CompanyBranch>(companybranch);
            var result = _companyBranchesService.Update(real);
            var cacheUpdate = Task.Run(() => GetList());//cache yenilemece
            if (result.Success)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }
    }
}
