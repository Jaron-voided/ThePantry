using Microsoft.AspNetCore.Mvc;
using ThePantry.Models.DTOs;
using ThePantry.Services.Measurement;
using ThePantry.Models.Measurement;
using ThePantry.Services.Measurement;

namespace ThePantry.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeasurementsController(IMeasurementService measurementService) : ControllerBase
{
    // Get: api/Measurements
    [HttpGet]
    public IActionResult GetMeasurementsByRecipe([FromQuery] Guid recipeId)
    {
        var measurements = measurementService.GetMeasurementsByRecipe(recipeId);
        return Ok(measurements);
    }
    
    // Get api/Measurements/{id}
    [HttpGet("{id}")]
    public IActionResult GetMeasurementById(Guid id)
    {
        var measurement = measurementService.GetById(id);
        if (measurement == null)
        {
            return NotFound();
        }

        return Ok(measurement);
    }

    // POST: api/Measurements
    [HttpPost]
    public IActionResult CreateMeasurement([FromBody] MeasurementDTO measurementDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // 400 if model validation fails
        }

        var measurement = measurementService.MapToMeasurement(measurementDto);
        measurementService.AddMeasurement(measurement);
        
        return CreatedAtAction(nameof(GetMeasurementById), new { id = measurement.Id }, measurement);
    }
    
    // PUT: api/Measurements/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateMeasurement(Guid id, [FromBody] MeasurementDTO measurementDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingMeasurement = measurementService.GetById(id);
        if (existingMeasurement == null)
        {
            return NotFound();
        }
        
        var updatedMeasurement = measurementService.MapToMeasurement(measurementDto);
        updatedMeasurement.Id = id; // Ensure the ID remains the same
        measurementService.UpdateMeasurement(updatedMeasurement);

        return NoContent(); // 204 No Content after successful update
    }
    
    // DELETE: api/Measurements/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteMeasurement(Guid id)
    {
        MeasurementDTO measurement = measurementService.GetById(id);
        if (measurement == null)
        {
            return NotFound();
        }

        var measurementToDelete = measurementService.MapToMeasurement(measurement);
        measurementService.DeleteMeasurement(measurementToDelete);
        return NoContent(); // 204 No Content after successful deletion
    }
}