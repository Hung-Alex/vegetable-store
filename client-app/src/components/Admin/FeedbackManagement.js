import React, { useState, useEffect } from "react";
import Table from "react-bootstrap/Table";
import { NavLink } from "react-router-dom";
import config from "../../config";
import { Button } from "react-bootstrap";
import { getFeedbackList } from "../../services/FeedbackRepository";


const FeedbackManagement = () => {
    const [feedbackList, setFeedbackList] = useState([]);

    const baseUrl = config.i18n.baseUrl.ApiUrl;
    const token = localStorage.getItem('token');

    // console.log(feedbackList);

    useEffect(() => {
        document.title = "Order list";

        getFeedbackList(token).then(data => {
            if (data)
                setFeedbackList(data);
            else
                setFeedbackList([]);
        }, [token]);

    }, []);

    const handleDelete = (item) => {

        // const checkItem = deleteFood(item.id, token);
        // if (checkItem) {
        //     alert("Delete product success!");
        // }
        // else {
        //     alert("Delete product fail!");
        // }
    }

    return (
        <div className="head-title">
            <div className="head-container" style={{ display: "flex", justifyContent: "space-between" }}>
                <h1>Feedback Management</h1>
                <NavLink to={"/admin/order/add"}>
                    <Button className="btn btn-primary" style={{ height: "38px", alignSelf: "end", margin: "0 40px 20px 0" }}>Add new</Button>
                </NavLink>
            </div>

            <div className="product-management-container">
                <Table striped responsive bordered>
                    <thead>
                        <tr>
                            <th>STT</th>
                            <th>Title</th>
                            {/* <th>Description</th> */}
                            <th>Email</th>
                            <th>Meta</th>
                            <th>Shipping date</th>
                            <th>Operation</th>
                        </tr>
                    </thead>

                    <tbody>
                        {feedbackList.length > 0 ? feedbackList.map((item, index) =>
                            <tr key={index}>
                                <td>
                                    {index + 1}
                                </td>
                                <td>
                                    <NavLink to={`/admin/feedback/edit/${item.id}`} className="text-blod">
                                        {item.title}
                                    </NavLink>
                                    <p className="text-muted">{item.description}</p>
                                </td>
                                <td>{item.email}</td>
                                <td>{item.meta}</td>
                                <td>{item.shippingDate}</td>
                                <td style={{ fontSize: "24px" }}>
                                    <NavLink to={`/admin/feedback/edit/${item.id}`}>
                                        <i className="bx bx-edit"></i>
                                    </NavLink>
                                    <i className="bx bx-trash" onClick={() => handleDelete(item)}></i>
                                </td>
                            </tr>
                        ) :
                            <tr>
                                <td colSpan={4}>
                                    <h4 className="text-danger text-center">
                                        Feedback is not found.
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

export default FeedbackManagement;