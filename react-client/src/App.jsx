import { useState } from 'react'
import { Routes, Link, Route } from 'react-router-dom'
import Test from './pages/Test'
import './App.css'

function App() {
  const [count, setCount] = useState(0)

    return (
        <>
            <Routes>
                <Route path="client/test" element={ <Test/> } />
            </Routes>
        </>
    )
}

export default App
