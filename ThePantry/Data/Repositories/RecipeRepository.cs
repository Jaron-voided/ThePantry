using Microsoft.Data.SqlClient;
using ThePantry.Models.Extras;
using ThePantry.Models.Recipe;

namespace ThePantry.Data.Repositories;

public class RecipeRepository(SqlConnection connection) : IRecipeRepository
{
    private readonly SqlConnection _connection = connection;

    public void Add(IRecipe recipe)
    {
        const string insertCommandText = @"INSERT INTO Recipe (Id, Name, RecipeCategory, Instructions, ServingsPerRecipe)
                                           VALUES (@id, @name, @recipeCategory, @instructions, @servingsPerRecipe);";

        SqlParameter[] parameters =
        {
            new SqlParameter("@id", recipe.Id),
            new SqlParameter("@name", recipe.Name),
            new SqlParameter("@recipeCategory", (int)recipe.RecipeCategory),
            new SqlParameter("@instructions", string.Join(";", recipe.Instructions)), // Assuming instructions are stored as a semicolon-separated string
            new SqlParameter("@servingsPerRecipe", recipe.ServingsPerRecipe)
        };

        DB.ExecuteNonQuery(insertCommandText, parameters);
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