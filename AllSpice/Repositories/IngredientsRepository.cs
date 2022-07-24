using System.Collections.Generic;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{
    public class IngredientsRepository
    {

        private readonly IDbConnection _db;

        public IngredientsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Ingredient Create(Ingredient ingredientData)
        {
            string sql = @"
            INSERT INTO ingredients
            (name, quantity, recipeId)
            VALUES
            (@Name, @Quantity, @RecipeId);
            SELECT LAST_INSERT_ID();";

            int id = _db.ExecuteScalar<int>(sql, ingredientData);
            ingredientData.RecipeId = id;
            return ingredientData;
        }

        internal List<Ingredient> GetByRecipeId(int recipeId)
        {
            string sql = "SELECT * FROM ingredients WHERE recipeId = @recipeId";
            return _db.Query<Ingredient>(sql, new { recipeId }).ToList();
        }

        internal Ingredient GetByIngredientId(int ingredientId)
        {
            string sql = "SELECT * FROM ingredients WHERE Id = @ingredientId";
            return _db.QueryFirstOrDefault<Ingredient>(sql, new { ingredientId });
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM ingredients WHERE id = @id";
            _db.Execute(sql, new { id });
        }

        internal void Edit(Ingredient original)
        {
            string sql = @"
            UPDATE ingredients
            SET
            name = @Name,
            quantity = @Quantity
            WHERE id = @Id
            ";
            _db.Execute(sql, original);
        }
    }
}