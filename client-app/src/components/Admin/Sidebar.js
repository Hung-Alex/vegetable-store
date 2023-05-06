import React from "react";
import { NavLink } from "react-router-dom";
import "../../styles/Admin/sidebar.css";

const Sidebar = () => {
    return (
        <section className="sidebar">
            <NavLink to="/admin" className="brand">
                <i className="bx bxs-smile"></i>
                <span className="sidebar-text">Agri</span>
            </NavLink>
            <ul className="side-menu top">
                <li className="active">
                    <NavLink to="/admin">
                        <i className='bx bxs-dashboard' ></i>
                        <span className="sidebar-text">Dashboard</span>
                    </NavLink>
                </li>
                <li>
                    <NavLink to="/admin/product">
                        <i className='bx bxs-shopping-bag-alt' ></i>
                        <span className="sidebar-text">Product Management</span>
                    </NavLink>
                </li>
                <li>
                    <NavLink to="/admin/category">
                        <i className='bx bxs-book' ></i>
                        <span className="sidebar-text">Category Management</span>
                    </NavLink>
                </li>
                <li>
                    <NavLink to="/admin/user">
                        <i className='bx bxs-user' ></i>
                        <span className="sidebar-text">User Management</span>
                    </NavLink>
                </li>
                <li>
                    <NavLink to="/admin/order">
                        <i className='bx bxs-cart' ></i>
                        <span className="sidebar-text">Order Management</span>
                    </NavLink>
                </li>
                <li>
                    <NavLink to="/admin/feedback">
                        <i className='bx bxs-message-dots' ></i>
                        <span className="sidebar-text">Feedback Management</span>
                    </NavLink>
                </li>
            </ul>
            <ul className="side-menu">
                <li>
                    <NavLink to="/admin/setting">
                        <i className='bx bxs-cog' ></i>
                        <span className="sidebar-text">Settings</span>
                    </NavLink>
                </li>
                <li>
                    <NavLink to="/admin/logout" className="logout">
                        <i className='bx bxs-log-out-circle' ></i>
                        <span className="sidebar-text">Logout</span>
                    </NavLink>
                </li>
            </ul>
        </section>
    )
}

export default Sidebar;