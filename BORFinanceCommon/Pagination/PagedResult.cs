using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceCommon.Pagination
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();

        public int TotalRecords { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalPages =>
            (int)Math.Ceiling((double)TotalRecords / PageSize);

        public bool HasPrevious => PageNumber > 1;

        public bool HasNext => PageNumber < TotalPages;
    }
}
