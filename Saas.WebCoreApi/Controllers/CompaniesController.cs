using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saas.Business.Abstract.Companies;
using Saas.Entities.Dto;
using Saas.Entities.Models;
using Saas.WebCoreApi.Dto;
using Saas.WebCoreApi.Helpers;


namespace Saas.WebCoreApi.Controllers
{
    /// <summary>
    /// Company Process
    /// </summary>
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyService"></param>
        /// <param name="mapper"></param>
        public CompaniesController(ICompanyService companyService, IMapper mapper)
        {
            _mapper = mapper;
            _companyService = companyService;
        }


        /// <summary>
        /// Get All Companies..
        /// </summary>
        /// <returns></returns>
        [HttpGet(template: "GetList")]
        [MapToApiVersion("1.0")]
        [Authorize()]
        public IActionResult GetList()
        {
            var result = _companyService.GetCompanyList();
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);

        }

        /// <summary>
        ///  get company by Id
        /// </summary>
        /// <param name="companyId"> Guid</param>
        /// <returns></returns>
        [HttpGet(template: "GetById/{companyId:Guid}")]
        [MapToApiVersion("1.0")]
        [Authorize()]
        public IActionResult GetById(Guid companyId)
        {
            var result = _companyService.GetCompanyById(companyId);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        /// <summary>
        /// cache will update, then insert of company
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>

        [HttpPost("Add")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public IActionResult Add(CompanyDto company)
        {
            var real = _mapper.Map<Company>(company);
            var token = Request.Headers.Authorization;
            if (!string.IsNullOrWhiteSpace(token) || !string.IsNullOrEmpty(token))
            {
                var username = TokenHelper.ParseUserNameFromAccessToken(token);
                if (username != null)
                    real.CreatedBy = username;
            }
            var result = _companyService.Add(real);
            var cacheUpdate = Task.Run(() => GetList());//cache yenilemece
            if (result.Success)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }


        /// <summary>
        /// cache will update, then update of company
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        //[HttpPost(template: "update")]
        [HttpPut(template: "Update")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public IActionResult Update(CompanyUpdateDto company)
        {
            var real = _mapper.Map<Company>(company);
            var result = _companyService.Update(real);
            var CacheUpdate = Task.Run(() => GetList());//cache yenilemece
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        /// <summary>
        /// cache will update, then delete of company (not hard Delete)
        /// </summary>
        /// <param name="company">firma ID</param>
        /// <returns></returns>
        [HttpPost(template: "Delete/{companyId:Guid}")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public IActionResult Delete(Guid companyId)
        {
            var res = _companyService.GetCompanyById(companyId);
            if (res.Success)
            {
                var result = _companyService.Update(PrepareForDelete(res.Data));

                if (result.Success)
                {
                    var CacheUpdate = Task.Run(() => GetList());//cache yenilemece
                    return Ok(result.Message);
                }
            }
            return BadRequest(res.Message);
        }
        // [HttpGet(template: "PrepareForDelete")]
        private Company PrepareForDelete(Company company)
        {
            if (company.Deleted)
                return company;
            company.Deleted = true;
            return company;
        }
        /// <summary>
        /// async method for GetList
        /// </summary>
        /// <returns></returns>
        [HttpGet(template: "GetListAsync")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public async Task<IActionResult> GetListAsync()
        {
            var result = await _companyService.GetCompanyListAsync();
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }


    }
}
