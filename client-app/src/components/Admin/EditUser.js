import React, { useState, useEffect } from "react";
import { Link, useParams, useNavigate, Navigate } from "react-router-dom";
import { isInteger, decode } from "../../utils/Utils";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import { getUserById, updateUser } from "../../services/UserRepository";
import config from "../../config";

const EditUser = () => {
    const initialState = {
        id: 0,
        name: '',
        password: '',
        role: 0
    },
        [user, setUser] = useState(initialState);
    const navigate = useNavigate();
    let { id } = useParams();
    id = id ?? 0;

    const baseUrl = config.i18n.baseUrl.ApiUrl;
    const token = localStorage.getItem('token');

    useEffect(() => {
        document.title = 'Add or update user';

        getUserById(token, id).then(data => {
            if (data)
                setUser({
                    ...data,
                });
            else
                setUser(initialState);
        }, [token, id]);
    }, []);

    const handleSubmit = (e) => {
        e.preventDefault();

        let form = new FormData();
        form.append("id", String(user.id));
        form.append("name", String(user.name));
        form.append("password", String(user.password));
        form.append("role", String(user.role));

        updateUser(id, form, token, navigate).then(data => {
            if (data === true) {
                alert("Success!");
                navigate("/admin/user");
            } else {
                alert("You cannot edit permission");
            }
        })
    }

    if (id && !isInteger(id))
        return (
            <Navigate to={`/400?redirectTo=/admin/user`} />
        )
    return (
        <>
            <div className="head-title" style={{ padding: "40px", overflowY: "scroll" }}>
                <Form method="post" encType="multipart/form-data" onSubmit={handleSubmit}>
                    <Form.Control type="hidden" name="id" value={user.id} />
                    <div className="row mb-3" style={{ marginBottom: "30px" }}>
                        <Form.Label className="col-sm-2 col-form-label">
                            User name
                        </Form.Label>
                        <div className="col-sm-10">
                            <Form.Control
                                type="text"
                                name="name"
                                title="name"
                                required
                                value={user.name || ''}
                                onChange={e => setUser({
                                    ...user,
                                    name: e.target.value
                                })}
                            />
                        </div>
                    </div>
                    <div className="row mb-3" style={{ marginBottom: "30px" }}>
                        <Form.Label className="col-sm-2 col-form-label">
                            Password
                        </Form.Label>
                        <div className="col-sm-10">
                            <Form.Control
                                type="text"
                                name="password"
                                title="password"
                                required
                                value={user.password || ''}
                                onChange={e => setUser({
                                    ...user,
                                    password: e.target.value
                                })}
                            />
                        </div>
                    </div>
                    <div className="row mb-3" style={{ marginBottom: "30px" }}>
                        <Form.Label className="col-sm-2 col-form-label">
                            Role
                        </Form.Label>
                        <div className="col-sm-10">
                            <Form.Select
                                name="role"
                                title="role"
                                required
                                value={user.role}
                                onChange={e => setUser({
                                    ...user,
                                    role: e.target.value
                                })}
                            >
                                <option value=''>-- Choose Role --</option>
                                <option value="admin">Admin</option>
                                <option value="user">User</option>
                            </Form.Select>
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

export default EditUser;