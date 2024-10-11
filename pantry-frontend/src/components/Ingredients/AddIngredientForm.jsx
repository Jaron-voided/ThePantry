import React, { useState } from 'react';
import PropTypes from 'prop-types';

// Enums
const measuredInOptions = [
    { value: 0, label: "Weight" },
    { value: 1, label: "Volume" },
    { value: 2, label: "Each" }
];

const ingredientCategoryOptions = [
    { value: 0, label: "Spice" },
    { value: 1, label: "Meat" },
    { value: 2, label: "Vegetable" },
    { value: 3, label: "Fruit" },
    { value: 4, label: "Dairy" },
    { value: 5, label: "Grain" },
    { value: 6, label: "Liquid" },
    { value: 7, label: "Baking" }
];


const AddIngredientForm = ({ onSubmit }) => {
    // Form state
    const [formData, setFormData] = useState({
        name: '',
        measuredIn: 0, // Default value to empty string
        ingredientCategory: 0, // Default value to empty string
        pricePerPackage: '',
        measurementsPerPackage: ''
    });

// Handle form change and convert value to integer
    const handleChange = (e) => {
        const { name, value } = e.target;

        setFormData({
            ...formData,
            // Convert to integer if the field is `measuredIn` or `ingredientCategory`
            [name]: name === "measuredIn" || name === "ingredientCategory" ? parseInt(value, 10) : value
        });
    };


    // Handle form submission
    const handleSubmit = (e) => {
        e.preventDefault();
        // Convert formData to the correct shape to send to the API
        onSubmit(formData);
    };

    return (
        <form className="add-ingredient-form" onSubmit={handleSubmit}>
            {/* Ingredient Name */}
            <div>
                <label htmlFor="name">Ingredient Name</label>
                <input
                    type="text"
                    name="name"
                    value={formData.name}
                    onChange={handleChange}
                    required
                />
            </div>

            {/* Measured In Dropdown */}
            <div>
                <label htmlFor="measuredIn">Measured In</label>
                <select
                    name="measuredIn"
                    value={formData.measuredIn}
                    onChange={handleChange}
                >
                    {measuredInOptions.map((option) => (
                        <option key={option.value} value={option.value}>
                            {option.label}
                        </option>
                    ))}
                </select>
            </div>

            {/* Ingredient Category Dropdown */}
            <div>
                <label htmlFor="ingredientCategory">Ingredient Category</label>
                <select
                    name="ingredientCategory"
                    value={formData.ingredientCategory}
                    onChange={handleChange}
                >
                    {ingredientCategoryOptions.map((option) => (
                        <option key={option.value} value={option.value}>
                            {option.label}
                        </option>
                    ))}
                </select>
            </div>

            {/* Price Per Package */}
            <div>
                <label htmlFor="pricePerPackage">Price Per Package</label>
                <input
                    type="number"
                    step="0.01"
                    name="pricePerPackage"
                    value={formData.pricePerPackage}
                    onChange={handleChange}
                    required
                />
            </div>

            {/* Measurements Per Package */}
            <div>
                <label htmlFor="measurementsPerPackage">Measurements Per Package</label>
                <input
                    type="number"
                    name="measurementsPerPackage"
                    value={formData.measurementsPerPackage}
                    onChange={handleChange}
                    required
                />
            </div>

            {/* Submit Button */}
            <div>
                <button type="submit">Add Ingredient</button>
            </div>
        </form>
    );
};

// Add PropTypes validation
AddIngredientForm.propTypes = {
    onSubmit: PropTypes.func.isRequired
};

export default AddIngredientForm;
