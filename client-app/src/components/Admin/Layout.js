import React from "react";
import { Outlet } from "react-router-dom";
import Navbar from "../Admin/Navbar";
import Sidebar from "../Admin/Sidebar";
import "../../styles/Admin/navbar.css";

const AdminLayout = () => {
    return (
        <>
            <div className="content">
                <Navbar />
                <Sidebar />
                <Outlet />
            </div>
        </>
    )
}

export default AdminLayout;