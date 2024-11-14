import React, {useEffect, useState} from 'react';
import PropTypes from 'prop-types';
import {useParams} from "react-router-dom";

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


const EditIngredientForm = ({ onSubmit }) => {
    const { id } = useParams();
    const [ingredient, setIngredient] = useState(null)

    // Form state
    const [formData, setFormData] = useState({
        name: '',
        measuredIn: '',
        ingredientCategory: '',
        pricePerPackage: '',
        measurementsPerPackage: ''
    });

    useEffect(() => {
        // Fetch Ingredient details from the API
        //fetch(`https://localhost:5001/ingredients/${id}`)
        fetch(`${import.meta.env.VITE_API_URL}/ingredients/${id}`)

            .then((response) => {
                if (response.ok) {
                    return response.json();
                }
                throw new Error('Failed to fetch ingredient');
            })
            .then((data) => {
                setIngredient(data);
                // Initialize formData once the ingredient is fetched
                setFormData({
                    name: data.name,
                    measuredIn: data.measuredIn,
                    ingredientCategory: data.ingredientCategory,
                    pricePerPackage: data.pricePerPackage,
                    measurementsPerPackage: data.measurementsPerPackage
                });
            })
            .catch((error) => console.error('Error fetching ingredient:', error));
    }, [id]);
    // Loading state

    if (!ingredient) {
        return <div>Loading...</div>
    }


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
                <button type="submit">Edit Ingredient</button>
            </div>
        </form>
    );
};

// Edit PropTypes validation
EditIngredientForm.propTypes = {
    onSubmit: PropTypes.func.isRequired
};

export default EditIngredientForm;
