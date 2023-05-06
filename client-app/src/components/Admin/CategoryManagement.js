import React, { useState, useEffect } from "react";
import { getCategoryList } from "../../services/CategoryRepository";
import config from "../../config";
import Table from "react-bootstrap/Table";
import { NavLink } from "react-router-dom";

const CategoryManagement = () => {
    const [cateList, setCateList] = useState([]);
    console.log(cateList);
    const baseUrl = config.i18n.baseUrl.ApiUrl;
    const token = localStorage.getItem('token');

    useEffect(() => {
        document.title = "Category list";

        getCategoryList(token).then(data => {
            if (data)
                setCateList(data);
            else
                setCateList([]);
        }, [token]);
    }, []);

    const handleDelete = (item) => {

    }

    return (
        <div className="head-title">
            <h1>Category Management</h1>
            <div className="product-management-container">
                <Table striped responsive bordered>
                    <thead>
                        <tr>
                            <th>STT</th>
                            <th>Name</th>
                            <th>Operation</th>
                        </tr>
                    </thead>

                    <tbody>
                        {cateList.length > 0 ? cateList.map((item, index) =>

                            <tr key={index}>
                                <td>
                                    {index + 1}
                                </td>
                                <td>
                                    <NavLink to={`/admin/category/edit/${item.id}`} className="text-blod">
                                        {item.name}
                                    </NavLink>
                                </td>
                                <td style={{ fontSize: "24px" }}>
                                    <NavLink to={`/admin/category/edit/${item.id}`}>
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

export default CategoryManagement;