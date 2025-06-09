using Microsoft.AspNetCore.Mvc;

namespace ZooManagement.Controllers;

public abstract class BaseApiController : ControllerBase
{
    protected int DefaultPageSize { get; } = 10;

    protected int MaxPageSize { get; } = 50;

    protected BaseApiController()
    {
        DefaultPageSize = 10;
        MaxPageSize = 50;
    }

    protected int GetPageSize(int pageSize)
    {
        return pageSize > MaxPageSize ? MaxPageSize : pageSize;
    }

    protected int GetPageNumber(int pageNumber)
    {
        return pageNumber < 1 ? 1 : pageNumber;
    }
}