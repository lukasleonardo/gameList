namespace gameList.Models
{
    public class PagedList<T>
    {
        public List<T> Items { get; set; }
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int PageCount => (int)Math.Ceiling(TotalItems / (double)PageSize);

        public bool HasPreviousPage
        {
            get { return PageNumber > 1; }
        }

        public bool HasNextPage
        {
            get { return PageNumber < PageCount; }
        }
    }
}
