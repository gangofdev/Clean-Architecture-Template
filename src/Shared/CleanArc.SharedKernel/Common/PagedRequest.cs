using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.SharedKernel.Common
{
    public record PagedRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
