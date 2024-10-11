import React, {useEffect, useState} from 'react';
import '/src/app/layout/styles/recipe/addRecipePage.css';
import Layout from "../../app/layout/layout.jsx";
import AddRecipe from "../../components/Recipes/AddRecipe.jsx";

const AddRecipePage = () => (
    <Layout>
        <div className="content">
            <h1> Add Recipe </h1>
            < AddRecipe />
        </div>
    </Layout>
);

export default AddRecipePage;