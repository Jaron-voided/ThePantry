using ThePantry.Models.DTOs;
using ThePantry.Models.Extras;
using ThePantry.Models.Measurement;
using ThePantry.Models.Recipe;

namespace ThePantry.Services.Recipe;

public interface IRecipeService
{
    //Methods for calculated properties
    decimal CalculateTotalPriceForRecipe(IRecipe recipe);
    decimal CalculatePricePerServing(IRecipe recipe);

    IEnumerable<IMeasurement> GetAllMeasurementsForRecipe(Guid recipeId);
    
    // DTO Mapping
    RecipeDTO MapToDTO(IRecipe recipe);
    IRecipe MapToRecipe(RecipeDTO recipeDTO);
    
    // Additional Methods
    string ConvertInstructionsToJson(IRecipe recipe);
    
    // Repository wrappers
    IEnumerable<RecipeDTO> GetAllRecipes();
    RecipeDTO GetById(Guid id);
    void AddRecipe(IRecipe recipe);
    void UpdateRecipe(IRecipe updatedRecipe);
    void DeleteRecipe(IRecipe recipeToDelete);
    
    IEnumerable<IRecipe> GetByCategory(Categories.RecipeCategory category);
    IEnumerable<IRecipe> GetByIngredient(Guid ingredientId);
    IEnumerable<IRecipe> SortByPrice();
}