namespace PiecykPolHurt.Model.Queries
{
    public abstract class ListQuery
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
