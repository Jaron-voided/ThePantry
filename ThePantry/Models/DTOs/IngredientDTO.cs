using ThePantry.Models.Extras;

namespace ThePantry.Models.DTOs;

public class IngredientDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Categories.MeasuredIn MeasuredIn { get; set; }
    public Categories.IngredientCategory IngredientCategory { get; set; }
    public decimal PricePerPackage { get; set; }
    public int MeasurementsPerPackage { get; set; }
    
    // Calculated Property
    public decimal PricePerMeasurement { get; set; }
}