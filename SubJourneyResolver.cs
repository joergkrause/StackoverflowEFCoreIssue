// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json.Serialization;
using Trustme.MigrationConsole;

internal class SubJourneyResolver : IReferenceResolver
{

  private readonly IDictionary<string, SubJourney> _sjCache = new Dictionary<string, SubJourney>();

  public void AddReference(object context, string reference, object value)
  {
    if (value is SubJourney sj)
    {
      var id = reference;
      if (!_sjCache.ContainsKey(id))
      {
        _sjCache.Add(id, sj);
      }
    }
  }

  public string GetReference(object context, object value)
  {
    if (value is SubJourney sj)
    {
      _sjCache[sj.Id] = sj;
      return sj.Id;
    }
    return null;
  }

  public bool IsReferenced(object context, object value)
  {
    if (value is SubJourney sj)
    {
      return _sjCache.ContainsKey(sj.Id);
    }
    return false;
  }

  public object ResolveReference(object context, string reference)
  {
    var id = reference;
    _sjCache.TryGetValue(id, out var sj);
    return sj;
  }
}