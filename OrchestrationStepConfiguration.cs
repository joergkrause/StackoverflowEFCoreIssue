using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Trustme.MigrationConsole;

public class OrchestrationStepConfiguration : IEntityTypeConfiguration<OrchestrationStep>
{
  public void Configure(EntityTypeBuilder<OrchestrationStep> modelBuilder)
  {
    modelBuilder.ToTable("OrchestrationSteps");
    modelBuilder.HasKey(e => e.DbKey);
    modelBuilder.Property<string>("OrchestrationStepJourney").HasMaxLength(32).IsUnicode(false).IsRequired();
    modelBuilder
      .HasDiscriminator<string>("OrchestrationStepJourney")
      .HasValue<OrchestrationStepUserJourney>(nameof(OrchestrationStepUserJourney))
      .HasValue<OrchestrationStepSubJourney>(nameof(OrchestrationStepSubJourney));
    modelBuilder
      .HasMany(e => e.JourneyList)
      .WithOne()
      .IsRequired()
      .OnDelete(DeleteBehavior.Cascade);
  }
}
