/*
using Microsoft.Data.SqlClient;
using ThePantry.Data.Repositories;

namespace ThePantry.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly SqlConnection _connection;
    private SqlTransaction _transaction;

    // Repositories
    public IIngredientRepository Ingredients { get; }
    public IRecipeRepository Recipes { get; }
    public IMeasurementRepository Measurements { get; }

    // Constructor
    public UnitOfWork(string connectionString)
    {
        _connection = new SqlConnection(connectionString);
        _connection.Open();
        _transaction = _connection.BeginTransaction();

        // Initialize repositories and pass the same transaction
        Ingredients = new IngredientRepository(_connection, _transaction);
        Recipes = new RecipeRepository(_connection, _transaction);
        Measurements = new MeasurementRepository(_connection, _transaction);
    }

    // Commit transaction and save changes
    public void Commit()
    {
        try
        {
            _transaction.Commit();
        }
        catch
        {
            _transaction.Rollback();
            throw;
        }
        finally
        {
            _transaction.Dispose();
            _connection.Close();
        }
    }

    // Async Commit
    public async Task CommitAsync()
    {
        try
        {
            await _transaction.CommitAsync();
        }
        catch
        {
            await _transaction.RollbackAsync();
            throw;
        }
        finally
        {
            await _transaction.DisposeAsync();
            await _connection.CloseAsync();
        }
    }

    // Dispose method to clean up
    public void Dispose()
    {
        _transaction?.Dispose();
        _connection?.Dispose();
    }
}
*/

