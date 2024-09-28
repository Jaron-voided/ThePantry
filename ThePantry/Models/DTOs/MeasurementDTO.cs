namespace ThePantry.Models.DTOs;

public class MeasurementDTO
{
    public Guid Id { get; init; }
    public Guid RecipeId { get; init; }
    public Guid IngredientId { get; init; }
    public decimal Amount { get; init; }
}