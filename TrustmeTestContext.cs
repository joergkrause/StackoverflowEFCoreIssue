using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System.Reflection;

namespace Trustme.MigrationConsole;

public class TrustmeTestContextFactory : IDesignTimeDbContextFactory<TrustmeTestContext>
{
  public TrustmeTestContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<TrustmeTestContext>();
    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TrustmeDbTestMigration;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", sqlServerOptionsAction: sqlOptions =>
    {
      sqlOptions.MigrationsAssembly(typeof(TrustmeTestContext).GetTypeInfo().Assembly.GetName().Name);
    });

    return new TrustmeTestContext(optionsBuilder.Options);
  }
}

public class TrustmeTestContext : DbContext
{
  public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(b => b.AddConsole());

  public TrustmeTestContext(DbContextOptions<TrustmeTestContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {

    modelBuilder.ApplyConfiguration<TrustFrameworkPolicy>(new TrustFrameworkPolicyConfiguration());

    // TrustFrameworkPolicy children

    modelBuilder.Entity<OrchestrationStepUserJourney>().ToTable("OrchestrationSteps");
    modelBuilder.Entity<OrchestrationStepSubJourney>().ToTable("OrchestrationSteps");

    modelBuilder.ApplyConfiguration<OrchestrationStep>(new OrchestrationStepConfiguration());

    modelBuilder.Entity<UserJourney>().ToTable("UserJourneys");
    modelBuilder.Entity<UserJourney>().HasKey(e => e.DbKey);
    modelBuilder.Entity<UserJourney>()
      .HasMany(e => e.OrchestrationSteps)
      .WithOne(e => e.Journey)
      .OnDelete(DeleteBehavior.ClientCascade);

    modelBuilder.Entity<SubJourney>().ToTable("SubJourneys");
    modelBuilder.Entity<SubJourney>().HasKey(e => e.DbKey);
    modelBuilder.Entity<SubJourney>()
      .HasMany(e => e.Candidates)
      .WithOne(e => e.SubJourney)
      .OnDelete(DeleteBehavior.NoAction);
    modelBuilder.Entity<SubJourney>()
      .HasMany(e => e.OrchestrationSteps)
      .WithOne(e => e.Journey)
      .IsRequired(false)
      .OnDelete(DeleteBehavior.ClientCascade);

    modelBuilder.Entity<Candidate>().ToTable("Candidates");
    modelBuilder.Entity<Candidate>().HasKey(e => e.DbKey);
    modelBuilder.Entity<Candidate>().Property(e => e.SubJourneyReferenceId).HasMaxLength(128).IsUnicode(false).IsRequired();
    modelBuilder.Entity<Candidate>()
      .HasOne(e => e.SubJourney)
      .WithMany(e => e.Candidates)
      .IsRequired()
      .OnDelete(DeleteBehavior.Restrict);



  }


}
