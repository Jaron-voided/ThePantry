import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import PropTypes from "prop-types";
import '../../app/layout/styles/recipe/addRecipeNavbar.css';


const RecipesAddNavbar = ({
                              sortByCategory,
                              handleCategorySort,
                              handlePriceSort,
                              selectedIngredient,
                              handleIngredientSelect,
                              addRecipe
}) => {
    const [ingredients, setIngredients] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');



    // Fetch the ingredients when the component loads
    useEffect(() => {
        fetch(`${import.meta.env.VITE_API_URL}/ingredients`)
            .then(response => response.json())
            .then(data => {
                setIngredients(data);
                setLoading(false);
            })
            .catch((error) => {
                console.error('Error loading ingredients:', error);
                setError('Failed to load ingredients');
                setLoading(false);
            });
    }, []);

    if (loading) {
        return <div>Loading Ingredients....</div>;
    }

    if (error) {
        return <div>Error: {error}</div>;
    }

    return (
        <nav className="add-navbar">
            <div className="add">
                <label>Add A Recipe</label>
                <button onClick={addRecipe} className="add-recipe-button">
                    Add a recipe!
                </button>
            </div>

            {/* Category Sorting */}
            <div className="sort-category">
                <label>Sort by Category: </label>
                <select
                    value={sortByCategory}
                    onChange={(e) => handleCategorySort(e.target.value)}
                >
                    <option value="">All</option>
                    <option value="Soup">Soup</option>
                    <option value="Dessert">Dessert</option>
                    <option value="Appetizer">Appetizer</option>
                    <option value="Dinner">Dinner</option>
                    {/* Add more categories as needed */}
                </select>
            </div>

            {/* Price Sorting */}
            <div className="sort-price">
                <label>Sort by Price: </label>
                <button onClick={handlePriceSort}>Sort</button>
            </div>

            {/* Ingredient Dropdown */}
            <div className="filter-ingredient">
                <label>Select Ingredient: </label>
                <select name="ingredientId" value={selectedIngredient}
                        onChange={(e) => handleIngredientSelect(e.target.value)} required>
                    <option value="">--Select an Ingredient--</option>
                    {ingredients.map((ingredient) => (
                        <option key={ingredient.id} value={ingredient.id}>
                            {ingredient.name}
                        </option>
                    ))}
                </select>
            </div>
        </nav>
    );
};

// Add PropTypes validation
RecipesAddNavbar.propTypes = {
    sortByCategory: PropTypes.string.isRequired,
    handleCategorySort: PropTypes.func.isRequired,
    handlePriceSort: PropTypes.func.isRequired,
    selectedIngredient: PropTypes.string,
    handleIngredientSelect: PropTypes.func.isRequired,
    addRecipe: PropTypes.func.isRequired
};




export default RecipesAddNavbar;




/*
import React from 'react';
import { Link } from 'react-router-dom';

const RecipesAddNavbar = () => (
    <nav className="add-navbar">
        <ul>
            <li><Link to="/recipes/add">Add New Recipe</Link></li>
        </ul>
    </nav>
);

export default RecipesAddNavbar;
*/
