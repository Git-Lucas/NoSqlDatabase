using NoSqlDatabase.Models;

namespace NoSqlDatabase.DTOs;

public class GetPagedResponse<T>(long countDataInDatabase, int skip, int take, IEnumerable<T> data) where T : BaseEntity
{
    public long Count { get; private set; } = countDataInDatabase;
    public int Skip { get; private set; } = skip;
    public int Take { get; private set; } = take;
    public int CurrentPage { get; private set; } = GetPagedResponse<T>.SetCurrentPage(skip, take);
    public long TotalPages { get; private set; } = GetPagedResponse<T>.SetTotalPages(countDataInDatabase, take);
    public IEnumerable<T> Results { get; private set; } = data;

    private static int SetCurrentPage(int skip, int take)
    {
        return skip / take + 1;
    }

    private static long SetTotalPages(long countDataInDatabase, int take)
    {
        return
            countDataInDatabase % take != 0 ?
            countDataInDatabase / take + 1 :
            countDataInDatabase / take;
    }
}