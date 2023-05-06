import React, { useState, useEffect } from "react";
//import "../../styles/Admin/product.css";
import { getUsers } from "../../services/UserRepository";
import Table from "react-bootstrap/Table";
import { NavLink } from "react-router-dom";
import config from "../../config";

const UserManagement = () => {
    const [userList, setUserList] = useState([]);
    console.log(userList);
    const baseUrl = config.i18n.baseUrl.ApiUrl;
    const token = localStorage.getItem('token');

    let p = 1, ps = 10;

    useEffect(() => {
        document.title = "User list";

        getUsers(token, ps, p).then(data => {
            if (data)
                setUserList(data.items);
            else
                setUserList([]);
        }, [token, p, ps]);
        console.log(userList.length)
    }, [token, p, ps]);

    const handleDelete = (item) => {

        // const checkItem = deleteUser(item.id, token);
        // if (checkItem) {
        //     alert("Delete product success!");
        // }
        // else {
        //     alert("Delete product fail!");
        // }
    }

    return (
        <div className="head-title">
            <h1>User Management</h1>
            <div className="product-management-container">
                <Table striped responsive bordered>
                    <thead>
                        <tr>
                            <th>STT</th>
                            <th>Name</th>
                            <th>Password</th>
                            <th>Role</th>
                            <th>Operation</th>
                        </tr>
                    </thead>

                    <tbody>
                        {userList.length > 0 ? userList.map((item, index) =>

                            <tr key={index}>
                                <td>
                                    {index + 1}
                                </td>
                                <td>
                                    <NavLink to={`/admin/user/edit/${item.id}`} className="text-blod">
                                        {item.name}
                                    </NavLink>
                                    <p className="text-muted">{item.description}</p>
                                </td>
                                <td>{item.password}</td>
                                <td>{item.role}</td>
                                <td style={{ fontSize: "24px" }}>
                                    <NavLink to={`/admin/user/edit/${item.id}`}>
                                        <i className="bx bx-edit"></i>
                                    </NavLink>
                                    <i className="bx bx-trash" onClick={() => handleDelete(item)}></i>
                                </td>
                            </tr>
                        ) :
                            <tr>
                                <td colSpan={4}>
                                    <h4 className="text-danger text-center">
                                        User is not found.
                                    </h4>
                                </td>
                            </tr>
                        }
                    </tbody>
                </Table>
            </div>
        </div>
    )
}

export default UserManagement;