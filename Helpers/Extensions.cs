using Newtonsoft.Json;

namespace SmartSchool.WebAPI.Helpers;

public static class Extensions
{
    public static void AddPagination(this HttpResponse Response, int currentPage, int itemsPerPage, int totalItems, int totalPage)
    {
        var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPage);
        Response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader));
        Response.Headers.Add("Access-Control-Expose-Header", "Pagination");
    }
}
