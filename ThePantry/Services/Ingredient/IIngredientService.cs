using ThePantry.Models.DTOs;
using ThePantry.Models.Ingredient;

namespace ThePantry.Services.Ingredient;

public interface IIngredientService
{
    decimal PricePerPortion(IIngredient ingredient, decimal portion);
    
    // DTO Mapping
    IngredientDTO MapToDTO(IIngredient ingredient);
    IIngredient MapToIngredient(IngredientDTO dto);
    IEnumerable<IngredientDTO> GetAllIngredients();
    IngredientDTO GetById(Guid id);
    void AddIngredient(IIngredient ingredient);
    void UpdateIngredient(IIngredient updatedIngredient);
    void DeleteIngredient(IIngredient ingredient);
}