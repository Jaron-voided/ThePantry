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

export default RecipePage;