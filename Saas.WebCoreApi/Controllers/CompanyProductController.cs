using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Saas.Business.Abstract.Companies;
using Saas.Business.Abstract.Product;
using Saas.Business.Abstract.User;
using Saas.Entities.Dto;
using Saas.Entities.Models;
using Saas.Entities.Models.Products;
using Saas.Entities.Models.User;
using Saas.WebCoreApi.Helpers;

namespace Saas.WebCoreApi.Controllers
{
    /// <summary>
    /// CompanyUser
    /// </summary>
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CompanyProductController : ControllerBase
    {
        private ICompanyProductService _companyProductService;
        private IMapper _mapper;
        private ICompanyService _companyService;

        public CompanyProductController(ICompanyProductService companyProductService, IMapper mapper, ICompanyService companyService)
        {
            _companyProductService = companyProductService;
            _mapper = mapper;
            _companyService = companyService;
        }

        /// <summary>
        /// async list CompanyProduct
        /// </summary>
        /// <returns></returns>
        [HttpGet(template: "GetListAsync")]
        [MapToApiVersion("1.0")]
        [Authorize()]
        public async Task<IActionResult> GetList()
        {
            var result = await _companyProductService.GetCompanyProductListAsync();
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);

        }

        /// <summary>
        ///  GetById CompanyProduct
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet(template: "GetById/{Pid:Guid}")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public IActionResult GetById(Guid Pid)
        {
            var result = _companyProductService.GetCompanyProductById(Pid);

            if (result.Success)
            {
                result.Data.Company = _companyService.GetCompanyById(result.Data.CompanyId).Data;
                return Ok(result.Data);

            }
            return BadRequest(result.Message);
        }


        /// <summary>
        /// CompanyProduct add
        /// </summary>
        /// <param name="pr"></param>
        /// <returns></returns>
        [HttpPost("AddAsync")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public async Task<IActionResult> Add(CompanyProductDto pr)
        {
            var products = _mapper.Map<CompanyProducts>(pr);
            // products.Company = _companyService.GetCompanyById(pr.CompanyId).Data;
            var token = Request.Headers.Authorization;
            var username = TokenHelper.ParseUserNameFromAccessToken(token);
            products.CreatedBy = username;
            var result = await _companyProductService.AddAsync(products);
            if (result.Success)
            {
                var cacheUpdate = Task.Run(() => GetList());//cache tazelen'yor
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        /// <summary>
        ///  CompanyProducts update
        /// </summary>
        /// <param name="usr"></param>
        /// <returns></returns>
        [HttpPut(template: "UpdateAsync")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public async Task<IActionResult> Update(CompanyProductUpdateDto prdt)
        {
            var products = _mapper.Map<CompanyProducts>(prdt);
            products.Company = _companyService.GetCompanyById(prdt.CompanyId).Data;
            var result = await _companyProductService.UpdateAsync(products);
            if (result.Success)
            {
                var cacheUpdate = Task.Run(() => GetList());//cache tazelen'yor
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        ///  CompanyProducts delete
        /// </summary>
        /// <param name="usr"></param>
        /// <returns></returns>
        [HttpPost(template: "Delete")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public IActionResult Delete(Guid Id)
        {
            var usr = _companyProductService.GetCompanyProductById(Id);
            if (usr == null || !usr.Success)
                return NotFound();
            var result = _companyProductService.Update(PrepareForDelete(usr.Data));
            if (result.Success)
            {
                var cacheUpdate = Task.Run(() => GetList());//cache tazelen'yor
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [HttpGet(template: "PrepareForDelete")]
        private CompanyProducts PrepareForDelete(CompanyProducts usr)
        {
            if (usr.Deleted)
                return usr;
            usr.Deleted = true;
            return usr;
        }

    }
}
