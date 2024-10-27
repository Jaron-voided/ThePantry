import React, {useEffect, useState} from 'react';
import '/src/app/layout/styles/recipe/viewRecipe.css';
import Layout from "../../app/layout/layout.jsx";
import ViewRecipe from "../../components/Recipes/ViewRecipe.jsx";

const ViewRecipePage = () => (
    <Layout>
        <div className="content">
            <h1> View Recipe </h1>
            < ViewRecipe />
        </div>
    </Layout>
);

export default ViewRecipePage;