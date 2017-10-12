using System.Data.Entity;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing;
using Microsoft.AspNet.Identity.EntityFramework;


namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Context
{
    public class EptsContext:  IdentityDbContext<ApplicationUser>
    {
        public EptsContext()
            : base("EPTSContext", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public static EptsContext Create()
        {
            return  new EptsContext();
        }
        public DbSet<BusinessUnit> BusinessUnits { get; set; }
        public DbSet<Family> Family { get; set; }
        public DbSet<Model> Model { get; set; }
        public DbSet<PartNumber> PartNumber { get; set; }
        public DbSet<ModelDetail> ModelDetail { get; set; }
        public DbSet<Line> Line { get; set; }
        public DbSet<Station> Station { get; set; }
        public DbSet<StationType> StationType { get; set; }

        public DbSet<StationGroup> StationGroups { get; set; }
        public DbSet<Test> Test { get; set; }
        public DbSet<TestGroup> TestGroup { get; set; }
        public DbSet<TestPlan> TestPlan { get; set; }
        public DbSet<TestType> TestType { get; set; }
        public DbSet<TestUnit> TestUnit { get; set; }

        public DbSet<TestResult> TestResult { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Student>()
            //.HasOptional<Standard>(s => s.Standard)
            //.WithMany()
            //.WillCascadeOnDelete(false);
        }
    }
}