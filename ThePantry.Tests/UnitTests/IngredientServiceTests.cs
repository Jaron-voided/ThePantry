using Moq;
using ThePantry.Data.Repositories;
using ThePantry.Services.Ingredient;
using ThePantry.Models.DTOs;
using ThePantry.Models.Extras;
using ThePantry.Tests.TestData;

namespace ThePantry.Tests.UnitTests;

public class IngredientServiceTests
{
    private readonly IIngredientService _ingredientService;
    
    public IngredientServiceTests()
    {
        // Set up repository or use mock, if applicable
        var ingredientRepository = new Mock<IIngredientRepository>();
        
        // Optionally set up the repository behavior
        // ingredientRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(...);
        
        // Instantiate IngredientService with the repository
        _ingredientService = new IngredientService(ingredientRepository.Object);
    }

    [Fact]
    public void PricePerPortion_CalculatesCorrectly()
    {
        // Arrange
        var ingredient = IngredientFactory.CreateAllIngredients().First();
        var portion = 100; // Example portion size

        // Act
        var result = _ingredientService.PricePerPortion(ingredient, portion);
        var expectedPrice = ingredient.PricePerMeasurement * portion;
        // Assert
        Assert.Equal(expectedPrice, result);
    }

    [Fact]
    public void MapToDTO_MapsCorrectly()
    {
        // Arrange
        var ingredient = IngredientFactory.CreateAllIngredients().First();

        // Act
        IngredientDTO ingredientDto = _ingredientService.MapToDTO(ingredient);

        // Assert
        Assert.Equal(ingredient.Name, ingredientDto.Name);
        Assert.Equal(ingredient.MeasuredIn, ingredientDto.MeasuredIn);
        // Other assertions as needed
    }

    [Fact]
    public void MapToIngredient_MapsCorrectly()
    {
        // Arrange
        var ingredientDto = new IngredientDTO 
        {
            Name = "Salt",
            MeasuredIn = Categories.MeasuredIn.Weight,
            IngredientCategory = Categories.IngredientCategory.Spice,
            // Set other properties...
        };

        // Act
        var ingredient = _ingredientService.MapToIngredient(ingredientDto);

        // Assert
        Assert.Equal(ingredientDto.Name, ingredient.Name);
        Assert.Equal(ingredientDto.MeasuredIn, ingredient.MeasuredIn);
        // Other assertions as needed
    }
}
