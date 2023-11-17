using System;
using System.Collections.Generic;
using System.Linq;

namespace StockControl.Shared.Models
{
    public class PaginatedResponse<T>
    {
        public List<T> Results { get; set; }
        public string ErrorMessage { get; set; }
        public int CurrentPage { get; private set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }

        public PaginatedResponse() { }

        public PaginatedResponse(List<T> results, int count, int currentPage, int pageSize, string errorMessage = "")
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                TotalItems = count;
                TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                CurrentPage = currentPage;
                Results = new List<T>();
                Results.AddRange(results);
            }
            ErrorMessage = errorMessage;
        }

        public static PaginatedResponse<T> ToPaginatedResponse(IQueryable<T> source, int currentPage, int pageSize)
        {
            var count = source.Count();
            var results = source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedResponse<T>(results, count, currentPage, pageSize, null);
        }
    }
}