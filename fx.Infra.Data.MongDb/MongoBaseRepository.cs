using System;

namespace fx.Infra.Data.MongDb
{
    public class MongoBaseRepository : IMongoBaseRepository
    {
        public void DeleteById(string id)
        {
            throw new NotImplementedException();
        }

        public T GetById<T>(string id) where T : new()
        {
            throw new NotImplementedException();
        }

        public void InsertNewEntity<T>(T entity) where T : new()
        {
            throw new NotImplementedException();
        }

        public void UpdateEntity<T>(T entity) where T : new()
        {
            throw new NotImplementedException();
        }
    }
}