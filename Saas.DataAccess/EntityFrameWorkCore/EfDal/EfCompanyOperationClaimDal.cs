﻿using Saas.DataAccess.EntityFrameWorkCore.DbContexts;
using Saas.DataAccess.EntityFrameWorkCore.IDal;
using Saas.Entities.Generic;
using Saas.Entities.Models.UserClaims;

namespace Saas.DataAccess.EntityFrameWorkCore.EfDal
{
    public class EfCompanyOperationClaimDal :EfEntityRepositoryBase<CompanyOperationClaim,GordionDbContext>, ICompanyOperationClaimDal
    {
    }
}
