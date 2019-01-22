namespace fx.Infra.Data.MongDb
{

    public interface IMongoBaseRepository
    {
        void InsertNewEntity<T>(T entity) where T : new();
        void UpdateEntity<T>(T entity) where T : new();
        void DeleteById(string id);
        T GetById<T>(string id) where T : new();
    }
}