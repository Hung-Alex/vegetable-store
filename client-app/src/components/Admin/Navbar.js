import React from "react";
import { NavLink } from "react-router-dom";
import imageUser from "../../assets/images/user.png";
import "../../styles/Admin/navbar.css";

const Navbar = () => {
    return (
        <nav className="navadmin">
            <i className="bx bx-menu"></i>
            <form action="#">
                <div class="form-input">
                    <input type="search" placeholder="Search..." />
                    <button type="submit" className="search-btn">
                        <i className='bx bx-search'></i>
                    </button>
                    {/* <input type="checkbox" className="switch-mode" hidden />
                    <label for="switch-mode" className="switch-mode"></label> */}
                    <NavLink to="/admin" className="notification">
                        <i className='bx bxs-bell' ></i>
                        <span className="num">8</span>
                    </NavLink>
                    <NavLink to="/admin" className="profile">
                        <img src={imageUser} />
                    </NavLink>
                </div>
            </form>
        </nav>
    )
}

export default Navbar;