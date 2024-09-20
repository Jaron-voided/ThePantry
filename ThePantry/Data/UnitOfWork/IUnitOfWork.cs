using ThePantry.Data.Repositories;

namespace ThePantry.Data.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IIngredientRepository Ingredients { get; }
    IRecipeRepository Recipes { get; }
    IMeasurementRepository Measurements { get; }

    void Commit();  // Save changes to the database
    Task CommitAsync();  // Async version for saving changes to the database
}
