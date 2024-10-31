using Microsoft.AspNetCore.Mvc;
using ThePantry.Models.DTOs;
using ThePantry.Models.Extras;
using ThePantry.Services.Recipe;
//using ThePantry.Models.Recipe;

namespace ThePantry.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController(IRecipeService recipeService) : ControllerBase
{
  
        
    // Get: api/Recipes
    [HttpGet]
    public IActionResult GetAllRecipes()
    {
        var recipes = recipeService.GetAllRecipes();
        return Ok(recipes);
    }
    
    // Get api/Recipes/byCategory/{category}
    [HttpGet("byCategory/{category}")]
    public IActionResult GetRecipesByCategory(Categories.RecipeCategory category)
    {
        var recipes = recipeService.GetByCategory(category);
        if (recipes == null)
        {
            return NotFound();
        }
        return Ok(recipes);
    }
    
    // Get api/Recipes/byIngredient/{ingredientId}
    [HttpGet("byIngredient/{ingredientId}")]
    public IActionResult GetRecipesByIngredient(Guid ingredientId)
    {
        var recipes = recipeService.GetByIngredient(ingredientId);
        if (recipes == null)
        {
            return NotFound();
        }
        return Ok(recipes);
    }
    
    // Get api/Recipes/sortedByPrice
    [HttpGet("sortedByPrice")]
    public IActionResult GetRecipesByPrice()
    {
        var recipe = recipeService.SortByPrice();
        if (recipe == null)
        {
            return NotFound();
        }
        return Ok(recipe);
    }
    
    // Get api/Recipes/{id}
    [HttpGet("{id}")]
    public IActionResult GetRecipeById(Guid id)
    {
        var recipe = recipeService.GetById(id);
        if (recipe == null)
        {
            return NotFound();
        }
        return Ok(recipe);
    }

    // POST: api/Recipes
    [HttpPost]
    public IActionResult CreateRecipe([FromBody] RecipeDTO recipeDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // 400 if model validation fails
        }

        try
        {
            var recipe = recipeService.MapToRecipe(recipeDto);
            recipeService.AddRecipe(recipe);
            return CreatedAtAction(nameof(GetRecipeById), new { id = recipe.Id }, recipe);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while adding recipe: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }

    }
    
    // PUT: api/Recipes/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateRecipe(Guid id, [FromBody] RecipeDTO recipeDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var existingRecipe = recipeService.GetById(id);
        
        if (existingRecipe == null)
        {
            return NotFound();
        }
        
        var updatedRecipe = recipeService.MapToRecipe(recipeDto);
        updatedRecipe.Id = id; // Ensure the ID remains the same
        recipeService.UpdateRecipe(updatedRecipe);

        return NoContent(); // 204 No Content after successful update
    }
    
    // DELETE: api/Recipes/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteRecipe(Guid id)
    {
        RecipeDTO recipe = recipeService.GetById(id);
        if (recipe == null)
        {
            return NotFound();
        }

        var recipeToDelete = recipeService.MapToRecipe(recipe);
        recipeService.DeleteRecipe(recipeToDelete);
        return NoContent(); // 204 No Content after successful deletion
    }

    [HttpGet("totalPrice/{id}")]
    public ActionResult<decimal> GetTotalPriceForRecipe(Guid id)
    {
        RecipeDTO recipe = recipeService.GetById(id);

        if (recipe == null)
        {
            return NotFound();
        }

        var recipeToQuery = recipeService.MapToRecipe(recipe);
        var totalPrice = recipeService.CalculateTotalPriceForRecipe(recipeToQuery);
        return Ok(totalPrice); // This still can box but return type is explicit
    }

    [HttpGet("pricePerServing/{id}")]
    public ActionResult<decimal> GetPricePerServing(Guid id)
    {
        var recipe = recipeService.GetById(id);

        if (recipe == null)
        {
            return NotFound();
        }

        var recipeToQuery = recipeService.MapToRecipe(recipe);
        var pricePerServing = recipeService.CalculatePricePerServing(recipeToQuery);
        return Ok(pricePerServing); // This still can box but return type is explicit
    }


}