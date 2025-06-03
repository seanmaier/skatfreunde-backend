namespace skat_back.Lib;

public class PagedResult<T>(IEnumerable<T> data, int pageNumber, int pageSize, int totalCount)
{
    public IEnumerable<T> Data { get; set; } = data;
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
    public int TotalCount { get; } = totalCount;
    private int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}