using ThePantry.Models.Extras;
using ThePantry.Models.Ingredient;

namespace ThePantry.Tests.TestData;
public static class IngredientFactory
{
    public static Ingredient CreateIngredient1()
    {
        return new Ingredient
        {
            Id = Guid.NewGuid(),
            Name = "Sugar",
            MeasuredIn = Categories.MeasuredIn.Weight,
            IngredientCategory = Categories.IngredientCategory.Baking,
            PricePerPackage = 2.99m,
            MeasurementsPerPackage = 1000 // grams
        };
    }

    public static Ingredient CreateIngredient2()
    {
        return new Ingredient
        {
            Id = Guid.NewGuid(),
            Name = "Salt",
            MeasuredIn = Categories.MeasuredIn.Weight,
            IngredientCategory = Categories.IngredientCategory.Baking,
            PricePerPackage = 0.99m,
            MeasurementsPerPackage = 500 // grams
        };
    }

    public static Ingredient CreateIngredient3()
    {
        return new Ingredient
        {
            Id = Guid.NewGuid(),
            Name = "Butter",
            MeasuredIn = Categories.MeasuredIn.Weight,
            IngredientCategory = Categories.IngredientCategory.Dairy,
            PricePerPackage = 3.50m,
            MeasurementsPerPackage = 200 // grams
        };
    }

    public static Ingredient CreateIngredient4()
    {
        return new Ingredient
        {
            Id = Guid.NewGuid(),
            Name = "Olive Oil",
            MeasuredIn = Categories.MeasuredIn.Volume,
            IngredientCategory = Categories.IngredientCategory.Liquid,
            PricePerPackage = 6.50m,
            MeasurementsPerPackage = 750 // ml
        };
    }

    public static Ingredient CreateIngredient5()
    {
        return new Ingredient
        {
            Id = Guid.NewGuid(),
            Name = "Flour",
            MeasuredIn = Categories.MeasuredIn.Weight,
            IngredientCategory = Categories.IngredientCategory.Grain,
            PricePerPackage = 1.50m,
            MeasurementsPerPackage = 1000 // grams
        };
    }

    public static Ingredient CreateIngredient6()
    {
        return new Ingredient
        {
            Id = Guid.NewGuid(),
            Name = "Tomato",
            MeasuredIn = Categories.MeasuredIn.Each,
            IngredientCategory = Categories.IngredientCategory.Vegetable,
            PricePerPackage = 2.00m,
            MeasurementsPerPackage = 10 // individual tomatoes
        };
    }

    public static Ingredient CreateIngredient7()
    {
        return new Ingredient
        {
            Id = Guid.NewGuid(),
            Name = "Chicken Breast",
            MeasuredIn = Categories.MeasuredIn.Weight,
            IngredientCategory = Categories.IngredientCategory.Meat,
            PricePerPackage = 5.99m,
            MeasurementsPerPackage = 500 // grams
        };
    }

    public static Ingredient CreateIngredient8()
    {
        return new Ingredient
        {
            Id = Guid.NewGuid(),
            Name = "Milk",
            MeasuredIn = Categories.MeasuredIn.Volume,
            IngredientCategory = Categories.IngredientCategory.Dairy,
            PricePerPackage = 1.25m,
            MeasurementsPerPackage = 1000 // ml
        };
    }

    public static Ingredient CreateIngredient9()
    {
        return new Ingredient
        {
            Id = Guid.NewGuid(),
            Name = "Cinnamon",
            MeasuredIn = Categories.MeasuredIn.Weight,
            IngredientCategory = Categories.IngredientCategory.Spice,
            PricePerPackage = 4.00m,
            MeasurementsPerPackage = 50 // grams
        };
    }

    public static Ingredient CreateIngredient10()
    {
        return new Ingredient
        {
            Id = Guid.NewGuid(),
            Name = "Eggs",
            MeasuredIn = Categories.MeasuredIn.Each,
            IngredientCategory = Categories.IngredientCategory.Dairy,
            PricePerPackage = 2.50m,
            MeasurementsPerPackage = 12 // eggs
        };
    }

    // Function to create all ingredients and return as a list
    public static List<Ingredient> CreateAllIngredients()
    {
        return new List<Ingredient>
        {
            CreateIngredient1(),
            CreateIngredient2(),
            CreateIngredient3(),
            CreateIngredient4(),
            CreateIngredient5(),
            CreateIngredient6(),
            CreateIngredient7(),
            CreateIngredient8(),
            CreateIngredient9(),
            CreateIngredient10()
        };
    }
}

