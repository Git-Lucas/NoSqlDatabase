namespace NoSqlDatabase.Models;

public interface IEntity
{
    Guid Id => Guid.NewGuid();
}
