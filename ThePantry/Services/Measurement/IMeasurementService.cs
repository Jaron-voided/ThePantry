using ThePantry.Models.DTOs;
using ThePantry.Models.Measurement;

namespace ThePantry.Services.Measurement;

public interface IMeasurementService
{
    // Calculate the price for a specific measurement
    decimal CalculatePrice(IMeasurement measurement);
    
    // DTO mapping
    MeasurementDTO MapToDTO(IMeasurement measurement);
    IMeasurement MapToMeasurement(MeasurementDTO measurement);
}