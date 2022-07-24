using System;
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
    public class IngredientsController : ControllerBase
    {
        private readonly IngredientsService _ings;

        public IngredientsController(IngredientsService @ings)
        {
            _ings = @ings;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ingredient>> GetByIngredientId(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Ingredient ingredient = _ings.GetByIngredientId(id, userInfo.Id);
                return Ok(ingredient);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Ingredient>> DeleteAsync(int id)
        {
            Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
            Ingredient deletedIngredient = _ings.Delete(id, userInfo.Id);
            return Ok(deletedIngredient);
        }

        [HttpPut("{id}")]
        [Authorize]

        public async Task<ActionResult<Ingredient>> EditAsync(int id, [FromBody] Ingredient ingredientData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                ingredientData.Id = id;
                Ingredient update = _ings.Edit(ingredientData, userInfo.Id, id);
                return Ok(update);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost]

        public async Task<ActionResult<Ingredient>> Create([FromBody] Ingredient ingredientData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Ingredient ingredient = _ings.Create(ingredientData, userInfo.Id);
                return Ok(ingredient);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }


    }
}