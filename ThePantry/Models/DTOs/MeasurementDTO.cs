namespace ThePantry.Models.DTOs;

public class MeasurementDTO
{
    public Guid Id { get; set; }
    public Guid RecipeId { get; set; }
    public Guid IngredientId { get; set; }
    public decimal Amount { get; set; }
}