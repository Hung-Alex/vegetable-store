import './App.css';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import React from 'react';
import Header from './components/Header';
import Homepage from './components/Homepage';
import Footer from './components/Footer';
import Help from './components/Help';

import Contact from './components/Contact';
import Signin from './components/Signin';
import Signup from './components/Signup';
import Filter from './components/Filter';
import ListProduct from './components/ListProduct';


function App() {
  return (
    <BrowserRouter>
      <Header />
      <Routes>
        <Route path='/' element={<Header />} />
        <Route index element={<Homepage />} />
        <Route path='/help' element={<Help />} />
       <Route path='/product'element={<ListProduct  />}/>
        <Route path="/contact" element={<Contact />} />
        <Route path="/signin" element={<Signin/>}/>
        <Route path="/signup" element={<Signup/>}/>
        

      </Routes>
      <Footer />
    </BrowserRouter>

  );
}

export default App;
