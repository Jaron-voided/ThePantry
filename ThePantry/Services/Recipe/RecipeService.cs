using System.Text.Json;
using ThePantry.Data.Repositories;
using ThePantry.Models.DTOs;
using ThePantry.Models.Measurement;
using ThePantry.Models.Recipe;
using ThePantry.Services.Measurement;

namespace ThePantry.Services.Recipe;

public class RecipeService(
    IMeasurementRepository measurementRepository,
    IMeasurementService measurementService,
    IRecipeRepository recipeRepository)
    : IRecipeService
{
    public decimal CalculateTotalPriceForRecipe(IRecipe recipe)
    {
        decimal totalPrice = 0;
        IEnumerable<Models.Measurement.Measurement> measurements =
            measurementRepository.GetMeasurementsByRecipe(recipe.Id);
        foreach (var measurement in measurements)
        {
            totalPrice += measurementService.CalculatePrice(measurement);
        }

        return totalPrice;
    }

    public decimal CalculatePricePerServing(IRecipe recipe)
    {
        decimal totalPrice = CalculateTotalPriceForRecipe(recipe);
        return totalPrice / recipe.ServingsPerRecipe;
    }
    
    public IEnumerable<IMeasurement> GetAllMeasurementsForRecipe(Guid recipeId)
    {
        // Delegate this task to the MeasurementRepository
        return measurementRepository.GetMeasurementsByRecipe(recipeId);
    }

    public RecipeDTO MapToDTO(IRecipe recipe)
    {
        return new RecipeDTO()
        {
            Id = recipe.Id,
            Name = recipe.Name,
            RecipeCategory = recipe.RecipeCategory,
            Instructions = recipe.Instructions,
            ServingsPerRecipe = recipe.ServingsPerRecipe
        };
    }

    public IRecipe MapToRecipe(RecipeDTO dto)
    {
        return new Models.Recipe.Recipe()
        {
            Id = dto.Id,
            Name = dto.Name,
            RecipeCategory = dto.RecipeCategory,
            Instructions = dto.Instructions,
            ServingsPerRecipe = dto.ServingsPerRecipe
        };
    }

    public string ConvertInstructionsToJson(IRecipe recipe)
    {
        // Serialize Instructions list to JSON
        return JsonSerializer.Serialize(recipe.Instructions);
    }

    public IEnumerable<RecipeDTO> GetAllRecipes()
    {
        IEnumerable<IRecipe> recipes = recipeRepository.GetAll();
        return recipes.Select(recipe => MapToDTO(recipe));
    }

    public RecipeDTO GetById(Guid id)
    {
        var recipe = recipeRepository.GetById(id);
        return recipe == null ? null : MapToDTO(recipe);
    }

    public void AddRecipe(IRecipe recipe)
    {
        recipeRepository.Add(recipe);
    }

    public void UpdateRecipe(IRecipe updatedRecipe)
    {
        recipeRepository.Update(updatedRecipe);
    }

    public void DeleteRecipe(IRecipe recipeToDelete)
    {
        recipeRepository.Delete(recipeToDelete);
    }
}