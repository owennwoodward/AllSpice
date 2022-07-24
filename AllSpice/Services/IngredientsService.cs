using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
    public class IngredientsService
    {
        private readonly RecipesService _rs;

        private readonly IngredientsRepository _repo;
        public IngredientsService(RecipesService rs, IngredientsRepository repo)
        {
            _rs = rs;
            _repo = repo;
        }

        internal Ingredient Create(Ingredient ingredientData, string userId)
        {
            _rs.GetById(ingredientData.RecipeId, userId);
            return _repo.Create(ingredientData);
        }

        internal List<Ingredient> GetByRecipeId(int recipeId, string userId)
        {
            _rs.GetById(recipeId, userId);
            return _repo.GetByRecipeId(recipeId);
        }

        internal Ingredient GetByIngredientId(int ingredientId, string userId)
        {
            Ingredient found = _repo.GetByIngredientId(ingredientId);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            // if ( != userId)
            // {
            //     throw new Exception("Not your Ingredient");
            // }
            return found;
        }

        internal Ingredient Delete(int id, string userId)
        {
            Ingredient original = _repo.GetByIngredientId(id);
            Recipe found = _rs.GetById(original.RecipeId, userId);
            if (original == null)
            {
                throw new Exception("This does not Exist");
            }
            if (found.CreatorId != userId)
            {
                throw new Exception("You are not the creator");
            }

            _repo.Delete(id);
            return original;
        }

        internal Ingredient Edit(Ingredient ingredientData, string userId, int id)
        {
            Ingredient original = _repo.GetByIngredientId(id);
            Recipe found = _rs.GetById(original.RecipeId, userId);
            if (original == null)
            {
                throw new Exception("This does not Exist");
            }
            if (found.CreatorId != userId)
            {
                throw new Exception("You are not the creator");
            }
            original.Name = ingredientData.Name ?? original.Name;
            original.Quantity = ingredientData.Quantity ?? original.Quantity;


            _repo.Edit(original);
            return original;
        }
    }
}