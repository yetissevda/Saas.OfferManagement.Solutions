using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Saas.Business.Abstract.Companies;
using Saas.Business.Abstract.Product;
using Saas.Entities.Dto;
using Saas.Entities.Models.Products;
using Saas.WebCoreApi.Helpers;


namespace Saas.WebCoreApi.Controllers
{
    /// <summary>
    /// CompanyUser
    /// </summary>
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CompanyProductUnitController : ControllerBase
    {
        private readonly ICompanyProductUnitService _companyProductUnitService;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyProductUnitService"></param>
        /// <param name="mapper"></param>
        /// <param name="companyService"></param>
        public CompanyProductUnitController(ICompanyProductUnitService companyProductUnitService, IMapper mapper, ICompanyService companyService)
        {
            _companyProductUnitService = companyProductUnitService;
            _mapper = mapper;
            _companyService = companyService;
        }


        /// <summary>
        /// async list CompanyProductUnit
        /// </summary>
        /// <returns>Productlist</returns>
        [HttpGet(template: "GetListAsync")]
        [MapToApiVersion("1.0")]
        [Authorize()]
        public async Task<IActionResult> GetList()
        {
            var result = await _companyProductUnitService.GetCompanyProductUnitListAsync();
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);

        }

        /// <summary>
        ///  GetById CompanyProductUnit
        /// </summary>
        /// <param name="Puid"></param>
        /// <returns>Product</returns>
        [HttpGet(template: "GetByIdAsync/{Puid:Guid}")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid Puid)
        {
            var result = await _companyProductUnitService.GetCompanyProductUnitByIdAsync(Puid);

            if (result.Success)
            {
                var r = await _companyService.GetCompanyByIdAsync(result.Data.CompanyId);
                result.Data.Company = r.Data;
                return Ok(result.Data);

            }
            return BadRequest(result.Message);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pr"></param>
        /// <returns>Product</returns>
        [HttpPost("AddAsync")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public async Task<IActionResult> Add(CompanyProductUnitDto pr)
        {
            var products = _mapper.Map<CompanyProductUnits>(pr);
            // products.Company = _companyService.GetCompanyById(pr.CompanyId).Data;
            var token = Request.Headers.Authorization;
            if (token == string.Empty || token.IsNullOrEmpty())
                return BadRequest("Auth Problem");

            var username = TokenHelper.ParseUserNameFromAccessToken(token);
            products.CreatedBy = username;
            var result = await _companyProductUnitService.AddAsync(products);
            if (result.Success)
            {
                var cacheUpdate = Task.Run(() => GetList());//cache tazelen'yor
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        /// <summary>
        ///  CompanyProductUnit update
        /// </summary>
        /// <param name="usr"></param>
        /// <returns>Product</returns>
        [HttpPut(template: "UpdateAsync")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public async Task<IActionResult> Update(CompanyProductUnitUpdateDto prdt)
        {
            var products = _mapper.Map<CompanyProductUnits>(prdt);
            products.Company = (await _companyService.GetCompanyByIdAsync(prdt.CompanyId)).Data;
            var result = await _companyProductUnitService.UpdateAsync(products);
            if (result.Success)
            {
                var cacheUpdate = Task.Run(() => GetList());//cache tazelen'yor
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        ///  CompanyProductUnit delete
        /// </summary>
        /// <returns>NoContent</returns>
        [HttpPost(template: "Delete")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public IActionResult Delete(Guid Id)
        {
            var usr = _companyProductUnitService.GetCompanyProductUnitById(Id);
            if (usr == null || !usr.Success)
                return NotFound();
            var result = _companyProductUnitService.Update(PrepareForDelete(usr.Data));
            if (result.Success)
            {
                var cacheUpdate = Task.Run(() => GetList());//cache tazelen'yor
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet(template: "PrepareForDelete")]
        private CompanyProductUnits PrepareForDelete(CompanyProductUnits usr)
        {
            if (usr.Deleted)
                return usr;
            usr.Deleted = true;
            return usr;
        }

    }
}
