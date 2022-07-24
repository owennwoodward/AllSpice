using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AllSpice.Models;
using AllSpice.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllSpice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly RecipesService _rs;
        private readonly IngredientsService _ings;
        private readonly StepsService _ss;

        public RecipesController(RecipesService rs, IngredientsService ings, StepsService ss)
        {
            _rs = rs;
            _ings = ings;
            _ss = ss;
        }

        [HttpGet]
        public async Task<ActionResult<List<Recipe>>> Get(string query = "")
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                List<Recipe> recipes = _rs.GetAll(userInfo.Id);

                return Ok(recipes);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetById(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Recipe recipe = _rs.GetById(id, userInfo.Id);

                return Ok(recipe);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}/ingredients")]
        public async Task<ActionResult<List<Ingredient>>> GetIngredients(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                List<Ingredient> ingredients = _ings.GetByRecipeId(id, userInfo.Id);
                return Ok(ingredients);

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/steps")]
        public async Task<ActionResult<List<Step>>> GetSteps(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                List<Step> steps = _ss.GetByRecipeId(id, userInfo.Id);
                return Ok(steps);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Recipe>> CreateAsync([FromBody] Recipe recipeData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                recipeData.CreatorId = userInfo.Id;
                Recipe newRecipe = _rs.Create(recipeData);
                newRecipe.Creator = userInfo;
                newRecipe.CreatedAt = new DateTime();
                newRecipe.UpdatedAt = new DateTime();
                return Ok(newRecipe);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]

        public async Task<ActionResult<Recipe>> EditASync(int id, [FromBody] Recipe recipeData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                recipeData.Id = id;
                recipeData.CreatorId = userInfo.Id;
                Recipe update = _rs.Edit(recipeData);
                return Ok(update);

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]

        public async Task<ActionResult<Recipe>> DeleteAsync(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Recipe deletedRecipe = _rs.Delete(id, userInfo.Id);
                return Ok(deletedRecipe);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }


    }
}