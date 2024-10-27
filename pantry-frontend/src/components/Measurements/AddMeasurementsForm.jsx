import React, {useEffect, useState} from 'react';
import PropTypes from 'prop-types';
import '../../app/layout/styles/measurements/measurement.css';



const AddMeasurementsForm = ({ onSubmit, recipeId, finish }) => {
    const [error, setError] = useState('');

    // Form state
    const [formData, setFormData] = useState({
        recipeId: recipeId,
        ingredientId: '',
        amount: 0
    });

    const [ingredients, setIngredients] = useState([]);

    // Fetch the ingredients when the component loads
    useEffect(() => {
        fetch('https://localhost:5001/api/ingredients')
            .then(response => response.json())
            .then(data => setIngredients(data))
            .catch((error) => {
                console.error('Error adding ingredient:', error);
                setError('Failed to load ingredients');
            });
    }, []);

// Handle form change and convert value to integer
    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({
            ...formData,
            [name]: value // Update the form data based on input name
        });
    };

    // Handle form submission
    const handleSubmit = (e) => {
        e.preventDefault();
        // onSubmit(formData, finish);
        onSubmit(formData, false);


        // Reset the form only if we're adding another measurement
        setFormData({
            recipeId: recipeId,
            ingredientId: '',
            amount: 0
        });
    };

    return (
        <form className="add-measurement-form" onSubmit={handleSubmit}>
            {/* Ingredient dropdown */}
            <div>
                <label htmlFor="ingredientId">Select Ingredient:</label>
                <select name="ingredientId" value={formData.ingredientId} onChange={handleChange} required>
                    <option value="">--Select an Ingredient--</option>
                    {ingredients.map((ingredient) => (
                        <option key={ingredient.id} value={ingredient.id}>
                            {ingredient.name}
                        </option>
                    ))}
                </select>
            </div>

            {/* Amount input */}
            <div>
                <label htmlFor="amount">Amount:</label>
                <input
                    type="number"
                    name="amount"
                    value={formData.amount}
                    onChange={handleChange}
                    required
                />
            </div>

            {/* Buttons for adding another measurement or finishing */}
            <div>
                <button type="submit">Enter Another Measurement</button>
                <button type="button" onClick={() => onSubmit(formData, true)}>Finish</button> {/* Finish button */}
            </div>
        </form>
    );
};

// Add PropTypes validation
AddMeasurementsForm.propTypes = {
    onSubmit: PropTypes.func.isRequired,
    recipeId: PropTypes.string.isRequired,
    finish: PropTypes.bool
};

export default AddMeasurementsForm;
