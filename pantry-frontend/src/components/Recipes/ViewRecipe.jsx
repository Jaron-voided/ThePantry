import React, {useEffect, useState} from 'react';
import { useParams } from 'react-router-dom';
import { getRecipeCategoryLabel } from "../../utils/enumHelpers.js";
import '../../app/layout/styles/recipe/viewRecipe.css';


const ViewRecipe = () => {
    const { id } = useParams();
    const [recipe, setRecipe] = useState(null);
    const [measurements, setMeasurements] = useState([]);
    const [ingredientMap, setIngredientMap] = useState({});
    const [totalPrice, setTotalPrice] = useState(0);
    const [pricePerServing, setPricePerServing] = useState(0);

    // Fetch recipe details
    useEffect(() => {
        fetch(`${import.meta.env.VITE_API_URL}/api/recipes/${id}`, )
            .then((response) => {
                if (response.ok) {
                    return response.json();
                }
                throw new Error('Failed to fetch recipe');
            })
            .then((data) => setRecipe(data))
            .catch((error) => console.error('Error adding recipe:', error));
        },  [id]);

    // Fetch the total price for the recipe
    useEffect(() => {
        if (recipe && recipe.id) {
            fetch(`${import.meta.env.VITE_API_URL}/api/recipes/totalPrice/${recipe.id}`)
                .then((response) => {
                    if (response.ok) {
                        return response.json();
                    }
                    throw new Error('Failed to fetch total price');
                })
                .then((data) => setTotalPrice(data))
                .catch((error) => console.error('Error fetching total price:', error));
        }
    }, [recipe]);

    // Fetch the price per serving for the recipe
    useEffect(() => {
        if (recipe && recipe.id) {
            fetch(`${import.meta.env.VITE_API_URL}/api/recipes/pricePerServing/${recipe.id}`, )
                .then((response) => {
                    if (response.ok) {
                        return response.json();
                    }
                    throw new Error('Failed to fetch price per serving');
                })
                .then((data) => setPricePerServing(data))
                .catch((error) => console.error('Error fetching price per serving:', error));
        }
    }, [recipe]);


    // Fetch the measurements for the recipe
    useEffect(() => {
        if (recipe && recipe.id) {
            fetch(`${import.meta.env.VITE_API_URL}/api/measurements?recipeId=${recipe.id}`)//this looks different then normal because its passing data to the api
                //recipeId=${recipe.id} is the actual query parameter, where recipeId is the key and ${recipe.id} is the value you're passing (using template literals to dynamically insert recipe.id).
                .then((response) => {
                    if (response.ok) {
                        return response.json();
                    }
                    throw new Error('Failed to fetch measurements');
                })
                .then((data) => {
                    setMeasurements(data)

                    // Extract unique ingredientIds from the measurements
                    const ingredientIds = [...new Set(data.map(m => m.ingredientId))];

                    // Fetch ingredient names based on their IDs
                    fetchIngredients(ingredientIds);
                })
                .catch((error) => console.error('Error fetching measurement:', error));
        }
    }, [recipe]);

    // Fetch ingredient names based on their IDs
    const fetchIngredients = (ingredientIds) => {
        Promise.all(ingredientIds.map(id =>
            fetch(`${import.meta.env.VITE_API_URL}/api/ingredients/${id}`)
                .then(response => response.json())
        )).then(ingredientData => {
            const ingredientMap = ingredientData.reduce((map, ingredient) => {
                map[ingredient.id] = ingredient.name;
                return map;
            }, {});
            setIngredientMap(ingredientMap);
        }).catch(error => console.error('Error fetching ingredients:', error));
    };

    // Loading state
    if (!recipe) {
        return <div>Loading...</div>
    }

    // Logging
    console.log('Fetched Recipe:', recipe);

    return (
        <div className="recipe-container">
            <h1 className="recipe-header">View Recipe</h1>
            <div className="recipe-details">
                <p className="recipe-row"><strong>Name:</strong> {recipe.name}</p>
                <p className="recipe-row"><strong>Category:</strong> {recipe.recipeCategory}</p>
                <p className="recipe-row"><strong>Instructions:</strong> {recipe.instructions.join(', ')}</p>
                <p className="recipe-row"><strong>Servings Per Recipe:</strong> {recipe.servingsPerRecipe}</p>

                {/* Total Price and Price per Serving */}
                <p className="recipe-row"><strong>Total Price for Recipe:</strong> ${totalPrice.toFixed(2)}</p>
                <p className="recipe-row"><strong>Price per Serving:</strong> ${pricePerServing.toFixed(2)}</p>

                {/* Ingredients and Measurements */}
                <h2 className="recipe-row">Ingredients and Measurements</h2>
                {measurements.length > 0 ? (
                    <ul className="recipe-measurements">
                        {measurements.map((measurement) => (
                            <li key={measurement.id} className="recipe-row">
                                <strong>Ingredient:</strong> {ingredientMap[measurement.ingredientId] || 'Loading...'} -
                                <strong>Amount:</strong> {measurement.amount}
                            </li>
                        ))}
                    </ul>
                ) : (
                    <p className="recipe-row">No measurements found for this recipe.</p>
                )}
            </div>
        </div>
    );
};

export default ViewRecipe;