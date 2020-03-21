using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CAM.Core.SharedKernel
{
    /// <summary>
    /// Data structure that contains all pagination concerns and validation within it. If pageIndex is invalid given
    /// the page count, it returns the first page. If ipp is less than 1, returns a default of 10.
    /// <summary>
    public class PaginatedList<T> : List<T>
    {
        public PaginatedList(List<T> items, int count, int pageIndex, int ipp)
        {
            var pageTotal = (int)Math.Ceiling(count / (double)ipp);
            PageTotal = pageTotal > 0 ? pageTotal : 1;
            PageIndex = PageInRange(pageIndex) ? pageIndex : 1;
            ItemIndex = count > 0 ? ipp * (pageIndex - 1) + 1 : 0;
            ItemsPerPage = ipp > 0 ? ipp : 10;
            ItemTotal = count;
            ItemEndIndex = (ItemIndex + ItemsPerPage - 1) > ItemTotal ? ItemTotal : (ItemIndex + ItemsPerPage - 1);
            this.AddRange(items);
        }
        public int PageIndex { get; private set; }
        public int PageTotal { get; private set; }
        public int ItemIndex { get; private set; }
        public int ItemsPerPage { get; private set; }
        public int ItemTotal { get; private set; }
        public int ItemEndIndex { get; private set; }
        public int[] ControlPanelPageNumbers
        {
            get
            {
                if (PageIndex % 2 == 0)
                    return Enumerable.Range(PageIndex - 1, 3).ToArray();
                return Enumerable.Range(PageIndex, 3).ToArray();
            }
        }
        public bool HasPrevPage
        {
            get => PageInRange(PageIndex - 1);
        }
        public bool HasNextPage
        {
            get => PageInRange(PageIndex + 1);
        }
        public string StateMessage
        {
            get => $"Showing {ItemIndex} to {ItemEndIndex} of {ItemTotal} entries.";
        }
        public bool PageInRange(int index) => (index > 0 && index <= PageTotal) ? true : false;
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int ipp)
        {
            var count = await source.CountAsync();
            var trimmed = await source.Skip((pageIndex - 1) * ipp).Take(ipp).ToListAsync();
            return new PaginatedList<T>(trimmed, count, pageIndex, ipp);
        }
    }
}