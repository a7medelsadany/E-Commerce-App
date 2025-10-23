using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class PagintedResult<TEntity>
    {
        public PagintedResult(int _pageSize,int _pageIndex,int _totalCount,IEnumerable<TEntity> _data)
        {
            PageSize = _pageSize;
            PageIndex = _pageIndex;
            TotalCount = _totalCount;
            Data = _data;
        }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<TEntity> Data { get; set; }
    }
}
