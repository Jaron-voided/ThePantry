using ThePantry.Models.Ingredient;

namespace ThePantry.Data.Repositories;

public interface IIngredientRepository
{
    void Add(IIngredient ingredient);
    void Update(IIngredient ingredient);
    void Delete(IIngredient ingredient);
    IIngredient GetById(Guid id);
    IEnumerable<IIngredient> GetAll();
}