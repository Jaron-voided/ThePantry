import React, {useEffect, useState} from 'react';
import { useParams } from 'react-router-dom';
import { getMeasuredInLabel, getIngredientCategoryLabel } from '../../utils/enumHelpers';
import '/src/app/layout/styles/ingredient/ViewIngredientPage.css';

const ViewIngredient = () => {
    const { id } = useParams();
    const [ingredient, setIngredient] = useState(null);

    useEffect(() => {
        // Fetch Ingredient details from the API
        fetch(`${import.meta.env.VITE_API_URL}/ingredients/${id}`)
            .then((response) => {
                if (response.ok) {
                    return response.json();
                }
                throw new Error('Failed to fetch ingredient');
            })
            .then((data) => setIngredient(data))
            .catch((error) => console.error('Error adding ingredient:', error));
    }, [id]);

    // Loading state
    if (!ingredient) {
        return <div>Loading...</div>
    }

    // Logging
    console.log('Price Per Measurement:', ingredient.pricePerMeasurement);
    console.log('Fetched Ingredient:', ingredient);


    return (
        <div className="ingredient-container">
            <h1 className="ingredient-header">View Ingredient</h1>
            <div className="ingredient-details">
                <p className="ingredient-row"><strong>Name:</strong> {ingredient.name}</p>
                <p className="ingredient-row"><strong>Measured In:</strong> {getMeasuredInLabel(ingredient.measuredIn)}
                </p>
                <p className="ingredient-row">
                    <strong>Category:</strong> {getIngredientCategoryLabel(ingredient.ingredientCategory)}</p>
                <p className="ingredient-row"><strong>Price Per Package:</strong> ${ingredient.pricePerPackage}</p>
                <p className="ingredient-row"><strong>Measurements Per
                    Package:</strong> {ingredient.measurementsPerPackage}</p>
                <p className="ingredient-row"><strong>Price Per Measurement:</strong> ${ingredient.pricePerMeasurement}
                </p>
            </div>
        </div>
    );
};

export default ViewIngredient;