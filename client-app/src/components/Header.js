import React from 'react';
import { NavLink } from 'react-router-dom';

const Header = () => {
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
                    <li><NavLink to ="/contact"><i className="fa fa-life-ring icon"></i>Contact</NavLink></li>

                    {/* {props.user.userId != null ? (<li className="dropdown">
                        <a href="#" className="dropdown-toggle" data-toggle="dropdown"><span className="glyphicon glyphicon-user icon"></span>{props.user.email !== null ? props.user.email : "Hello, Sign in"}</a>
                        <ul className="dropdown-menu">
                            <li align="center" className="well">
                                {props.user.email === null ? (<a onClick={() => props.buy("signin")}><span className="glyphicon glyphicon-user icon"></span>Sign In</a>) : ''}
                                <a onClick={() => props.buy("profile")} > Profile</a>
                                <a onClick={() => props.buy("orders")} > Orders</a>
                                <a onClick={() => props.buy("favourites")} > Favourites</a>
                                {props.user.email !== null ? (<a onClick={props.logout} ><span className="glyphicon glyphicon-log-out"></span> Logout</a>) : ''}
                            </li>
                        </ul>
                    </li>) : ''} */}
                    <li><a><i className="fa fa-cart-plus icon"></i><span>Cart</span></a></li>
                   
                </ul>
            </div>
        </nav>
    )
}

export default Header;