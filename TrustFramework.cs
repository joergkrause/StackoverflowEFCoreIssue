using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Trustme.MigrationConsole;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

[DisplayName("https://learn.microsoft.com/en-us/azure/active-directory-b2c/trustframeworkpolicy")]
public partial class TrustFrameworkPolicy: EntityBase
{
}

[DisplayName("https://learn.microsoft.com/en-us/azure/active-directory-b2c/trustframeworkpolicy")]
public partial class BasePolicy
{
}

public partial class Candidate : EntityBase
{
  public SubJourney SubJourney { get; set; }
}

public partial class UserJourney : EntityBase
{
  public TrustFrameworkPolicy Policy { get; set; }
}

public partial class SubJourney: EntityBase
{
  public TrustFrameworkPolicy Policy { get; set; }

  public ICollection<Candidate> Candidates { get; set; } = new HashSet<Candidate>();
}

public partial class OrchestrationStep: EntityBase
{
}

public partial class OrchestrationStepUserJourney: OrchestrationStep
{
  public UserJourney Journey { get; set; }
}

public partial class OrchestrationStepSubJourney: OrchestrationStep
{
  public SubJourney Journey { get; set; }
}

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.