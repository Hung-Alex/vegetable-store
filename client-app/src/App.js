import './App.css';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import React from 'react';
import Header from './components/Header';
import Homepage from './components/Homepage';
import Footer from './components/Footer';
import Help from './components/Help';
import Product from './components/Product';
import About from './components/About';
import Login from './components/Login';
import Register from './components/Register';
import Layout from './components/Layout';
import AdminLayout from './components/Admin/Layout';
import Dashboard from './components/Admin/Dashboard';
import ProductManagement from './components/Admin/ProductManagement';
import EditProduct from './components/Admin/EditProduct';
import Contact from './components/Contact';
import Signin from './components/Signin';
import Signup from './components/Signup';
import Filter from './components/Filter';
import ListProduct from './components/ListProduct';
import DeTailProduct from './components/DetailsProduct';


function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<Layout />}>
          {/* <Route path='/' element={<Header />} /> */}
          <Route index element={<Homepage />} />
          <Route path='/help' element={<Help />} />
          <Route path='/product' element={<ListProduct />} />
          <Route path='/product/detail/:id' element={<DeTailProduct />} />
          <Route path='/about' element={<About />} />
          <Route path="/signin" element={<Signin />} />
          <Route path="/signup" element={<Signup />} />
        </Route>
        <Route path='/admin' element={<AdminLayout />}>
          <Route path='/admin' element={<Dashboard />} />
          <Route path='/admin/product' element={<ProductManagement />} />
          <Route path='/admin/product/edit' element={<EditProduct />} />
          <Route path='/admin/product/edit/:id' element={<EditProduct />} />
        </Route>
      </Routes>
      <Routes>
        <Route path='/register' element={<Register />} />

      </Routes>
      {/* <Footer /> */}
    </BrowserRouter>

  );
}

export default App;
