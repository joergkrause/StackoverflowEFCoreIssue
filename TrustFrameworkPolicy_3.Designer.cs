// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code++. Version 6.0.74.0. www.xsd2code.com
//    {"TargetFramework":"Net48","NameSpace":"Trustme.Data","Properties":{},"XmlAttribute":{"Enabled":true},"ClassParams":{},"Miscellaneous":{}}
//  </auto-generated>
// ------------------------------------------------------------------------------
#pragma warning disable
namespace Trustme.MigrationConsole;

using System;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;
using System.Xml;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public partial class TrustFrameworkPolicy
{
  #region Private fields
  private List<UserJourney> _userJourneys;
  private List<SubJourney> _subJourneys;
  #endregion

  public TrustFrameworkPolicy()
  {
    _subJourneys = new List<SubJourney>();
    _userJourneys = new List<UserJourney>();
  }
  
  [XmlArrayItemAttribute(IsNullable = false)]
  public List<UserJourney> UserJourneys
  {
    get
    {
      return _userJourneys;
    }
    set
    {
      _userJourneys = value;
    }
  }

  [XmlArrayItemAttribute(IsNullable = false)]
  public List<SubJourney> SubJourneys
  {
    get
    {
      return _subJourneys;
    }
    set
    {
      _subJourneys = value;
    }
  }

}

public partial class SubJourney
{
  #region Private fields
  private List<OrchestrationStepSubJourney> _orchestrationSteps;
  private string _id;
  private SubJourneyTYPE _type;
  #endregion

  public SubJourney()
  {
    _orchestrationSteps = new List<OrchestrationStepSubJourney>();
  }

  public List<OrchestrationStepSubJourney> OrchestrationSteps
  {
    get
    {
      return _orchestrationSteps;
    }
    set
    {
      _orchestrationSteps = value;
    }
  }

  public string Id
  {
    get
    {
      return _id;
    }
    set
    {
      _id = value;
    }
  }

  public SubJourneyTYPE Type
  {
    get
    {
      return _type;
    }
    set
    {
      _type = value;
    }
  }

  public override int GetHashCode()
  {
    return 0;
  }

  public override bool Equals(object obj)
  {
    return true;
  }

}

public partial class OrchestrationStep
{
  #region Private fields
  private List<Candidate> _journeyList;
  private int _order;
  private OrchestrationStepType _type;
  private string _contentDefinitionReferenceId;
  private string _cpimIssuerTechnicalProfileReferenceId;
  #endregion

  public OrchestrationStep()
  {
    _journeyList = new List<Candidate>();
  }

  // according to docs is just one
  public List<Candidate> JourneyList
  {
    get
    {
      return _journeyList;
    }
    set
    {
      _journeyList = value;
    }
  }

  public OrchestrationStepType Type
  {
    get
    {
      return _type;
    }
    set
    {
      _type = value;
    }
  }

}

public partial class Candidate
{
  #region Private fields
  private string _subJourneyReferenceId;
  #endregion

  [XmlAttribute]
  public string SubJourneyReferenceId
  {
    get
    {
      return _subJourneyReferenceId;
    }
    set
    {
      _subJourneyReferenceId = value;
    }
  }
}

public enum OrchestrationStepType
{
  ConsentScreen,
  ClaimsProviderSelection,
  CombinedSignInAndSignUp,
  ClaimsExchange,
  ReviewScreen,
  SendClaims,
  GetClaims,
  UserDialog,
  InvokeSubJourney,
  Noop,
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.9032.0")]
[Serializable]
[XmlTypeAttribute(Namespace = "http://schemas.microsoft.com/online/cpim/schemas/2013/06")]
public enum SubJourneyTYPE
{
  Transfer,
  Call,
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.9032.0")]
[Serializable]
[DebuggerStepThrough]
[DesignerCategoryAttribute("code")]
[XmlTypeAttribute(Namespace = "http://schemas.microsoft.com/online/cpim/schemas/2013/06")]
public partial class UserJourney
{
  #region Private fields
  private string _assuranceLevel;
  private bool _preserveOriginalAssertion;
  private List<OrchestrationStepUserJourney> _orchestrationSteps;
  private string _id;
  private bool _nonInteractive;
  private string _defaultCpimIssuerTechnicalProfileReferenceId;
  #endregion

  public UserJourney()
  {
    _orchestrationSteps = new List<OrchestrationStepUserJourney>();
  }

  public string AssuranceLevel
  {
    get
    {
      return _assuranceLevel;
    }
    set
    {
      _assuranceLevel = value;
    }
  }

  public List<OrchestrationStepUserJourney> OrchestrationSteps
  {
    get
    {
      return _orchestrationSteps;
    }
    set
    {
      _orchestrationSteps = value;
    }
  }

  public string Id
  {
    get
    {
      return _id;
    }
    set
    {
      _id = value;
    }
  }

}
#pragma warning restore
