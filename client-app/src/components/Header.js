import React, { useState } from 'react';
import { NavLink } from 'react-router-dom';
import { useSelector } from 'react-redux';

const Header = () => {
    const user = useSelector((state) => state.auth.login.currentUser);
    console.log(user);
    return (
        <nav className="navbar">
            <div className="container-fluid">
                <div className="navbar-header">
                    <NavLink className="navbar-brand" to="/">AgriCart</NavLink>
                </div>
                <ul className="nav navbar-nav navbar-right">
                    <li className="active"><NavLink to="/"><i className="fa fa-home icon"></i>Home</NavLink></li>
                    <li className="active"><NavLink to="/product"><i className="glyphicon glyphicon-search icon"></i>Products</NavLink></li>
                    <li className="active"><NavLink to="/about"><i className="fa fa-user icon"></i>About</NavLink></li>
                    <li><NavLink to="/help"><i className="fa fa-life-ring icon"></i>Help</NavLink></li>
                    <li><a><i className="fa fa-cart-plus icon"></i><span>Cart</span></a></li>
                    {user ? (
                        <>
                            <li><NavLink to="/logout"><i className="fa fa-user icon"></i>Log Out</NavLink></li>
                            <li><a>Hi, {user.result.username}</a></li>
                        </>

                    ) : (
                        <li><NavLink to="/login"><i className="fa fa-user icon"></i>Login</NavLink></li>
                    )}
                </ul>
            </div>
        </nav>
    )
}

export default Header;