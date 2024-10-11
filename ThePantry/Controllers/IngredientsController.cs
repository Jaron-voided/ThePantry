using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ThePantry.Models.DTOs;
using ThePantry.Services.Ingredient;
using ThePantry.Models.Ingredient;

namespace ThePantry.Controllers;

[EnableCors("AllowReactApp")]
[ApiController]
[Route("api/[controller]")]
public class IngredientsController(IIngredientService ingredientService) : ControllerBase
{
    // Get: api/Ingredients
    [HttpGet]
    public IActionResult GetAllIngredients()
    {
        var ingredients = ingredientService.GetAllIngredients();
        return Ok(ingredients);
    }
    
    // Get api/Ingredients/{id}
    [HttpGet("{id}")]
    public IActionResult GetIngredientById(Guid id)
    {
        var ingredient = ingredientService.GetById(id);
        if (ingredient == null)
        {
            return NotFound();
        }

        return Ok(ingredient);
    }

    // POST: api/Ingredients
    [HttpPost]
    public IActionResult CreateIngredient([FromBody] IngredientDTO ingredientDto)
    {
        // Debug: log the received data
        Console.WriteLine($"Received Ingredient: {ingredientDto.Name}, {ingredientDto.MeasuredIn}, {ingredientDto.IngredientCategory}");
        Console.WriteLine($"Received measuredIn: {ingredientDto.MeasuredIn}");
        Console.WriteLine($"Received ingredientCategory: {ingredientDto.IngredientCategory}");

        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // 400 if model validation fails
        }

        try
        {
            var ingredient = ingredientService.MapToIngredient(ingredientDto);
            ingredientService.AddIngredient(ingredient);
            return CreatedAtAction(nameof(GetIngredientById), new { id = ingredient.Id }, ingredient);
        }
        catch (Exception ex)
        {
            // Log the exception for debugging
            Console.WriteLine($"Error occurred while adding ingredient: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
    
    // PUT: api/Ingredients/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateIngredient(Guid id, [FromBody] IngredientDTO ingredientDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingIngredient = ingredientService.GetById(id);
        if (existingIngredient == null)
        {
            return NotFound();
        }
        
        var updatedIngredient = ingredientService.MapToIngredient(ingredientDto);
        updatedIngredient.Id = id; // Ensure the ID remains the same
        ingredientService.UpdateIngredient(updatedIngredient);

        return NoContent(); // 204 No Content after successful update
    }
    
    // DELETE: api/Ingredients/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteIngredient(Guid id)
    {
        IngredientDTO ingredient = ingredientService.GetById(id);
        if (ingredient == null)
        {
            return NotFound();
        }

        var ingredientToDelete = ingredientService.MapToIngredient(ingredient);
        ingredientService.DeleteIngredient(ingredientToDelete);
        return NoContent(); // 204 No Content after successful deletion
    }
}