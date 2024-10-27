using ThePantry.Models.Extras;
using ThePantry.Models.Recipe;

namespace ThePantry.Data.Repositories;

public interface IRecipeRepository
{
    void Add(IRecipe recipe);
    void Update(IRecipe recipe);
    void Delete(IRecipe recipe);
    IRecipe GetById(Guid id);
    IEnumerable<IRecipe> GetAll();
    IEnumerable<IRecipe> GetByCategory(Categories.RecipeCategory category);
    /*IEnumerable<IRecipe> GetByIngredient(Guid ingredientId);
    IEnumerable<IRecipe> SortByPrice();*/
}