using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Saas.Entities.Generic;
using Saas.Entities.Models;
using Saas.Entities.Models.Branch;
using Saas.Entities.Models.Invoices.Header;
using Saas.Entities.Models.Invoices.Rows;
using Saas.Entities.Models.Products;
using Saas.Entities.Models.User;
using Saas.Entities.Models.UserClaims;
using System.Security.Principal;

namespace Saas.DataAccess.EntityFrameWorkCore.DbContexts;

public class GordionDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Cahatay Pc
        //  optionsBuilder.UseSqlServer(
        //@"Server=TRIST-LPF2RZWAY;Database=OfferManagement;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True").ConfigureWarnings(b => b.Ignore(RelationalEventId.AmbientTransactionWarning));

        //Sevda Pc
        optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=OfferManagement ;Integrated Security=true;MultipleActiveResultSets=true;TrustServerCertificate=True").ConfigureWarnings(b => b.Ignore(RelationalEventId.AmbientTransactionWarning)); ;

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //fluentApi
        #region Company

        //modelBuilder.Entity<Company>().Property(x => x.TaxNumber).HasMaxLength(11).IsRequired();
        //modelBuilder.Entity<Company>().Property(x => x.Deleted).HasDefaultValue(0);
        //modelBuilder.Entity<Company>().Property(x => x.FullName).IsRequired();

        #endregion


        #region CompanyUser 

        ////modelBuilder.Entity<CompanyUser>().Property(x => x.CompanyId).IsRequired();
        ////modelBuilder.Entity<CompanyUser>().Property(x => x.Email).IsRequired();
        //modelBuilder.Entity<CompanyUser>().Property(x => x.PassWordHash).IsRequired();
        //modelBuilder.Entity<CompanyUser>().Property(x => x.PassWordSalt).IsRequired();



        //modelBuilder.Entity<CompanyUserBranches>().Property(x => x.IsAdmin).HasDefaultValue(0);
        #endregion

        #region CompanyBranch 

        //modelBuilder.Entity<CompanyBranch>().Property(x => x.CompanyId).IsRequired();
        //modelBuilder.Entity<CompanyBranch>().Property(x => x.Deleted).HasDefaultValue(0);
        //modelBuilder.Entity<CompanyBranch>().Property(x => x.FullName).IsRequired();

        #endregion


        #region Roles

        //modelBuilder.Entity<CompanyOperationClaim>().Property(x => x.Name).IsRequired();

        //modelBuilder.Entity<CompanyOperationUserClaim>().Property(x => x.CompanyUserId).IsRequired();
        //modelBuilder.Entity<CompanyOperationUserClaim>().Property(x => x.CompanyOperationClaimId).IsRequired();


        #endregion
    }

    #region Company-User-Branch

    public DbSet<Company> Company { get; set; }

    public DbSet<CompanyUser> CompanyUser { get; set; }

    public DbSet<CompanyBranch> CompanyBranch { get; set; }

    public DbSet<CompanyUserBranches> CompanyUserBranches { get; set; }

    #endregion

    #region Roles

    public DbSet<CompanyOperationClaim> CompanyOperationClaim { get; set; }

    public DbSet<CompanyOperationUserClaim> CompanyOperationUserClaim { get; set; }

    #endregion

    public DbSet<Logs> Logs { get; set; }



    #region products-unit

    public DbSet<CompanyProducts> Products { get; set; }
    public DbSet<CompanyProductUnits> ProductUnits { get; set; }

    #endregion

    #region offer-rows

    public DbSet<CompanyOffer> CompanyOffer { get; set; }
    public DbSet<OfferRow> OfferRow { get; set; }
    #endregion

    public virtual void Save()
    {
        base.SaveChanges();
    }

    private string UserProvider
    {
        get
        {
            if (!string.IsNullOrEmpty(WindowsIdentity.GetCurrent().Name))
                return WindowsIdentity.GetCurrent().Name.Split('\\')[1];
            return string.Empty;
        }
    }
    #region default alanları insert-update

    public Func<DateTime> TimestampProvider { get; set; } = ()
        => DateTime.UtcNow;
    public override int SaveChanges()
    {
        TrackChanges();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        TrackChanges();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void TrackChanges()
    {
        foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
        {
            if (entry.Entity is IEntity)
            {
                var auditable = entry.Entity as BaseModel;
                if (entry.State == EntityState.Added)
                {
                    if (auditable != null)
                    {
                        if (auditable.CreatedBy == "" || auditable.CreatedBy.IsNullOrEmpty())
                            auditable.CreatedBy = UserProvider; //  
                        auditable.CreatedDate = TimestampProvider();
                        auditable.UpdatedDate = TimestampProvider();
                    }
                }
                else
                {
                    if (auditable != null)
                    {
                        if (auditable.CreatedBy == "" || auditable.CreatedBy.IsNullOrEmpty())
                            auditable.CreatedBy = UserProvider; //  
                        if (auditable.UpdatedBy == "")
                            auditable.UpdatedBy = UserProvider; //  
                        auditable.UpdatedDate = TimestampProvider();
                    }
                }
            }
        }
    }
    #endregion
}