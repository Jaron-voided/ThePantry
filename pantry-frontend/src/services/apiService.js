import axios from 'axios';

// Log the environment variable
console.log('VITE_API_URL:', import.meta.env.VITE_API_URL);

// Create an axios instance with the base URL from the .env file
const api = axios.create({
    baseURL: import.meta.env.VITE_API_URL,
    //baseURL: `${import.meta.env.VITE_API_URL}/api',
})

// Function to fetch ingredients
export const getIngredients = () => {
    // Log the URL being requested
    console.log('Requesting from:', `${api.defaults.baseURL}/ingredients`);
    
    return api.get('/ingredients')
        .then((response) => {
            console.log('Response:', response.data);
            return response.data;
        })
        .catch(error => {
            console.error('Error fetching ingredients:' ,error);
            throw error;
        });
};

// Function to fetch recipes
export const getRecipes = () => {
    const url = '/recipes';
    console.log('Requesting from:', `${api.defaults.baseURL}${url}`);

    return api.get(url)
        .then((response) => {
            console.log('Response:', response.data);
            return response.data;
        })
        .catch(error => {
            console.log('Error fetching recipes:' ,error);
            throw error;
        });
};

// Function to fetch recipes by category
export const getRecipesByCategory = (category) => {
    const url = `/recipes/byCategory/${category}`;
    console.log('Requesting from:', `${api.defaults.baseURL}${url}`);

    return api.get(url)
        .then((response) => {
            console.log('Response:', response.data);
            return response.data;
        })
        .catch(error => {
            console.log('Error fetching recipes:' ,error);
            throw error;
        });
};

// Function to fetch recipes by ingredient
export const getRecipesByIngredient = (ingredientId) => {
    const url = `/recipes/byIngredient/${ingredientId}`;
    console.log('Requesting from:', `${api.defaults.baseURL}${url}`);

    return api.get(url)
        .then((response) => {
            console.log('Response:', response.data);
            return response.data;
        })
        .catch(error => {
            console.log('Error fetching recipes:' ,error);
            throw error;
        });
};

// Function to fetch recipes by category
export const getRecipesByPrice = () => {
    const url = '/recipes/sortedByPrice';
    console.log('Requesting from:', `${api.defaults.baseURL}${url}`);

    return api.get(url)
        .then((response) => {
            console.log('Response:', response.data);
            return response.data;
        })
        .catch(error => {
            console.log('Error fetching recipes:' ,error);
            throw error;
        });
};

// Function to fetch measurements
export const getMeasurements = () => {
    console.log('Requesting from:', `${api.defaults.baseURL}/measurements`);

    return api.get('/measurements')
        .then((response) => {
            console.log('Response:', response.data);
            return response.data;
        })
        .catch(error => {
            console.log('Error fetching measurements:' ,error);
            throw error
        });
};

export default api;