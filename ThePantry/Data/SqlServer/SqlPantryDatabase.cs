namespace ThePantry.Data.SqlServer;

public class SqlPantryDatabase : IPantryDatabase
{
    public void CreateIngredientTable()
    {
        const string createTableQuery = @"
            IF OBJECT_ID('Ingredient', 'U') IS NULL
            CREATE TABLE Ingredient (
                Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
                Name NVARCHAR(100) NOT NULL,
                MeasuredIn INT NOT NULL,
                IngredientCategory INT NOT NULL,
                PricePerPackage DECIMAL(10, 2) NOT NULL,
                MeasurementsPerPackage INT NOT NULL
            );";

        DB.CreateTable(createTableQuery);
    }

    public void CreateMeasurementTable()
    {
        const string createTableQuery = @"
            IF OBJECT_ID('Measurement', 'U') IS NULL
            CREATE TABLE Measurement (
                Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
                RecipeId UNIQUEIDENTIFIER NOT NULL,
                IngredientId UNIQUEIDENTIFIER NOT NULL,
                Amount DECIMAL(18, 2) NOT NULL,
                FOREIGN KEY (RecipeId) REFERENCES Recipe(Id) ON DELETE CASCADE,
                FOREIGN KEY (IngredientId) REFERENCES Ingredient(Id) ON DELETE CASCADE
            );";

        DB.CreateTable(createTableQuery);
    }

    public void CreateRecipeTable()
    {
        const string createTableQuery = @"
            IF OBJECT_ID('Recipe', 'U') IS NULL
            CREATE TABLE Recipe (
                Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
                Name NVARCHAR(100) NOT NULL,
                RecipeCategory INT NOT NULL,
                Instructions NVARCHAR(MAX),
                ServingsPerRecipe INT NOT NULL,
                TotalPriceForRecipe DECIMAL(18, 2),
                PricePerServing DECIMAL(18, 2)
            );";

        DB.CreateTable(createTableQuery);
    }

    public void CreateAllTables()
    {
        CreateIngredientTable();
        CreateRecipeTable();
        CreateMeasurementTable();
    }
}
