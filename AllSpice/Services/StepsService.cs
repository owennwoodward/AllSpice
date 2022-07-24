using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
    public class StepsService
    {

        private readonly RecipesService _rs;
        private readonly StepsRepository _repo;

        public StepsService(RecipesService rs, StepsRepository repo)
        {
            _rs = rs;
            _repo = repo;
        }

        internal List<Step> GetByRecipeId(int recipeId, string userId)
        {
            _rs.GetById(recipeId, userId);
            return _repo.GetRecipeId(recipeId);
        }
    }
}