using System.Text.Json.Serialization;
using ThePantry.Models.Extras;

namespace ThePantry.Models.DTOs;

public class IngredientDTO
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public Categories.MeasuredIn MeasuredIn { get; init; }
    public Categories.IngredientCategory IngredientCategory { get; init; }
    public decimal PricePerPackage { get; init; }
    public int MeasurementsPerPackage { get; init; }
    
    // Calculated Property
    // Exclude from serialization
    //[JsonIgnore]
    //public decimal PricePerMeasurement PricePerMeasurement => PricePerPackage / MeasurementsPerPackage;
    public decimal PricePerMeasurement
    {
        get
        {
            return PricePerPackage / MeasurementsPerPackage;
        }
    }
}