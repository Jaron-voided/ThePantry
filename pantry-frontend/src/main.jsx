import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './app/layout/App.jsx'
import './app/layout/styles/styles.css'

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <App />
  </StrictMode>,
)