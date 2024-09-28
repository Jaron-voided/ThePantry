using ThePantry.Models.Recipe;

namespace ThePantry.Data.Repositories;

public interface IRecipeRepository
{
    void Add(IRecipe recipe);
    void Update(IRecipe recipe);
    void Delete(IRecipe recipe);
    IRecipe GetById(Guid id);
    IEnumerable<IRecipe> GetAll();
    
}