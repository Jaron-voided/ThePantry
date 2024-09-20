namespace ThePantry.Data;

public interface IPantryDatabase
{
    void CreateIngredientTable();
    void CreateMeasurementTable();
    void CreateRecipeTable();
    void CreateAllTables();
}