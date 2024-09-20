using ThePantry.Models.DTOs;
using ThePantry.Models.Recipe;

namespace ThePantry.Services.Recipe;

public interface IRecipeService
{
    //Methods for calculated properties
    float CalculateTotalPriceForRecipe(IRecipe recipe);
    float CalculatePricePerServing(IRecipe recipe);
    
    // DTO Mapping
    RecipeDTO MapToDTO(Models.Recipe.Recipe recipe);
    IRecipe MapToRecipe(RecipeDTO recipeDTO);
    
    // Additional Methods
    string ConvertInstructionsToJson(IRecipe recipe);
    string RecipeToString(IRecipe recipe);
}