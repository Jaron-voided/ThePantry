import React, { useState } from 'react';
import Layout from "../../app/layout/layout.jsx";
import RecipesAddNavbar from "../../components/Recipes/RecipesAddNavBar.jsx";
import RecipesList from "../../components/Recipes/RecipesList.jsx";
import { useNavigate } from 'react-router-dom';

const RecipePage = () => {
    const [sortByCategory, setSortByCategory] = useState("");
    const [selectedIngredient, setSelectedIngredient] = useState("");
    const [priceSortOrder, setPriceSortOrder] = useState("");

    const navigate = useNavigate();

    const handleCategorySort = (category) => {
        setSortByCategory(category);
    };

    const handlePriceSort = () => {
        setPriceSortOrder("asc");
    };

    const handleIngredientSelect = (ingredientId) => {
        setSelectedIngredient(ingredientId);
    };

    const addRecipe = () => {
        navigate('/recipes/add');
    }

    return (
        <Layout>
            {/* Pass sorting and filtering props to the navbar */}
            <RecipesAddNavbar

                sortByCategory={sortByCategory}
                handleCategorySort={handleCategorySort}
                handlePriceSort={handlePriceSort}
                selectedIngredient={selectedIngredient}
                handleIngredientSelect={handleIngredientSelect}
                addRecipe={addRecipe}
            />

            <div className="content">
                <h1>Recipe Page</h1>

                {/* Pass sorting and filtering values to the RecipesList */}
                <RecipesList
                    sortByCategory={sortByCategory}
                    priceSortOrder={priceSortOrder}
                    selectedIngredient={selectedIngredient}
                />
            </div>
        </Layout>
    );
};

export default RecipePage;


/*
import React from 'react';
import Layout from "../../app/layout/layout.jsx";
import RecipesAddNavbar from "../../components/Recipes/RecipesAddNavBar.jsx";
import RecipesList from "../../components/Recipes/RecipesList.jsx";

const RecipePage = () => (
    <Layout>
        < RecipesAddNavbar />
        <div className="content">
            <h1>Recipe Page</h1>
            < RecipesList />
        </div>
    </Layout>
);

export default RecipePage;*/
