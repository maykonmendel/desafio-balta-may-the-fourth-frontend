namespace MayTheFourth.Models;

public class PagedResult<T>
{
    public int Total { get; set; }
    public int CurrentPage { get; set; }
    public int Take { get; set; }
    public List<T> Result { get; set; } = new List<T>();
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)Total / PageSize);

    public static PagedResult<T> ToPagedResult(List<T> items, int pageNumber, int pageSize)
    {
        var count = items.Count;
        var startIndex = (pageNumber - 1) * pageSize;
        var endIndex = Math.Min(startIndex + pageSize, count);
        var pagedItems = items.GetRange(startIndex, endIndex - startIndex);

        return new PagedResult<T>
        {
            Total = count,
            CurrentPage = pageNumber,
            Take = pagedItems.Count,
            Result = pagedItems,
            PageSize = pageSize
        };
    }
}