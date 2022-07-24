using System.Collections.Generic;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{
    public class StepsRepository
    {

        private readonly IDbConnection _db;

        public StepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Step> GetRecipeId(int recipeId)
        {
            string sql = "SELECT * FROM steps WHERE recipeId = @recipeId";
            return _db.Query<Step>(sql, new { recipeId }).ToList();
        }
    }
}