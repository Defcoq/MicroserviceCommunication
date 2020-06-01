using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.API.ResponseModels
{
    public class PaginatedBooksResponseModel<TEntity> where TEntity : class
    {

        public PaginatedBooksResponseModel(int pageIndex, int pageSize,long total, IEnumerable<TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize; Total = total;
            Data = data;
        }
        public int PageIndex { get; }
        public int PageSize { get; }
        public long Total { get; }
        public IEnumerable<TEntity> Data { get; }
    }
}
