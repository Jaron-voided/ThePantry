namespace ThePantry.Models.Measurement;

public class Measurement : IMeasurement
{
    public Guid Id { get; set; }
    public Guid RecipeId { get; set; }
    public Guid IngredientId { get; set; }
    public decimal Amount { get; set; }
    
    // Constructors
    public Measurement(Guid recipeId, Guid ingredientId, decimal amount)
    {
        Id = Guid.NewGuid();
        RecipeId = recipeId;
        IngredientId = ingredientId;
        Amount = amount;
    }
    
    // Parameterless
    public Measurement()
    {
        Id = Guid.NewGuid();
    }
}