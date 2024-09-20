using ThePantry.Models.DTOs;
using ThePantry.Models.Ingredient;

namespace ThePantry.Services.Ingredient;

public interface IIngredientService
{
    decimal PricePerPortion(decimal portion);
    
    // DTO Mapping
    IngredientDTO MapToDTO(Models.Ingredient.Ingredient ingredient);
    IIngredient MapToIngredient(IngredientDTO dto);
    
    // Overrides
    string IngredientToString(IIngredient ingredient);
}