using AutoMapper;
using Saas.Entities.Dto;
using Saas.Entities.Models;
using Saas.Entities.Models.Branch;
using Saas.Entities.Models.Invoices.Header;
using Saas.Entities.Models.Invoices.Rows;
using Saas.Entities.Models.Products;
using Saas.Entities.Models.User;
using Saas.WebCoreApi.Dto;

namespace Saas.Core.CrossCuttingConcerns.Mapper.AutoMapper
{
    public class MappingModels : Profile
    {
        public MappingModels()
        {
            CreateMap<CompanyDto, Company>().ReverseMap();
            CreateMap<CompanyUpdateDto, Company>().ReverseMap();

            CreateMap<CompanyBranchDto, CompanyBranch>().ReverseMap();
            CreateMap<CompanyBranchUpdateDto, CompanyBranch>().ReverseMap();

            CreateMap<CompanyUserBranchDto, CompanyUserBranches>().ReverseMap();
            CreateMap<CompanyUserBranchUpdateDto, CompanyUserBranches>().ReverseMap();


            CreateMap<CompanyProductDto, CompanyProducts>().ReverseMap();
            CreateMap<CompanyProductUpdateDto, CompanyProducts>().ReverseMap();

            CreateMap<CompanyProductUnitDto, CompanyProductUnits>().ReverseMap();
            CreateMap<CompanyProductUnitUpdateDto, CompanyProductUnits>().ReverseMap();

            CreateMap<CompanyOfferDto, CompanyOffer>().ReverseMap();
            CreateMap<CompanyOfferUpdateDto, CompanyOffer>().ReverseMap();

            CreateMap<CompanyOfferRowDto, OfferRow>().ReverseMap();
            CreateMap<CompanyOfferRowUpdateDto, OfferRow>().ReverseMap();
        }
    }
}
