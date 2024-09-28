using Microsoft.Data.SqlClient;
using ThePantry.Models.Extras;
using ThePantry.Models.Ingredient;

namespace ThePantry.Data.Repositories;

public class IngredientRepository(SqlConnection connection) : IIngredientRepository
{
    private readonly SqlConnection _connection = connection;
    public void Add(IIngredient ingredient)
    {
        ingredient.Id = Guid.NewGuid();
        const string insertCommandText = @"INSERT INTO Ingredient (Id, Name, MeasuredIn, IngredientCategory, 
                        PricePerPackage, MeasurementsPerPackage) VALUES (@id, @name, @measuredIn, 
                            @ingredientCategory, @pricePerPackage, @measurementsPerPackage);";
        SqlParameter[] parameters =
        {
            new SqlParameter("@id", ingredient.Id),
            new SqlParameter("@name", ingredient.Name),
            new SqlParameter("@measuredIn", (int)ingredient.MeasuredIn),
            new SqlParameter("@ingredientCategory", (int)ingredient.IngredientCategory),
            new SqlParameter("@pricePerPackage", ingredient.PricePerPackage),
            new SqlParameter("@measurementsPerPackage", ingredient.MeasurementsPerPackage)
        };

        DB.ExecuteNonQuery(insertCommandText, parameters);
    }
    public void Update(IIngredient ingredient)
    {
        const string updateCommandText = @"UPDATE Ingredient 
                                           SET Name = @name, MeasuredIn = @measuredIn, IngredientCategory = @ingredientCategory, 
                                               PricePerPackage = @pricePerPackage, MeasurementsPerPackage = @measurementsPerPackage 
                                           WHERE Id = @id;";

        SqlParameter[] parameters =
        {
            new SqlParameter("@id", ingredient.Id),
            new SqlParameter("@name", ingredient.Name),
            new SqlParameter("@measuredIn", (int)ingredient.MeasuredIn),
            new SqlParameter("@ingredientCategory", (int)ingredient.IngredientCategory),
            new SqlParameter("@pricePerPackage", ingredient.PricePerPackage),
            new SqlParameter("@measurementsPerPackage", ingredient.MeasurementsPerPackage)
        };

        DB.ExecuteNonQuery(updateCommandText, parameters);
    }
    public void Delete(IIngredient ingredient)
    {
        const string deleteCommandText = "DELETE FROM Ingredient WHERE Id = @id;";

        SqlParameter[] parameters =
        {
            new SqlParameter("@id", ingredient.Id)
        };

        DB.ExecuteNonQuery(deleteCommandText, parameters);
    }
    public IIngredient GetById(Guid id)
    {
        const string selectCommandText = "SELECT * FROM Ingredient WHERE Id = @id;";
        
        SqlParameter[] parameters = { new SqlParameter("@id", id) };
        
        using var reader = DB.ExecuteReader(selectCommandText, parameters);

        if (reader.Read())
        {
            var ingredient = new Ingredient
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                MeasuredIn = (Categories.MeasuredIn)reader.GetInt32(reader.GetOrdinal("MeasuredIn")),
                IngredientCategory =
                    (Categories.IngredientCategory)reader.GetInt32(reader.GetOrdinal("IngredientCategory")),
                PricePerPackage = reader.GetDecimal(reader.GetOrdinal("PricePerPackage")),
                MeasurementsPerPackage = reader.GetInt32(reader.GetOrdinal("MeasurementsPerPackage"))
            };
            
            return ingredient;
        }
        
        return null; // Or handle this in another way, e.g., throw an exception
    }

    public IEnumerable<IIngredient> GetAll()
    {
        const string selectCommandText = "SELECT * FROM Ingredient;";

        var ingredients = new List<IIngredient>();

        using var reader = DB.ExecuteReader(selectCommandText, Array.Empty<SqlParameter>());
        while (reader.Read())
        {
            var id = reader.GetGuid(reader.GetOrdinal("Id"));
            var name = reader.GetString(reader.GetOrdinal("Name"));
            var measuredIn = (Categories.MeasuredIn)reader.GetInt32(reader.GetOrdinal("MeasuredIn"));
            var ingredientCategory = (Categories.IngredientCategory)reader.GetInt32(reader.GetOrdinal("IngredientCategory"));
            var pricePerPackage = reader.GetDecimal(reader.GetOrdinal("PricePerPackage"));
            var measurementsPerPackage = reader.GetInt32(reader.GetOrdinal("MeasurementsPerPackage"));

            var ingredient = new Ingredient
            {
                Id = id,
                Name = name,
                MeasuredIn = measuredIn,
                IngredientCategory = ingredientCategory,
                PricePerPackage = pricePerPackage,
                MeasurementsPerPackage = measurementsPerPackage
            };

            ingredients.Add(ingredient);
        }

        return ingredients;
    }
}