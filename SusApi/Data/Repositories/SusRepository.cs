using MongoDB.Bson;
using MongoDB.Driver;
using SusApi.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SusApi.Data.Repositories
{
    public class SusRepository : ISusRepository
    {
        protected readonly ContextMongoDb _context;
        protected readonly IMongoCollection<BsonDocument> Collection;
        public SusRepository(ContextMongoDb context)
        {
            _context = context;
        }

        public IEnumerable<dynamic> GetAll(string modulo, string[] ufs, string[] anos, string[] meses = null, int? page = null, int? limit = null)
        {
            ufs.ToList().ForEach(uf => uf = uf.ToUpper());
            var collection = _context.Database.GetCollection<dynamic>(modulo);
            var UfFilter = ufs == null || !ufs.Any() ? Builders<dynamic>.Filter.Empty : Builders<dynamic>.Filter.In("uf", ufs);
            var anoFilter = anos == null || !anos.Any() ? Builders<dynamic>.Filter.Empty : Builders<dynamic>.Filter.In("ano", anos);
            var mesesFilter = meses == null || !meses.Any() ? Builders<dynamic>.Filter.Empty : Builders<dynamic>.Filter.In("mes", meses);
            var find = collection.Find(UfFilter & anoFilter & mesesFilter);

            if (page.HasValue && limit.HasValue)
            {
                return find.Skip((page.Value - 1) * limit.Value)
                                 .Limit(limit)
                                 .ToEnumerable();
            }

            return collection.Find(UfFilter & anoFilter & mesesFilter).ToEnumerable();
        }
    }
}
