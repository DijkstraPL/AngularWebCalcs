using Build_IT_DataAccess.DeadLoads.Entities;
using Build_IT_DataAccess.Projects.Entites;
using Build_IT_DataAccess.ScriptInterpreter.Entities;
using Build_IT_DataAccess.ScriptInterpreter.Entities.Translations;
using Build_IT_DataAccess.SnowLoads.Entities;
using Build_IT_DataAccess.SteelProfiles.Entities;
using Build_IT_DataAccess_SqlServer;
using Build_IT_DataAccess_SqlServer.DeadLoads.Interfaces;
using Build_IT_DataAccess_SqlServer.Projects.Interfaces;
using Build_IT_DataAccess_SqlServer.ScriptInterpreter.Interfaces;
using Build_IT_DataAccess_SqlServer.SnowLoads.Interfaces;
using Build_IT_DataAccess_SqlServer.SteelProfiles.Interfaces;
using Build_IT_WebApplication.Identity;
using Build_IT_WebInfrastructure.Common;
using Build_IT_WebInfrastructure.Identity;
using Build_IT_WebInfrastructure.Persistence.Interceptors;
using Duende.IdentityServer.EntityFramework.Options;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebInfrastructure.Persistence
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>,
        IApplicationDbContext, IProjectsDbContext, IDeadLoadsDbContext, IScriptInterpreterDbContext, ISteelProfilesDbContext,
        ISnowLoadsDbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectDeadLoad> ProjectDeadLoad { get; set; }
        public DbSet<DeadLoadLayer> DeadLoadLayers { get; set; }
        public DbSet<DeadLoad> DeadLoads { get; set; }
        public DbSet<Designer> Designers { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<ParameterInput> ParameterInputs { get; set; }
        public DbSet<ProjectDesignerClaim> ProjectDesignerClaims { get; set; }
        public DbSet<ProjectScript> ProjectScriptInput { get; set; }
        public DbSet<UserCompany> UserCompanies { get; set; }
        public DbSet<Company> Companies { get; set; }

        public DbSet<ProfileType> ProfileTypes { get; set; }

        public DbSet<Script> Scripts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ParameterGroup> Groups { get; set; }
        public DbSet<Build_IT_DataAccess.ScriptInterpreter.Entities.Parameter> Parameters { get; set; }
        public DbSet<Figure> Figures { get; set; }
        public DbSet<Build_IT_DataAccess.ScriptInterpreter.Entities.Unit> Units { get; set; }

        public DbSet<ScriptTranslation> ScriptsTranslations { get; set; }
        public DbSet<ScriptGroupTranslation> ScriptGroupsTranslations { get; set; }
        public DbSet<ParameterGroupTranslation> ParameterGroupsTranslations { get; set; }
        public DbSet<ParameterTranslation> ParametersTranslations { get; set; }
        public DbSet<ValueOptionTranslation> ValueOptionsTranslations { get; set; }
        public DbSet<UnitTranslation> UnitTranslations { get; set; }

        public DbSet<TestData> TestDatas { get; set; }
        public DbSet<Assertion> Assertions { get; set; }
        public DbSet<TestParameter> TestParameters { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Addition> Additions { get; set; }
        public DbSet<MaterialAddition> MaterialAdditions { get; set; }

        public DbSet<SnowLoad> SnowLoads { get; set; }
        public DbSet<CylindricalRoof> CylindricalRoofs { get; set; }
        public DbSet<DriftingAtProjectionsObstructions> DriftingsAtProjectionsObstructions { get; set; }
        public DbSet<MultiSpanRoof> MultiSpanRoofs { get; set; }
        public DbSet<MonopitchRoof> MonopitchRoofs { get; set; }
        public DbSet<PitchedRoof> PitchedRoofs { get; set; }
        public DbSet<RoofAbuttingToTallerConstruction> RoofsAbuttingToTallerConstructions { get; set; }
        public DbSet<Snowguards> Snowguards { get; set; }
        public DbSet<SnowOverhanging> SnowOverhangings { get; set; }
        public DbSet<ProjectSnowLoad> ProjectsSnowLoads { get; set; }

        public DbSet<ApplicationUser> Users { get; set; }

        private readonly IMediator _mediator;
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            IMediator mediator,
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
            : base(options, operationalStoreOptions)
        {
            _mediator = mediator;
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(IDbContext)));

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEvents(this);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
