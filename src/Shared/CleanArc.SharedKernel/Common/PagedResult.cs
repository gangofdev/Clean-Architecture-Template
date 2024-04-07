using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.SharedKernel.Common
{
    public class PagedResult<T>
    {
        public List<T> Page { get; set; }
        public int TotalCount { get; set; }
        public int PageCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public PagedResult() { }
        public PagedResult(List<T> page, int totalCount, int pageCount, int pageIndex, int pageSize)
        {
            Page = page;
            TotalCount = totalCount;
            PageCount = pageCount;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
