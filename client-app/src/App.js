import './App.css';
import { BrowserRouter, Routes, Route } from "react-router-dom";

import Header from './components/Header';
import Homepage from './components/Homepage';
import Footer from './components/Footer';
import Help from './components/Help';
import Product from './components/Product';

function App() {
  return (
    <BrowserRouter>
      <Header />
      <Routes>
        <Route path='/' element={<Header />} />
        <Route index element={<Homepage />} />
        <Route path='/help' element={<Help />} />
        <Route path="/product" element={<Product />} />
      </Routes>
      <Footer />
    </BrowserRouter>

  );
}

export default App;
