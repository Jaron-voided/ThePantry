using Microsoft.AspNetCore.Mvc;
using ThePantry.Models.DTOs;
using ThePantry.Services.Recipe;
using ThePantry.Models.Recipe;

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
        
        var recipe = recipeService.MapToRecipe(recipeDto);
        recipeService.AddRecipe(recipe);
        
        return CreatedAtAction(nameof(GetRecipeById), new { id = recipe.Id }, recipe);
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
}