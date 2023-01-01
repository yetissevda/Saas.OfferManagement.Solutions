using AutoMapper;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saas.Business.Abstract.Invoice;
using Saas.Core.Utilities.Results;
using Saas.Entities.Dto;
using Saas.Entities.Models.Invoices.Header;
using Saas.Entities.Models.Invoices.Rows;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Saas.WebCoreApi.Controllers
{
    /// <summary>
    /// CompanyOffer
    /// </summary>
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CompanyOfferController : ControllerBase
    {
        private readonly ICompanyOfferService _companyOfferService;
        private readonly ICompanyOfferRowsService _companyOfferRowsService;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyOfferService"></param>
        /// <param name="mapper"></param>
        /// <param name="companyOfferRowsService"></param>
        public CompanyOfferController(ICompanyOfferService companyOfferService, IMapper mapper, ICompanyOfferRowsService companyOfferRowsService)
        {
            _companyOfferService = companyOfferService;
            _mapper = mapper;
            _companyOfferRowsService = companyOfferRowsService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet(template: "GetListAsync")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public async Task<IActionResult> GetListAsync()
        {
            var result = await _companyOfferService.GetCompanyOfferListAsync();

            if (result.Success) return Ok(result.Data);

            return BadRequest(result.Message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offerId"></param>
        /// <returns></returns>
        [HttpGet(template: "GetByIdAsync/{offerId:Guid}")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(Guid offerId)
        {
            var result = await _companyOfferService.GetCompanyOfferByIdAsync(offerId);

            if (result.Success) return Ok(result.Data);

            return BadRequest(result.Message);
        }


        /// <summary>
        /// if rows include they will save
        /// </summary>
        /// <param name="ofrr"></param>
        /// <returns></returns>
        [HttpPost("AddAsync")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] OfferCreateRequestDto ofrr)
        {
            var ofrRows = _mapper.Map<List<OfferRow>>(ofrr.Rows);

            var offer = _mapper.Map<CompanyOffer>(ofrr.Header);
            offer.Rows = ofrRows;
           
            var result = await _companyOfferService.AddAsync(offer);

            if (result.Success) return Ok(result.Data);

            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update All prop in offer
        /// </summary>
        /// <param name="ofrr"></param>
        /// <returns></returns>
        [HttpPut(template: "UpdateAsync")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] OfferCreateRequestDto ofrr)
        {
            var offer = _mapper.Map<CompanyOffer>(ofrr.Header);
            var ofrRows = _mapper.Map<List<OfferRow>>(ofrr.Rows);
            offer.Rows = ofrRows;
            ofrRows.ForEach(x => x.HeaderId = offer.ID);
            if (offer.ID == Guid.Empty) return BadRequest("Id Can not be blank");

            var control = await _companyOfferService.GetCompanyOfferByIdAsync(offer.ID);

            if (!control.Success) return BadRequest("Offer Cannot find");


            var result = await _companyOfferService.UpdateAsync(offer);

            if (result.Success) return Ok(result.Data);

            return BadRequest(result.Message);
        }
        /// <summary>
        /// flag that deleted
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost(template: "DeleteAsync")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var res = _companyOfferService.GetCompanyOfferById(Id);
            if (!res.Success)
                return BadRequest("Offer Not Found");
            var result = await _companyOfferService.UpdateAsync(await PrepareForDelete(res.Data));
            var cacheUpdate = await GetListAsync();
            if (result.Success) return Ok(result.Message);
            return BadRequest(result.Message);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="approve">
        ///  Created,
        ///  PendingForApprove,
        ///  Approved,
        ///  SendedForOffer,
        ///  OfferReturned,
        ///  Finalized
        /// </param>
        /// <returns></returns>
        [HttpPost(template: "ApproveAsync")]
        [MapToApiVersion("1.0")]
        [Authorize]
        public async Task<IActionResult> ApproveAsync(ApproveRequestDto req)
        {
            var result = await _companyOfferRowsService.Approve(req);
            if (!result.Success) return BadRequest();
            return Ok(result.Data);
        }

        private Task<CompanyOffer> PrepareForDelete(CompanyOffer compUserBranch)
        {
            if (compUserBranch.Deleted)
                return Task.FromResult(compUserBranch);
            compUserBranch.Deleted = true;
            return Task.FromResult(compUserBranch);
        }
    }
}
