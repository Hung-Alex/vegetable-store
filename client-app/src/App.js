import './App.css';
import { BrowserRouter, Routes, Route } from "react-router-dom";

import Header from './components/Header';
import Homepage from './components/Homepage';
import Footer from './components/Footer';
import Help from './components/Help';
import Product from './components/Product';
import About from './components/About';
import Login from './components/Login';

function App() {
  return (
    <BrowserRouter>
      <Header />
      <Routes>
        <Route path='/' element={<Header />} />
        <Route index element={<Homepage />} />
        <Route path='/help' element={<Help />} />
        <Route path="/product" element={<Product />} />
        <Route path='/about' element={<About />} />
        <Route path='/login' element={<Login />} />
      </Routes>
      <Footer />
    </BrowserRouter>

  );
}

export default App;
