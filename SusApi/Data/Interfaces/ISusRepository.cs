using MongoDB.Bson;
using System.Collections.Generic;

namespace SusApi.Data.Interfaces
{
    public interface ISusRepository
    {
        IEnumerable<dynamic> GetAll(string modulo, string[] ufs, string[] anos, string[] meses, int? page = null, int? limit = null);
    }
}
