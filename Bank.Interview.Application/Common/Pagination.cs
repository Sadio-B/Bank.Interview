namespace Bank.Interview.Application.Common
{
    public class Pagination
    {
        public int ElementCount { get; set; }

        public int PageCount { get; set; }

        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}
