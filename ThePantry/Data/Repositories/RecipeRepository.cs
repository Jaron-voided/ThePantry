using Microsoft.Data.SqlClient;
using ThePantry.Models.Extras;
using ThePantry.Models.Ingredient;
using ThePantry.Models.Recipe;

namespace ThePantry.Data.Repositories;

public class RecipeRepository(SqlConnection connection) : IRecipeRepository
{
    private readonly SqlConnection _connection = connection;

    public void Add(IRecipe recipe)
    {
        recipe.Id = Guid.NewGuid();
        // Log details for debugging
        Console.WriteLine($"Adding Recipe: {recipe.Name}");
        Console.WriteLine($"ID: {recipe.Id}");
        Console.WriteLine($"Category: {recipe.RecipeCategory}");
        Console.WriteLine($"Instructions: {string.Join(";", recipe.Instructions)}");
        Console.WriteLine($"Servings Per Recipe: {recipe.ServingsPerRecipe}");

        // SQL command to insert recipe
        const string insertCommandText = @"INSERT INTO Recipe (Id, Name, RecipeCategory, Instructions, ServingsPerRecipe)
                                       VALUES (@id, @name, @recipeCategory, @instructions, @servingsPerRecipe);";

        // Format instructions as a semicolon-separated string
        string formattedInstructions = string.Join(";", recipe.Instructions);

        // Log formatted instructions for debugging
        Console.WriteLine($"Formatted Instructions: {formattedInstructions}");

        // SQL parameters
        SqlParameter[] parameters =
        {
            new SqlParameter("@id", recipe.Id),
            new SqlParameter("@name", recipe.Name),
            new SqlParameter("@recipeCategory", (int)recipe.RecipeCategory),
            new SqlParameter("@instructions", formattedInstructions),
            new SqlParameter("@servingsPerRecipe", recipe.ServingsPerRecipe)
        };

        // Log parameters to ensure they are set correctly
        foreach (var param in parameters)
        {
            Console.WriteLine($"Parameter: {param.ParameterName} = {param.Value}");
        }

        // Execute the insert command
        try
        {
            DB.ExecuteNonQuery(insertCommandText, parameters);
            Console.WriteLine("Recipe added successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while adding recipe: {ex.Message}");
            throw;
        }
    }

    public void Update(IRecipe recipe)
    {
        const string updateCommandText = @"UPDATE Recipe 
                                           SET Name = @name, RecipeCategory = @recipeCategory, Instructions = @instructions, 
                                               ServingsPerRecipe = @servingsPerRecipe
                                           WHERE Id = @id;";

        SqlParameter[] parameters =
        {
            new SqlParameter("@id", recipe.Id),
            new SqlParameter("@name", recipe.Name),
            new SqlParameter("@recipeCategory", (int)recipe.RecipeCategory),
            new SqlParameter("@instructions", string.Join(";", recipe.Instructions)),
            new SqlParameter("@servingsPerRecipe", recipe.ServingsPerRecipe)
        };

        DB.ExecuteNonQuery(updateCommandText, parameters);
    }

    public void Delete(IRecipe recipe)
    {
        const string deleteCommandText = @"DELETE FROM Recipe WHERE Id = @id;";

        SqlParameter[] parameters =
        {
            new SqlParameter("@id", recipe.Id)
        };

        DB.ExecuteNonQuery(deleteCommandText, parameters);
    }

    public IRecipe GetById(Guid id)
    {
        const string selectCommandText = @"SELECT * FROM Recipe WHERE Id = @id;";

        SqlParameter[] parameters =
        {
            new SqlParameter("@id", id)
        };

        using var reader = DB.ExecuteReader(selectCommandText, parameters);
        if (!reader.Read()) return null;

        var recipe = new Recipe
        {
            Id = reader.GetGuid(reader.GetOrdinal("Id")),
            Name = reader.GetString(reader.GetOrdinal("Name")),
            RecipeCategory = (Categories.RecipeCategory)reader.GetInt32(reader.GetOrdinal("RecipeCategory")),
            Instructions = reader.GetString(reader.GetOrdinal("Instructions")).Split(';', StringSplitOptions.RemoveEmptyEntries).ToList(),
            ServingsPerRecipe = reader.GetInt32(reader.GetOrdinal("ServingsPerRecipe"))
        };

        return recipe;
    }

    public IEnumerable<IRecipe> GetAll()
    {
        const string selectCommandText = @"SELECT * FROM Recipe;";

        var recipes = new List<IRecipe>();

        using var reader = DB.ExecuteReader(selectCommandText, Array.Empty<SqlParameter>());
        while (reader.Read())
        {
            var recipe = new Recipe
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                RecipeCategory = (Categories.RecipeCategory)reader.GetInt32(reader.GetOrdinal("RecipeCategory")),
                Instructions = reader.GetString(reader.GetOrdinal("Instructions")).Split(';', StringSplitOptions.RemoveEmptyEntries).ToList(),
                ServingsPerRecipe = reader.GetInt32(reader.GetOrdinal("ServingsPerRecipe"))
            };
            
            recipes.Add(recipe);
        }

        return recipes;
    }
}