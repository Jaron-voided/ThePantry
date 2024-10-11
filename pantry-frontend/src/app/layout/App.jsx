import React from 'react';
import {BrowserRouter as Router, Link, Route, Routes} from "react-router-dom";
import Home from '../../pages/Home';
import IngredientsPage from '../../pages/Ingredients/IngredientsPage.jsx';
import RecipesPage from '../../pages/Recipes/RecipesPage.jsx';
import AddIngredientPage from "../../pages/Ingredients/AddIngredientPage.jsx";
import EditIngredientPage from "../../pages/Ingredients/EditIngredientPage.jsx";
import ViewIngredientPage from "../../pages/Ingredients/ViewIngredientPage.jsx";
import AddRecipePage from "../../pages/Recipes/AddRecipePage.jsx";
import ViewRecipePage from "../../pages/Recipes/ViewRecipePage.jsx";
import EditRecipePage from "../../pages/Recipes/EditRecipePage.jsx";
import AddMeasurementsPage from "../../pages/Measurements/AddMeasurementsPage.jsx";


const App = () => (
    <Router>
        <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/ingredients" element={<IngredientsPage />} />
            <Route path="/recipes" element={<RecipesPage />} />
            <Route path="/ingredients/add" element={<AddIngredientPage />} />
            <Route path="/ingredients/view/:id" element={<ViewIngredientPage />} />
            <Route path="/ingredients/edit/:id" element={<EditIngredientPage />} />
            <Route path="/recipes/add" element={<AddRecipePage />} />
            <Route path="/recipes/view/:id" element={<ViewRecipePage />} />
            <Route path="/recipes/edit/:id" element={<EditRecipePage />} />
            <Route path="/recipes/measurements/:recipeId" element={<AddMeasurementsPage />} />

        </Routes>
    </Router>
);

export default App;