using Bank.Interview.Application.Common;

namespace Bank.Interview.Application.Extensions
{
    public static class PaginationRequestExtension
    {
        public static Pagination CreatePagination(this PaginationRequest paginationRequest, int elementCount)
        {
            var pagination = new Pagination()
            {
                ElementCount = elementCount,
                PageCount = CalculatePageCount(elementCount, paginationRequest.PageSize),
                PageIndex = paginationRequest.PageIndex,
                PageSize = paginationRequest.PageSize,
            };

            return pagination;
        }

        private static int CalculatePageCount(int elementCount, int pageSize)
        {
            int remainder = elementCount % pageSize;

            int pageCount = remainder == 0
                ? elementCount / pageSize
                : elementCount / pageSize + 1;

            return pageCount;

        }
    }
}
