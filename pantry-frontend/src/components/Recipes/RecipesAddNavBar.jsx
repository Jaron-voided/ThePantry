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
