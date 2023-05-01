import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { NavLink, useNavigate } from 'react-router-dom';
import { Link } from 'react-router-dom';
import { useState } from 'react';
import { logOutUser } from '../Service/LoginUser';

const Header = () => {
    
   
    // @ts-ignore
    const user =useSelector((state) => state.auth.login.currentUser);
    const dispatch=useDispatch();
    const navigate=useNavigate();
    const token=user?.result.token;
    const handleLogOut=()=>{
        logOutUser(token,dispatch,navigate);
    }
    return (
        <nav className="navbar">
            <div className="container-fluid">
                <div className="navbar-header">
                    <NavLink className="navbar-brand" to="/">AgriCart</NavLink>
                </div>
                <ul className="nav navbar-nav navbar-right">
                    <li className="active"><NavLink to="/product"><i className="glyphicon glyphicon-search icon"></i>Products</NavLink></li>
                    <li ><a><i className="fa fa-percent icon"></i>Offers</a></li>
                    <li><NavLink to="/help"><i className="fa fa-life-ring icon"></i>Help</NavLink></li>
                    <li><NavLink to="/contact"><i className="fa fa-star icon"></i>Contact</NavLink></li>

                  
                    <li><a><i className="fa fa-cart-plus icon"></i><span>Cart</span></a></li>
                    {user!=null? (
                        <>
                           <li><NavLink to="/"><i className="fa fa-sign-in icon"></i>Hi {user.result.role}</NavLink></li>
                           <li><NavLink to="/" onClick={handleLogOut}><i className="fa fa-sign-in icon" ></i>Logout</NavLink></li>

                        </>
                    ):(
                        <>
                           <li><NavLink to="/signin"><i className="fa fa-sign-in icon"></i>Sign In</NavLink></li>
                        </>
                    )}
                    

                </ul>
            </div>
        </nav>
    )
}

export default Header;