namespace fx.Infra.Data.MongDb
{
    using fx.Domain.core;
    using MongoDB.Bson;
    using MongoDB.Driver;
    public class EventDbContext
    {
        private readonly IMongoDatabase database;
        public EventDbContext()
        {
            var client = new MongoClient("mongodb://192.168.201.131:23117");
            database = client.GetDatabase("foo");
        }

        public IMongoCollection<T> DbSet<T>() where T : DomainEvent
        {
            return database.GetCollection<T>(nameof(DomainEvent));
        }
    }
}