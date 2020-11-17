using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace SusApi.Data
{
    public class ContextMongoDb
    {

        public MongoClient Client { get; set; }
        public IMongoDatabase Database { get; set; }

        public ContextMongoDb()
        {
            var configuration = new ConfigurationBuilder()
                                    .AddEnvironmentVariables()
                                    .AddJsonFile("appsettings.json")
                                    .Build();

            string conn = configuration.GetConnectionString("MongoDb");
            var mongoUrl = new MongoUrl(conn);
            Client = new MongoClient(mongoUrl);
            Database = Client.GetDatabase(mongoUrl.DatabaseName);
        }
    }
}
