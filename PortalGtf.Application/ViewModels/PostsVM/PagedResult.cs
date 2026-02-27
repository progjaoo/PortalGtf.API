namespace PortalGtf.Application.ViewModels.PostsVM;

public class PagedResult<T>
{
    public List<T> Data { get; }
    public int TotalCount { get; }
    public int Page { get; }
    public int PageSize { get; }
    public int TotalPages => 
        (int)Math.Ceiling((double)TotalCount / PageSize);

    public PagedResult(List<T> data, int totalCount, int page, int pageSize)
    {
        Data = data;
        TotalCount = totalCount;
        Page = page;
        PageSize = pageSize;
    }
}