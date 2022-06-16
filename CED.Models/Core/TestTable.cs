using EasyMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Models.Core
{
  [Name("test_table")]
  public class TestTable
  {
    [Pk(Clustered = false)] Guid Uuid;
    [NotNull, Index] string Name;
    [NotNull, Index] string LastName;
  }
}
