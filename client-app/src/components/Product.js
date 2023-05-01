import React, { useState } from "react";
import "../styles/ListProduct.css";
import { Link, useNavigate } from "react-router-dom";
import rangge from "../assets/images/khoaitay.jpeg";
import { useSelector } from "react-redux";
import { AddToCart } from "../Service/Cart";
import config from "../config";
import "../styles/Product.css"


const Product = ({ item }) => {
  const [quantity, setQuantity] = useState(1);
  const [foodId, setFoodId] = useState(item.id);
  const navigate =useNavigate();
  const user =useSelector((state)=>state.auth.login.currentUser);
  const token=user?.result.token;
  const baseUrl=config.i18n.baseUrl.ApiUrl;

  const img=item.image?baseUrl+item.image:rangge;

  const handleAddToCart = (e) => {
    e.preventDefault();
    if(user==null) return navigate("/signin");
    const food={
      id:foodId,
      quantity:quantity
    }
    AddToCart(token,food,navigate);

  };
  return (
    <form onSubmit={handleAddToCart}>
      <div className="card Product" style={{ width: "18rem" }}>
        <img className="card-img-top" src={img} alt="Card image cap" width={"100%"} height={"300px"} />
        <div className="card-body">
          <h5 className="card-title">{item.name}</h5>
          
        </div>
        <ul className="list-group list-group-flush">
          <li className="list-group-item">Unit : {item.unit}</li>
          <li className="list-group-item">Price : {item.price} </li>
          <li className="list-group-item"> Weight :{item.weight} </li>
          <li className="list-group-item">
          <label htmlFor="quantity">Quantity:</label>
          <input type="number" name="quantity" defaultValue={1} min="1" max="5" onChange={(e)=>setQuantity(Number.parseInt(e.target.value))} />
          </li>

        </ul>
        <div className="card-body">
          
        <button type="submit" className="btn btn-primary">Add to cart</button>
        </div>
      </div>
    </form>
  );
};

export default Product;
