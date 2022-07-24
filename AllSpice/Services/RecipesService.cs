using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
    public class RecipesService
    {

        private readonly RecipesRepository _repo;

        public RecipesService(RecipesRepository repo)
        {
            _repo = repo;
        }

        internal List<Recipe> GetAll(string userId)
        {
            return _repo.GetAll(userId);
        }

        internal Recipe Create(Recipe recipeData)
        {
            return _repo.Create(recipeData);
        }

        internal Recipe GetById(int recipeId, string userId)
        {
            Recipe found = _repo.GetById(recipeId);
            if (found == null)
            {
                throw new Exception("Bad Id");
            }
            return found;
        }

        internal Recipe Edit(Recipe recipeData)
        {
            Recipe original = _repo.GetById(recipeData.Id);
            if (recipeData.CreatorId != original.CreatorId)
            {
                throw new Exception("Not your Recipe");
            }
            original.Picture = recipeData.Picture ?? original.Picture;
            original.Title = recipeData.Title ?? original.Title;
            original.Subtitle = recipeData.Subtitle ?? original.Subtitle;
            original.Category = recipeData.Category ?? original.Category;

            _repo.Edit(original);
            return original;

        }

        internal Recipe Delete(int recipeId, string userId)
        {
            Recipe original = _repo.GetById(recipeId);
            if (userId != original.CreatorId)
            {
                throw new Exception("Not your Recipe to Delete");
            }
            _repo.Delete(recipeId);
            return original;
        }
    }
}