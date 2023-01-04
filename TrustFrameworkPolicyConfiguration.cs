using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Trustme.MigrationConsole;

public class TrustFrameworkPolicyConfiguration : IEntityTypeConfiguration<TrustFrameworkPolicy>
{
  public void Configure(EntityTypeBuilder<TrustFrameworkPolicy> modelBuilder)
  {
    modelBuilder.ToTable("TrustFrameworkPolicies");
    modelBuilder.HasKey(e => e.DbKey);
    modelBuilder
      .HasMany(e => e.UserJourneys)
      .WithOne(e => e.Policy)
      .OnDelete(DeleteBehavior.Cascade);
    modelBuilder
      .HasMany(e => e.SubJourneys)
      .WithOne(e => e.Policy)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
