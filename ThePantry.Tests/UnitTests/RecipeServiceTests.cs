using Moq;
using ThePantry.Services.Recipe;
using ThePantry.Data.Repositories;
using ThePantry.Services.Measurement;
using ThePantry.Tests.TestData;

namespace ThePantry.Tests.UnitTests;

public class RecipeServiceTests
{
    [Fact]
    public void CalculateTotalPriceForRecipe_ShouldReturnCorrectPrice()
    {
        // Arrange
        var recipe = RecipeFactory.CreateAllRecipes().First(); // Get the first recipe (Soup recipe for example)
        
        // Get all ingredients and extract their IDs
        var allIngredients = IngredientFactory.CreateAllIngredients();
        var ingredientIds = allIngredients.Select(i => i.Id).ToList(); // Extract ingredient IDs
        
        // Use a specific method to create measurements for this recipe (passing recipe.Id and ingredientIds)
        var measurements = MeasurementFactory.CreateMeasurementsForSoupRecipe(recipe.Id, ingredientIds);
        
        var ingredientRepository = new Mock<IIngredientRepository>();
        var measurementRepository = new Mock<IMeasurementRepository>();
        var measurementService = new Mock<IMeasurementService>();

        // Set up mocked behavior for measurement repository
        measurementRepository.Setup(mr => mr.GetMeasurementsByRecipe(recipe.Id))
            .Returns(measurements);

        // Set up mocked behavior for ingredient repository
        foreach (var measurement in measurements)
        {
            var ingredient = allIngredients.FirstOrDefault(i => i.Id == measurement.IngredientId);

            ingredientRepository.Setup(ir => ir.GetById(measurement.IngredientId))
                .Returns(ingredient);

            // Assume ingredient.PricePerMeasurement * measurement.Amount is the price
            if (ingredient != null)
                measurementService.Setup(ms => ms.CalculatePrice(measurement))
                    .Returns(ingredient.PricePerMeasurement * measurement.Amount);
        }

        var recipeService = new RecipeService(
            measurementRepository.Object,
            measurementService.Object,
            new Mock<RecipeRepository>().Object);

        // Act
        var totalPrice = recipeService.CalculateTotalPriceForRecipe(recipe);

        // Assert
        decimal expectedTotalPrice = 0;
        foreach (var measurement in measurements)
        {
            var ingredient = allIngredients.FirstOrDefault(i => i.Id == measurement.IngredientId);

            if (ingredient != null) expectedTotalPrice += ingredient.PricePerMeasurement * measurement.Amount;
        }

        Assert.Equal(expectedTotalPrice, totalPrice);
    }

    [Fact]
    public void CalculatePricePerServing_ShouldReturnCorrectPricePerServing()
    {
        // Arrange
        var recipe = RecipeFactory.CreateAllRecipes().First(); // Get the first recipe (Soup recipe)
        
        var allIngredients = IngredientFactory.CreateAllIngredients();
        var ingredientIds = allIngredients.Select(i => i.Id).ToList();
        
        var measurements = MeasurementFactory.CreateMeasurementsForSoupRecipe(recipe.Id, ingredientIds);
        
        var ingredientRepository = new Mock<IIngredientRepository>();
        var measurementRepository = new Mock<IMeasurementRepository>();
        var measurementService = new Mock<IMeasurementService>();

        // Set up mocks for the repositories and services
        measurementRepository.Setup(mr => mr.GetMeasurementsByRecipe(recipe.Id))
            .Returns(measurements);

        foreach (var measurement in measurements)
        {
            var ingredient = allIngredients.FirstOrDefault(i => i.Id == measurement.IngredientId);

            ingredientRepository.Setup(ir => ir.GetById(measurement.IngredientId))
                .Returns(ingredient);

            if (ingredient != null)
                measurementService.Setup(ms => ms.CalculatePrice(measurement))
                    .Returns(ingredient.PricePerMeasurement * measurement.Amount);
        }

        var recipeService = new RecipeService(
            measurementRepository.Object,
            measurementService.Object,
            new Mock<RecipeRepository>().Object);

        // Act
        var pricePerServing = recipeService.CalculatePricePerServing(recipe);

        // Assert
        decimal expectedTotalPrice = 0;
        foreach (var measurement in measurements)
        {
            var ingredient = allIngredients.FirstOrDefault(i => i.Id == measurement.IngredientId);
            if (ingredient != null) expectedTotalPrice += ingredient.PricePerMeasurement * measurement.Amount;
        }

        decimal expectedPricePerServing = expectedTotalPrice / recipe.ServingsPerRecipe;
        Assert.Equal(expectedPricePerServing, pricePerServing);
    }

    [Fact]
    public void ConvertInstructionsToJson_ShouldReturnValidJson()
    {
        // Arrange
        var recipe = RecipeFactory.CreateAllRecipes().First(); // Get the first recipe (Soup recipe)
        var recipeService = new RecipeService(
            new Mock<IMeasurementRepository>().Object,
            new Mock<IMeasurementService>().Object,
            new Mock<RecipeRepository>().Object);

        // Act
        var json = recipeService.ConvertInstructionsToJson(recipe);

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(json));
        Assert.Contains(recipe.Instructions.First(), json);
    }
}
