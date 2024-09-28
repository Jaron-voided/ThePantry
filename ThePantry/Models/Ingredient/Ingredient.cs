using ThePantry.Models.Extras;

namespace ThePantry.Models.Ingredient;

public class Ingredient : IIngredient
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Categories.MeasuredIn MeasuredIn { get; set; }
    public Categories.IngredientCategory IngredientCategory { get; set; }
    public decimal PricePerPackage { get; set; }
    public int MeasurementsPerPackage { get; set; }
    
    // Calculated Properties
    public decimal PricePerMeasurement => PricePerPackage / MeasurementsPerPackage;
    
    //Constructors
    
    public Ingredient(string name, Categories.MeasuredIn measuredIn, Categories.IngredientCategory ingredientCategory,
                decimal pricePerPackage, int measurementsPerPackage)
    {
        Id = Guid.NewGuid();
        Name = name;
        MeasuredIn = measuredIn;
        IngredientCategory = ingredientCategory;
        PricePerPackage = pricePerPackage;
        MeasurementsPerPackage = measurementsPerPackage;
    }
    
    //Parameterless
    public Ingredient()
    {
        Id = Guid.NewGuid();
    }
    
}