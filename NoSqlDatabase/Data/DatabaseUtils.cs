using NoSqlDatabase.Models;

namespace NoSqlDatabase.Data;

public static class DatabaseUtils
{
    private static readonly Dictionary<Type, string> _namesCollectionsFromEntities = new()
    {
        [typeof(WeatherForecast)] = "teste"
    };

    public static string GetNameCollection(Type type)
    {
        if (_namesCollectionsFromEntities.TryGetValue(type, out string? nameCollection))
        {
            return nameCollection;
        }

        throw new Exception("Unable to get the name of table.");
    }
}
