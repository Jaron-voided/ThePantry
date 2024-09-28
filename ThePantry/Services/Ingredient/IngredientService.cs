using ThePantry.Data.Repositories;
using ThePantry.Models.DTOs;
using ThePantry.Models.Ingredient;

namespace ThePantry.Services.Ingredient;

public class IngredientService(IIngredientRepository ingredientRepository) : IIngredientService
{
    public decimal PricePerPortion(IIngredient ingredient, decimal portion)
        => ingredient.PricePerMeasurement * portion;
    

    // Mapping
    public IngredientDTO MapToDTO(IIngredient ingredient)
    {
        return new IngredientDTO()
        {
            Id = ingredient.Id,
            Name = ingredient.Name,
            MeasuredIn = ingredient.MeasuredIn,
            IngredientCategory = ingredient.IngredientCategory,
            PricePerPackage = ingredient.PricePerPackage,
            MeasurementsPerPackage = ingredient.MeasurementsPerPackage
        };
    }

    public IIngredient MapToIngredient(IngredientDTO dto)
    {
        return new Models.Ingredient.Ingredient()
        {
            Id = dto.Id,
            Name = dto.Name,
            MeasuredIn = dto.MeasuredIn,
            IngredientCategory = dto.IngredientCategory,
            PricePerPackage = dto.PricePerPackage,
            MeasurementsPerPackage = dto.MeasurementsPerPackage
        };
    }

    public IEnumerable<IngredientDTO> GetAllIngredients()
    {
        return ingredientRepository.GetAll().Select(MapToDTO);
    }

    public IngredientDTO GetById(Guid id)
    {
        var ingredient = ingredientRepository.GetById(id);
        return ingredient == null ? null : MapToDTO(ingredient);
    }

    // Wrappers for repository methods, if additional validation or logic needs added, add here
    // Wrap these in try catch blocks
    public void AddIngredient(IIngredient ingredient)
    {
        ingredientRepository.Add(ingredient);
    }

    public void UpdateIngredient(IIngredient updatedIngredient)
    {
        ingredientRepository.Update(updatedIngredient);
    }
    
    public void DeleteIngredient(IIngredient ingredient)
    {
        ingredientRepository.Delete(ingredient);
    }
}