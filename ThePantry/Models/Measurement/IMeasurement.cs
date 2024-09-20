namespace ThePantry.Models.Measurement;

public interface IMeasurement
{
    // Properties
    public Guid Id { get; set; } // Primary key for the Measurement
    public Guid RecipeId { get; set; } // Foreign key to the Recipe
    public Guid IngredientId { get; set; } // Foreign key to the Ingredient
    public decimal Amount { get; set; } // Amount of the ingredient used
}
