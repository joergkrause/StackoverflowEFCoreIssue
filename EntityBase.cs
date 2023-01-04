using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trustme.MigrationConsole;

public abstract class EntityBase
{
  [Key]
  public virtual Guid DbKey { get; set; }
}
