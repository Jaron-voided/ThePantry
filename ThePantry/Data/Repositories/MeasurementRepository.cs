using Microsoft.Data.SqlClient;
using ThePantry.Models.Measurement;

namespace ThePantry.Data.Repositories;

public class MeasurementRepository : IMeasurementRepository
{
    //private readonly SqlConnection _connection = connection;

    private readonly DB _db;

    public MeasurementRepository(DB db)
    {
        _db = db;
    }
    public void Add(IMeasurement measurement)
        {
            measurement.Id = Guid.NewGuid(); // Generate new GUID for the measurement

            const string insertCommandText = @"
                INSERT INTO Measurement (Id, RecipeId, IngredientId, Amount) 
                VALUES (@id, @recipeId, @ingredientId, @amount);";

            SqlParameter[] parameters =
            {
                new SqlParameter("@id", measurement.Id),
                new SqlParameter("@recipeId", measurement.RecipeId),
                new SqlParameter("@ingredientId", measurement.IngredientId),
                new SqlParameter("@amount", measurement.Amount)
            };

            _db.ExecuteNonQuery(insertCommandText, parameters);
        }
    public void Update(IMeasurement measurement)
    {
        const string updateCommandText = @"
            UPDATE Measurement 
            SET RecipeId = @recipeId, IngredientId = @ingredientId, Amount = @amount 
            WHERE Id = @id;";

        SqlParameter[] parameters =
        {
            new SqlParameter("@id", measurement.Id),
            new SqlParameter("@recipeId", measurement.RecipeId),
            new SqlParameter("@ingredientId", measurement.IngredientId),
            new SqlParameter("@amount", measurement.Amount)
        };

        _db.ExecuteNonQuery(updateCommandText, parameters);
    }
    public void Delete(IMeasurement measurement)
    {
        const string deleteCommandText = "DELETE FROM Measurement WHERE Id = @id;";

        SqlParameter[] parameters = { new SqlParameter("@id", measurement.Id) };

        _db.ExecuteNonQuery(deleteCommandText, parameters);
    }
    public Measurement GetById(Guid id)
    {
        const string selectCommandText = "SELECT * FROM Measurement WHERE Id = @id;";

        SqlParameter[] parameters = { new SqlParameter("@id", id) };

        using var reader = _db.ExecuteReader(selectCommandText, parameters);
        if (reader.Read())
        {
            return new Measurement
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                RecipeId = reader.GetGuid(reader.GetOrdinal("RecipeId")),
                IngredientId = reader.GetGuid(reader.GetOrdinal("IngredientId")),
                Amount = reader.GetDecimal(reader.GetOrdinal("Amount"))
            };
        }

        return null; // Return null if no measurement is found
    }
    public IEnumerable<Measurement> GetMeasurementsByRecipe(Guid recipeId)
    {
        const string selectCommandText = @"SELECT * FROM Measurement WHERE RecipeId = @recipeId;";

        SqlParameter[] parameters =
        {
            new SqlParameter("@recipeId", recipeId)
        };

        var measurements = new List<Measurement>();

        using var reader = _db.ExecuteReader(selectCommandText, parameters);
        while (reader.Read())
        {
            var measurement = new Measurement
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                RecipeId = reader.GetGuid(reader.GetOrdinal("RecipeId")),
                IngredientId = reader.GetGuid(reader.GetOrdinal("IngredientId")),
                Amount = reader.GetDecimal(reader.GetOrdinal("Amount"))
            };

            measurements.Add(measurement);
        }
        return measurements;
    }

    public IEnumerable<Measurement> GetMeasurementsWithIngredient(Guid ingredientId)
    {
        const string selectCommandText = @"SELECT * FROM Measurement WHERE IngredientId = @ingredientId;";

        SqlParameter[] parameters =
        {
            new SqlParameter("@ingredientId", ingredientId)
        };

        var measurements = new List<Measurement>();

        using var reader = _db.ExecuteReader(selectCommandText, parameters);
        while (reader.Read())
        {
            var measurement = new Measurement
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                RecipeId = reader.GetGuid(reader.GetOrdinal("RecipeId")),
                IngredientId = reader.GetGuid(reader.GetOrdinal("IngredientId")),
                Amount = reader.GetDecimal(reader.GetOrdinal("Amount"))
            };

            measurements.Add(measurement);
        }
        return measurements;
    }
}