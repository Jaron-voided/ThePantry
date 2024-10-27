import { useNavigate } from 'react-router-dom';
import PropTypes from "prop-types";
import AddMeasurementsForm from "./AddMeasurementsForm.jsx";
import {useState} from "react";

const EditMeasurementsForm = ({ recipeId, existingMeasurements, ingredients, handleSubmit }) => {
    const navigate = useNavigate();  // Initialize navigate for routing
    const [formData, setFormData] = useState({
        measurements: existingMeasurements,
        newMeasurement: { ingredientId: '', amount: '' }
    });

    const handleChange = (e, index) => {
        const { name, value } = e.target;
        const updatedMeasurements = [...formData.measurements];
        updatedMeasurements[index] = { ...updatedMeasurements[index], [name]: value };
        setFormData({ ...formData, measurements: updatedMeasurements });
    };

    const handleNewMeasurementChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, newMeasurement: { ...formData.newMeasurement, [name]: value } });
    };

    const handleDelete = (index) => {
        setFormData({
            ...formData,
            measurements: formData.measurements.filter((_, i) => i !== index)
        });
    };

    const addNewMeasurement = () => {
        setFormData({
            ...formData,
            measurements: [...formData.measurements, formData.newMeasurement],
            newMeasurement: { ingredientId: '', amount: '' }
        });
    };

    const onSubmit = (e) => {
        e.preventDefault();

        // Ensure you're replacing old measurements, not just adding new ones.
        handleSubmit(formData.measurements)  // Pass the full set of measurements for submission
            .then(() => {
                // After a successful update, navigate back to the recipe page
                navigate(`/recipes/view/${recipeId}`);  // Adjust path as needed
            })
            .catch((error) => {
                console.error('Failed to update measurements:', error);
            });
    };

    return (
        <form className="edit-measurements-form" onSubmit={onSubmit}>
            {formData.measurements.map((measurement, index) => (
                <div key={index}>
                    <label htmlFor={`ingredientId-${index}`}>Ingredient:</label>
                    <select
                        name="ingredientId"
                        value={measurement.ingredientId}
                        onChange={(e) => handleChange(e, index)}
                        required
                    >
                        <option value="">--Select an Ingredient--</option>
                        {ingredients.map((ingredient) => (
                            <option key={ingredient.id} value={ingredient.id}>
                                {ingredient.name}
                            </option>
                        ))}
                    </select>

                    <label htmlFor={`amount-${index}`}>Amount:</label>
                    <input
                        type="number"
                        name="amount"
                        value={measurement.amount}
                        onChange={(e) => handleChange(e, index)}
                        required
                    />

                    <button type="button" onClick={() => handleDelete(index)}>Delete</button>
                </div>
            ))}

            {/* New measurement form */}
            <div>
                <label htmlFor="newIngredientId">New Ingredient:</label>
                <select
                    name="ingredientId"
                    value={formData.newMeasurement.ingredientId}
                    onChange={handleNewMeasurementChange}
                    required
                >
                    <option value="">--Select an Ingredient--</option>
                    {ingredients.map((ingredient) => (
                        <option key={ingredient.id} value={ingredient.id}>
                            {ingredient.name}
                        </option>
                    ))}
                </select>

                <label htmlFor="newAmount">Amount:</label>
                <input
                    type="number"
                    name="amount"
                    value={formData.newMeasurement.amount}
                    onChange={handleNewMeasurementChange}
                    required
                />

                <button type="button" onClick={addNewMeasurement}>Add Another Measurement</button>
            </div>

            <button type="submit">Finish</button>
        </form>
    );
};

// Adding PropTypes validation for the props
EditMeasurementsForm.propTypes = {
    recipeId: PropTypes.string.isRequired,  // Ensure recipeId is a string and required
    existingMeasurements: PropTypes.arrayOf(PropTypes.shape({
        ingredientId: PropTypes.string.isRequired,
        amount: PropTypes.number.isRequired,
    })).isRequired,  // Array of measurement objects with required ingredientId and amount
    ingredients: PropTypes.arrayOf(PropTypes.shape({
        id: PropTypes.string.isRequired,
        name: PropTypes.string.isRequired
    })).isRequired,  // Array of ingredient objects with required id and name
    handleSubmit: PropTypes.func.isRequired  // Ensure handleSubmit is a function
};


export default EditMeasurementsForm;
