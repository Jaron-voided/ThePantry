using ThePantry.Models.Measurement;

namespace ThePantry.Tests.TestData;

public static class MeasurementFactory
{
    public static Measurement CreateMeasurement(Guid recipeId, Guid ingredientId, decimal amount)
    {
        return new Measurement
        {
            Id = Guid.NewGuid(),
            RecipeId = recipeId,
            IngredientId = ingredientId,
            Amount = amount
        };
    }

    public static List<Measurement> CreateMeasurementsForSoupRecipe(Guid recipeId, List<Guid> ingredientIds)
    {
        return new List<Measurement>
        {
            CreateMeasurement(recipeId, ingredientIds[0], 500m), // 500g tomatoes
            CreateMeasurement(recipeId, ingredientIds[1], 200m), // 200g onions
            CreateMeasurement(recipeId, ingredientIds[2], 50m)  // 50g oil
        };
    }

    public static List<Measurement> CreateMeasurementsForAppetizerRecipe(Guid recipeId, List<Guid> ingredientIds)
    {
        return new List<Measurement>
        {
            CreateMeasurement(recipeId, ingredientIds[3], 100m), // 100g bread
            CreateMeasurement(recipeId, ingredientIds[4], 50m),  // 50g tomatoes
            CreateMeasurement(recipeId, ingredientIds[5], 30m)   // 30g basil
        };
    }

    public static List<Measurement> CreateMeasurementsForDessertRecipe(Guid recipeId, List<Guid> ingredientIds)
    {
        return new List<Measurement>
        {
            CreateMeasurement(recipeId, ingredientIds[6], 200m), // 200g flour
            CreateMeasurement(recipeId, ingredientIds[7], 100m), // 100g sugar
            CreateMeasurement(recipeId, ingredientIds[8], 50m)   // 50g cocoa powder
        };
    }

    public static List<Measurement> CreateMeasurementsForLunchRecipe(Guid recipeId, List<Guid> ingredientIds)
    {
        return new List<Measurement>
        {
            CreateMeasurement(recipeId, ingredientIds[9], 200m), // 200g bread
            CreateMeasurement(recipeId, ingredientIds[6], 100m)  // 100g cheese
        };
    }

    public static List<Measurement> CreateAllMeasurements(List<Guid> recipeIds, List<Guid> ingredientIds)
    {
        var measurements = new List<Measurement>();

        // Assume recipeIds[0] is for Tomato Soup, recipeIds[1] is for Bruschetta, etc.
        measurements.AddRange(CreateMeasurementsForSoupRecipe(recipeIds[0], ingredientIds));
        measurements.AddRange(CreateMeasurementsForAppetizerRecipe(recipeIds[1], ingredientIds));
        measurements.AddRange(CreateMeasurementsForDessertRecipe(recipeIds[2], ingredientIds));
        measurements.AddRange(CreateMeasurementsForLunchRecipe(recipeIds[3], ingredientIds));

        return measurements;
    }
}

