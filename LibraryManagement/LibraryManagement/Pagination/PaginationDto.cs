using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Pagination
{
    public class PaginationDto<T>
    {
        public PaginationDto(IQueryable<T> items,int currentPage,int itemsCount)
        {
            Items = items.Skip((currentPage - 1) * itemsCount).Take(itemsCount).ToList();
            TotalCount = (int)Math.Ceiling((decimal)(items.Count()/ itemsCount));
            HasNext = currentPage< TotalCount;
            HasPrevious = currentPage > 1;
            CurrentPage = currentPage;
            ItemsCount = itemsCount;
        }
        public IEnumerable<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int ItemsCount { get; set; }
        public int TotalCount { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
    }
}
