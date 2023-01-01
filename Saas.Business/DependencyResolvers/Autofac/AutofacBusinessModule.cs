using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Saas.Business.Abstract;
using Saas.Business.Abstract.Branch;
using Saas.Business.Abstract.Companies;
using Saas.Business.Abstract.Invoice;
using Saas.Business.Abstract.Product;
using Saas.Business.Abstract.User;
using Saas.Business.Concrete;
using Saas.Business.Concrete.Branch;
using Saas.Business.Concrete.Companies;
using Saas.Business.Concrete.Invoice;
using Saas.Business.Concrete.Product;
using Saas.Business.Concrete.User;
using Saas.Core.Security.Security.Jwt;
using Saas.Core.Utilities.Interceptors;
using Saas.DataAccess.EntityFrameWorkCore.EfDal;
using Saas.DataAccess.EntityFrameWorkCore.EfDal.Invoices;
using Saas.DataAccess.EntityFrameWorkCore.EfDal.Product;
using Saas.DataAccess.EntityFrameWorkCore.IDal;
using Saas.DataAccess.EntityFrameWorkCore.IDal.Invoices;
using Saas.DataAccess.EntityFrameWorkCore.IDal.Product;

namespace Saas.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule :Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Bagimsiz api yetkileri

            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<EfCompanyOperationClaimDal>().As<ICompanyOperationClaimDal>();

            #endregion


            #region companie

            builder.RegisterType<CompanyManager>().As<ICompanyService>();
            builder.RegisterType<EfCompanyDal>().As<ICompanyDal>();

            #endregion
            

            #region branch

            builder.RegisterType<CompanyBranchesManager>().As<ICompanyBranchesService>();
            builder.RegisterType<EfCompanyBranchDal>().As<ICompanyBranchDal>();

            #endregion


            #region user

            builder.RegisterType<CompanyOperationUserClaimManager>().As<ICompanyOperationUserClaimService>();
            builder.RegisterType<EfCompanyOperationUserClaimDal>().As<ICompanyOperationUserClaimDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();


            builder.RegisterType<CompanyUsersManager>().As<ICompanyUserService>();
            builder.RegisterType<EfCompanyUserDal>().As<ICompanyUserDal>();



            builder.RegisterType<CompanyUserBranchesManager>().As<ICompanyUserBranchesService>();
            builder.RegisterType<EfCompanyUserBranchesDal>().As<ICompanyUserBranchesDal>();

            #endregion


            #region invoice

            ///offer-rows
            builder.RegisterType<CompanyOfferManager>().As<ICompanyOfferService>();
            builder.RegisterType<EfCompanyOfferDal>().As<ICompanyOfferDal>();


            builder.RegisterType<CompanyOfferRowsManager>().As<ICompanyOfferRowsService>();
            builder.RegisterType<EfCompanyOfferRowsDal>().As<ICompanyOfferRowDal>();
            ///offer-rows

            #endregion
            

            #region Products

            //product-units

            builder.RegisterType<CompanyProductManager>().As<ICompanyProductService>();
            builder.RegisterType<CompanyProductUnitManager>().As<ICompanyProductUnitService>();


            builder.RegisterType<EfCompanyProductDal>().As<ICompanyProductDal>();
            builder.RegisterType<EfCompanyProductUnitDal>().As<ICompanyProductUnitDal>();

            //product-units


            #endregion



            




           
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()

            }).SingleInstance();


        }
    }
}
