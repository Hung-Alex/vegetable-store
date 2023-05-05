import React, { useState, useEffect } from "react";
import "../../styles/Admin/product.css";
import { deleteFood, getFoods } from "../../services/FoodRepository";
import Table from "react-bootstrap/Table";
import { NavLink } from "react-router-dom";
import axios from "axios";
import config from "../../config";

const ProductManagement = () => {
    const [foodList, setFoodList] = useState([]);
    const [isVisibleLoading, setIsVisibleLoading] = useState(true);

    const baseUrl = config.i18n.baseUrl.ApiUrl;
    const token = localStorage.getItem('token')
    // console.log(foodList);
    let p = 1, ps = 10;

    useEffect(() => {
        document.title = "Food list";

        getFoods(ps, p).then(data => {
            if (data)
                setFoodList(data.items);
            else
                setFoodList([]);
            // setIsVisibleLoading(false);
        }, [p, ps]);
    }, []);

    const handleDelete = (item) => {
        let form = new FormData();

        deleteFood(form, token).then(() => {
            let newFoodList = foodList.filter((el) => el.id !== item.id);
            setFoodList(newFoodList);
        })
        // setFoodList(newFoodList);
    }

    return (
        <div className="head-title">
            <h1>Product Management</h1>
            <div className="product-management-container">
                <Table striped responsive bordered>
                    <thead>
                        <tr>
                            <th>STT</th>
                            <th>Name</th>
                            <th>Weight</th>
                            <th>Unit</th>
                            <th>Price</th>
                            <th>Category</th>
                            <th>Operation</th>
                        </tr>
                    </thead>

                    <tbody>
                        {foodList.length > 0 ? foodList.map((item, index) =>

                            <tr key={index}>
                                <td>
                                    {index + 1}
                                </td>
                                <td>
                                    <NavLink to={`/admin/product/edit/${item.id}`} className="text-blod">
                                        {item.name}
                                    </NavLink>
                                    <p className="text-muted">{item.description}</p>
                                </td>
                                <td>{item.weight}</td>
                                <td>{item.unit}</td>
                                <td>{item.price}</td>
                                <td>{item.categories.name}</td>
                                <td style={{ fontSize: "24px" }}>
                                    <NavLink to={`/admin/product/edit/${item.id}`}>
                                        <i className="bx bx-edit"></i>
                                    </NavLink>
                                    <i className="bx bx-trash" onClick={() => handleDelete(item)}></i>
                                </td>
                            </tr>
                        ) :
                            <tr>
                                <td colSpan={4}>
                                    <h4 className="text-danger text-center">
                                        Food not found.
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

export default ProductManagement;