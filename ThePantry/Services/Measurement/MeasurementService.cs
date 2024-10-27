using ThePantry.Data.Repositories;
using ThePantry.Models.DTOs;
using ThePantry.Models.Measurement;

namespace ThePantry.Services.Measurement;

public class MeasurementService(
    IIngredientRepository ingredientRepository,
    IMeasurementRepository measurementRepository)
    : IMeasurementService
{

    // Calculate the price of a measurement based on the ingredient's price
    public decimal CalculatePrice(IMeasurement measurement)
    {
        var ingredient = ingredientRepository.GetById(measurement.IngredientId);

        if (ingredient == null)
        {
            throw new Exception($"Ingredient with id {measurement.IngredientId} not found");
        }
        
        return ingredient.PricePerMeasurement * measurement.Amount;
    }

    public MeasurementDTO MapToDTO(IMeasurement measurement)
    {
        return new MeasurementDTO()
        {
            Id = measurement.Id,
            RecipeId = measurement.RecipeId,
            IngredientId = measurement.IngredientId,
            Amount = measurement.Amount
        };
    }

    public IMeasurement MapToMeasurement(MeasurementDTO dto)
    {
        return new Models.Measurement.Measurement
        {
            Id = dto.Id,
            RecipeId = dto.RecipeId,
            IngredientId = dto.IngredientId,
            Amount = dto.Amount
        };
    }

    public IEnumerable<MeasurementDTO> GetMeasurementsByRecipe(Guid recipeId)
    {
        IEnumerable<Models.Measurement.Measurement> measurements =
            measurementRepository.GetMeasurementsByRecipe(recipeId);
        return measurements.Select(measurement => MapToDTO(measurement));
    }

    public MeasurementDTO GetById(Guid id)
    {
        var measurement = measurementRepository.GetById(id);
        return measurement == null ? null : MapToDTO(measurement);
    }

    public void AddMeasurement(IMeasurement measurement)
    {
        measurementRepository.Add(measurement);
    }

    public void UpdateMeasurement(IMeasurement updatedMeasurement)
    {
        measurementRepository.Update(updatedMeasurement);
    }

    public void DeleteMeasurement(IMeasurement measurementToDelete)
    {
        measurementRepository.Delete(measurementToDelete);
    }


}