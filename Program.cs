// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Trustme.MigrationConsole;

Console.WriteLine("Test DB Schema");
var options = new DbContextOptionsBuilder<TrustmeTestContext>();
options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TrustmeDbTestMigration;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", sqlServerOptionsAction: sqlOptions =>
{
  sqlOptions.MigrationsAssembly(typeof(TrustmeTestContext).GetTypeInfo().Assembly.GetName().Name);
  sqlOptions.EnableRetryOnFailure(
  maxRetryCount: 5,
  maxRetryDelay: TimeSpan.FromSeconds(30),
  errorNumbersToAdd: null);
});
//options.UseValidationCheckConstraints();
//options.UseEnumCheckConstraints(); 
options.UseLoggerFactory(TrustmeTestContext.loggerFactory).EnableSensitiveDataLogging();

using var context = new TrustmeTestContext(options.Options);
Console.WriteLine("Re-create db");
context.Database.EnsureDeleted();
Console.WriteLine("Migrate");
context.Database.Migrate();
Console.WriteLine("Write to db");

#region Working

var sj = new SubJourney
{
  Type = SubJourneyTYPE.Transfer
};

//var sfguid = Guid.NewGuid();

//var trustFrameworkPolicy = new TrustFrameworkPolicy
//{
//  UserJourneys = new List<UserJourney>
//  {
//    new UserJourney
//    {
//      AssuranceLevel = "1",
//      Id = "Journey1",
//      OrchestrationSteps = new List<OrchestrationStepUserJourney>
//      {
//        new OrchestrationStepUserJourney
//        {
//          JourneyList = new List<Candidate> {
//            new Candidate
//            {
//              SubJourneyReferenceId = "Test",
//              SubJourney = sj
//            }
//          }
//        }
//      }
//    }
//  },

//  SubJourneys = new List<SubJourney>
//  {
//    sj
//  }
//};

#endregion

#region Not Working

// Unhandled exception. System.InvalidOperationException: The instance of entity type 'SubJourney' cannot be tracked because another instance with the key value '{DbKey: d44948dc-d514-4928-abea-3450150c26c4}' is already being tracked. When attaching existing entities, ensure that only one entity instance with a given key value is attached.

//var guid = Guid.NewGuid();

//var sj1 = new SubJourney
//{
//  DbKey = guid,
//  Type = SubJourneyTYPE.Transfer
//};

//var sj2 = new SubJourney
//{
//  DbKey = guid,
//  Type = SubJourneyTYPE.Transfer
//};

//var sfguid = Guid.NewGuid();

//var trustFrameworkPolicy = new TrustFrameworkPolicy
//{
//  UserJourneys = new List<UserJourney>
//  {
//    new UserJourney
//    {
//      AssuranceLevel = "1",
//      Id = "Journey1",
//      OrchestrationSteps = new List<OrchestrationStepUserJourney>
//      {
//        new OrchestrationStepUserJourney
//        {
//          JourneyList = new List<Candidate> {
//            new Candidate
//            {
//              SubJourneyReferenceId = "Test",
//              SubJourney = sj1
//            }
//          }
//        }
//      }
//    }
//  },

//  SubJourneys = new List<SubJourney>
//  {
//    sj2
//  }
//};

#endregion

#region Not Working

/* An exception occurred in the database while saving changes for context type 'Trustme.MigrationConsole.TrustmeTestContext'.
Microsoft.EntityFrameworkCore.DbUpdateException: An error occurred while saving the entity changes. See the inner exception for details.
       ---> Microsoft.Data.SqlClient.SqlException (0x80131904): The INSERT statement conflicted with the FOREIGN KEY constraint "FK_SubJourneys_TrustFrameworkPolicies_PolicyDbKey". The conflict occurred in database "TrustmeDbTestMigration", table "dbo.TrustFrameworkPolicies", column 'DbKey'.
      The INSERT statement conflicted with the FOREIGN KEY constraint "FK_Candidates_SubJourneys_SubJourneyDbKey".
*/

var guid = Guid.NewGuid();

var sj1 = new SubJourney
{
  DbKey = Guid.NewGuid(),
  Type = SubJourneyTYPE.Transfer
};

var sj2 = new SubJourney
{
  DbKey = Guid.NewGuid(),
  Type = SubJourneyTYPE.Transfer
};

var sfguid = Guid.NewGuid();

var trustFrameworkPolicy = new TrustFrameworkPolicy
{
  UserJourneys = new List<UserJourney>
  {
    new UserJourney
    {
      AssuranceLevel = "1",
      Id = "Journey1",
      OrchestrationSteps = new List<OrchestrationStepUserJourney>
      {
        new OrchestrationStepUserJourney
        {
          JourneyList = new List<Candidate> {
            new Candidate
            {
              SubJourneyReferenceId = "Test",
              SubJourney = sj1
            }
          }
        }
      }
    }
  },

  SubJourneys = new List<SubJourney>
  {
    sj2
  }
};

#endregion

context.Set<TrustFrameworkPolicy>().Add(trustFrameworkPolicy);
context.SaveChanges();

Console.WriteLine("Done");