using Moq;
using ThePantry.Models.DTOs;
using ThePantry.Services.Measurement;
using ThePantry.Data.Repositories;
using ThePantry.Models.Ingredient;
using ThePantry.Tests.TestData;

namespace ThePantry.Tests.UnitTests;

public class MeasurementServiceTests
{
    private readonly Mock<IIngredientRepository> _ingredientRepositoryMock;
    private readonly Mock<IMeasurementRepository> _measurementRepositoryMock;
    private readonly MeasurementService _measurementService;

    public MeasurementServiceTests()
    {
        // Initialize the mocks
        _ingredientRepositoryMock = new Mock<IIngredientRepository>();
        _measurementRepositoryMock = new Mock<IMeasurementRepository>();

        // Pass the mocks to the service
        _measurementService = new MeasurementService(_ingredientRepositoryMock.Object, _measurementRepositoryMock.Object);
    }

    [Fact]
    public void CalculatePrice_ShouldReturnCorrectPrice_WhenIngredientIsFound()
    {
        // Arrange
        var ingredient = IngredientFactory.CreateAllIngredients().First();
        var recipe = RecipeFactory.CreateAllRecipes().First();

        // Now create a measurement with the IngredientId and RecipeId
        var measurement = MeasurementFactory.CreateMeasurement(recipe.Id, ingredient.Id, 2m);  // 2m as example amount

        // Mock the GetById method to return the ingredient
        _ingredientRepositoryMock.Setup(repo => repo.GetById(measurement.IngredientId)).Returns(ingredient);

        // Act
        var result = _measurementService.CalculatePrice(measurement);

        // Assert
        var expectedPrice = ingredient.PricePerMeasurement * measurement.Amount;
        Assert.Equal(expectedPrice, result);
    }

    [Fact]
    public void CalculatePrice_ShouldThrowException_WhenIngredientIsNotFound()
    {
        // Arrange
        var ingredient = IngredientFactory.CreateAllIngredients().First();
        var recipe = RecipeFactory.CreateAllRecipes().First();
        var measurement = MeasurementFactory.CreateMeasurement(recipe.Id, ingredient.Id, 1m);  // 1m as example amount

        // Mock the GetById method to return null, simulating an ingredient not found
        _ingredientRepositoryMock.Setup(repo => repo.GetById(measurement.IngredientId)).Returns((Ingredient)null);

        // Act & Assert
        Assert.Throws<Exception>(() => _measurementService.CalculatePrice(measurement));
    }

    [Fact]
    public void MapToDTO_ShouldReturnCorrectDTO()
    {
        // Arrange
        var ingredient = IngredientFactory.CreateAllIngredients().First();
        var recipe = RecipeFactory.CreateAllRecipes().First();
        var measurement = MeasurementFactory.CreateMeasurement(recipe.Id, ingredient.Id, 2m);

        // Act
        var dto = _measurementService.MapToDTO(measurement);

        // Assert
        Assert.Equal(measurement.Id, dto.Id);
        Assert.Equal(measurement.RecipeId, dto.RecipeId);
        Assert.Equal(measurement.IngredientId, dto.IngredientId);
        Assert.Equal(measurement.Amount, dto.Amount);
    }

    [Fact]
    public void MapToMeasurement_ShouldReturnCorrectMeasurement()
    {
        // Arrange
        var dto = new MeasurementDTO
        {
            Id = Guid.NewGuid(),
            RecipeId = Guid.NewGuid(),
            IngredientId = Guid.NewGuid(),
            Amount = 2m
        };

        // Act
        var measurement = _measurementService.MapToMeasurement(dto);

        // Assert
        Assert.Equal(dto.Id, measurement.Id);
        Assert.Equal(dto.RecipeId, measurement.RecipeId);
        Assert.Equal(dto.IngredientId, measurement.IngredientId);
        Assert.Equal(dto.Amount, measurement.Amount);
    }
}

