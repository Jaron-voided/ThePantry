import React from 'react';
import Layout from "../../app/layout/layout.jsx";
import EditRecipe from "../../components/Recipes/EditRecipe.jsx";

const EditRecipePage = () => (
    <Layout>
        <div className="content">
            <h1> Edit Recipe </h1>
            < EditRecipe />
        </div>
    </Layout>
);

export default EditRecipePage;