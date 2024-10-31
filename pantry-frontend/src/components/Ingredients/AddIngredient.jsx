import React from 'react';
import Layout from "../../app/layout/layout.jsx";
import AddIngredientForm from "../../components/Ingredients/AddIngredientForm.jsx";
import { useNavigate } from 'react-router-dom';

const AddIngredient = () => {
    const navigate = useNavigate();

    // Function to handle form submission
    const handleAddIngredient = (ingredientData) => {
        // Convert Javascript object to the format expected by the API
        const ingredientDTO = {
            name: ingredientData.name,
            measuredIn: parseInt(ingredientData.measuredIn), // Ensure this is an integer
            ingredientCategory: parseInt(ingredientData.ingredientCategory), // Ensure this is an integer
            pricePerPackage: parseFloat(ingredientData.pricePerPackage),
            measurementsPerPackage: parseInt(ingredientData.measurementsPerPackage)
        };

        // Log the data being sent to the API for debugging
        console.log('Sending ingredient data:', ingredientDTO);


        // POST to your backend API to add the ingredient
        //fetch('https://localhost:5001/api/ingredients', {
        fetch('http://localhost:5000/api/ingredients', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(ingredientDTO)
        })
            .then((response) => {
                if (response.ok) {
                    return response.json();
                }
                // Log the response text for debugging
                return response.text().then(text => {
                    throw new Error(`Failed to add ingredient: ${text}`);
                });
            })
            .then((data) => {
                console.log('Ingredient added successfully:', data);
                // Redirect to IngredientsPage after successful addition
                navigate('/ingredients');
            })
            .catch((error) => console.error('Error adding ingredients:', error));
    };
    return (
        <div className="add-ingredient-container">
            <h1>Add New Ingredient</h1>
            <AddIngredientForm onSubmit={handleAddIngredient} />
        </div>
    );
};

export default AddIngredient;