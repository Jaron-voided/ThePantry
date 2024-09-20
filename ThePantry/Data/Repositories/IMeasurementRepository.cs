using ThePantry.Models.Measurement;

namespace ThePantry.Data.Repositories;

public interface IMeasurementRepository
{
    void Add(IMeasurement measurement);
    void Update(IMeasurement measurement);
    void Delete(IMeasurement measurement);
    Measurement GetById(Guid id);
    //IEnumerable<Measurement> GetMeasurementsByRecipe(Guid recipeId);
}