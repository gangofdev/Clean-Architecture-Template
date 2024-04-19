using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.SharedKernel.Common
{
    public class HostSettings
    {
        public HostDatabase Database { get; set; }

        public HostCache Cache { get; set; }
    }

    public enum HostDatabase
    {
         InMemory,
         SqlServer,
         Postgres,
    }

    public enum HostCache 
    {
        InMemory,
        Redis,
    }
}
