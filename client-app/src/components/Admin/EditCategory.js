import React, { useState, useEffect } from "react";
import { Link, useParams, useNavigate, Navigate } from "react-router-dom";
import { isInteger, decode } from "../../utils/Utils";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import { getUserById } from "../../services/UserRepository";
import config from "../../config";
import { getCategoryById, updateCategory } from "../../services/CategoryRepository";

const EditCategory = () => {
    const initialState = {
        id: 0,
        name: ''
    },
        [cate, setCate] = useState(initialState);

    const navigate = useNavigate();
    let { id } = useParams();
    id = id ?? 0;

    const baseUrl = config.i18n.baseUrl.ApiUrl;
    const token = localStorage.getItem('token');

    useEffect(() => {
        document.title = 'Update category';

        getCategoryById(token, id).then(data => {
            if (data)
                setCate({
                    ...data,
                });
            else
                setCate(initialState);
        }, [token, id]);
    }, []);

    const handleSubmit = (e) => {
        e.preventDefault();

        let form = new FormData();
        form.append("id", String(cate.id));
        form.append("name", String(cate.name));

        updateCategory(id, form, token, navigate).then(data => {
            if (data === true) {
                alert("Success!");
                navigate("/admin/category");
            } else {
                alert("You cannot edit permission");
            }
        })
        // const check = updateCategory(form, token, navigate);
        // if (check) {
        //     alert("Success!");
        //     navigate("/admin/category");
        // } else {
        //     alert("Error");
        // }
    }

    if (id && !isInteger(id))
        return (
            <Navigate to={`/400?redirectTo=/admin/user`} />
        )
    return (
        <>
            <div className="head-title" style={{ padding: "40px", overflowY: "scroll" }}>
                <Form method="post" encType="multipart/form-data" onSubmit={(e) => handleSubmit(e)}>
                    <Form.Control type="hidden" name="id" value={cate.id} />
                    <div className="row mb-3" style={{ marginBottom: "30px" }}>
                        <Form.Label className="col-sm-2 col-form-label">
                            Category name
                        </Form.Label>
                        <div className="col-sm-10">
                            <Form.Control
                                type="text"
                                name="name"
                                title="name"
                                required
                                value={cate.name || ''}
                                onChange={e => setCate({
                                    ...cate,
                                    name: e.target.value
                                })}
                            />
                        </div>
                    </div>
                    <div className="text-center">
                        <Button variant="primary" type="submit" style={{ marginRight: "20px" }}>
                            Save
                        </Button>
                        <Link to='/admin/product' className="btn btn-danger ms-2">
                            Cancel and back
                        </Link>
                    </div>
                </Form>
            </div>
        </>
    )
}

export default EditCategory;