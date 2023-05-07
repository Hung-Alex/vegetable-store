import React, { useState, useEffect } from "react";
import { Link, useParams, useNavigate, Navigate } from "react-router-dom";
import { isInteger, decode } from "../../utils/Utils";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import config from "../../config";
import { getFeedbackById, updateFeedback } from "../../services/FeedbackRepository";

const EditFeedback = () => {
    const initialState = {
        id: 0,
        title: '',
        description: '',
        email: '',
        meta: '',
        shippingDate: '',
        status: 0
    },
        [feedback, setFeedback] = useState(initialState),
        [subject, setSubject] = useState('');
    const [body, setBody] = useState('');


    // console.log(feedback);
    const navigate = useNavigate();
    let { id } = useParams();
    id = id ?? 0;

    const baseUrl = config.i18n.baseUrl.ApiUrl;
    const token = localStorage.getItem('token');

    useEffect(() => {
        document.title = 'Update Feedback';

        getFeedbackById(token, id).then(data => {
            if (data)
                setFeedback({
                    ...data,
                });
            else
                setFeedback(initialState);
        }, [token, id]);
    }, []);

    const handleSubmit = (e) => {
        e.preventDefault();
        // if (!fileList) {
        //     return;
        // }
        let form = new FormData();

        // form.append("description", String(feedback.description));
        // form.append("email", String(feedback.email));
        // form.append("meta", String(feedback.meta));
        // form.append("shippingDate", String(feedback.shippingDate));
        // form.append("status", String(feedback.status));
        // form.append("Actachments", fileList);


        // 


        updateFeedback(id, form, token, navigate).then(data => {
            if (data === true) {
                alert("Success!");
                navigate("/admin/feedback");
            } else {
                alert("You cannot edit permission");
            }
        })
        const check = updateFeedback(id, form, token, navigate);
        if (check) {
            alert("Success!");
            navigate("/admin/feedback");
        } else {
            alert("Error");
        }
    }

    // const [fileList, setFileList] = useState < FileList | null > (null);

    // const handleFileChange = (e) => {
    //     setFileList(e.target.files);
    // };


    // const files = fileList ? [...fileList] : [];

    if (id && !isInteger(id))
        return (
            <Navigate to={`/400?redirectTo=/admin/user`} />
        )
    return (
        <>
            <div className="head-title" style={{ padding: "40px", overflowY: "scroll" }}>
                <Form method="post" encType="multipart/form-data" onSubmit={(e) => handleSubmit(e)}>
                    <Form.Control type="hidden" name="id" value={feedback.id} />
                    <div className="row mb-3" style={{ marginBottom: "30px" }}>
                        <Form.Label className="col-sm-2 col-form-label">
                            Title
                        </Form.Label>
                        <div className="col-sm-10">
                            <Form.Control
                                type="text"
                                name="title"
                                title="title"
                                required
                                value={feedback.title || ''}
                                onChange={e => setFeedback({
                                    ...feedback,
                                    title: e.target.value
                                })}
                            />
                        </div>
                    </div>
                    <div className="row mb-3" style={{ marginBottom: "30px" }}>
                        <Form.Label className="col-sm-2 col-form-label">
                            Description
                        </Form.Label>
                        <div className="col-sm-10">
                            <Form.Control
                                as="textarea"
                                type="text"
                                name="description"
                                title="description"
                                required
                                value={decode(feedback.description || '')}
                                onChange={e => setFeedback({
                                    ...feedback,
                                    description: e.target.value
                                })}
                            />
                        </div>
                    </div>
                    <div className="row mb-3" style={{ marginBottom: "30px" }}>
                        <Form.Label className="col-sm-2 col-form-label">
                            Email
                        </Form.Label>
                        <div className="col-sm-10">
                            <Form.Control
                                type="text"
                                name="email"
                                title="email"
                                required
                                value={feedback.email || ''}
                                onChange={e => setFeedback({
                                    ...feedback,
                                    email: e.target.value
                                })}
                            />
                        </div>
                    </div>
                    <div className="row mb-3" style={{ marginBottom: "30px" }}>
                        <Form.Label className="col-sm-2 col-form-label">
                            Meta
                        </Form.Label>
                        <div className="col-sm-10">
                            <Form.Control
                                as="textarea"
                                type="text"
                                name="meta"
                                title="meta"
                                required
                                value={decode(feedback.meta || '')}
                                onChange={e => setFeedback({
                                    ...feedback,
                                    meta: e.target.value
                                })}
                            />
                        </div>
                    </div>
                    <div className="row mb-3" style={{ marginBottom: "30px" }}>
                        <Form.Label className="col-sm-2 col-form-label">
                            Shipping date
                        </Form.Label>
                        <div className="col-sm-10">
                            <Form.Control
                                type="datetime-local"
                                name="datetime"
                                title="datetime"
                                required
                                value={feedback.shippingDate || ''}
                                onChange={e => setFeedback({
                                    ...feedback,
                                    shippingDate: e.target.value
                                })}
                            />
                        </div>
                    </div>
                    <div className="row mb-3" style={{ marginBottom: "30px" }}>
                        <Form.Label className="col-sm-2 col-form-label">
                            Subject
                        </Form.Label>
                        <div className="col-sm-10">
                            <Form.Control
                                as="textarea"
                                type="text"
                                name="Subject"
                                title="Subject"
                                required
                                value=''
                                onChange={e => setSubject(
                                    e.target.value
                                )}
                            />
                        </div>
                    </div>
                    <div className="row mb-3" style={{ marginBottom: "30px" }}>
                        <Form.Label className="col-sm-2 col-form-label">
                            Body
                        </Form.Label>
                        <div className="col-sm-10">
                            <Form.Control
                                as="textarea"
                                type="text"
                                name="body"
                                title="body"
                                required
                                value=''
                                onChange={e => setBody(
                                    e.target.value
                                )}
                            />
                        </div>
                    </div>
                    <div>
                        {/* <input type="file" onChange={handleFileChange} multiple /> */}

                        <ul>
                            {/* {files.map((file, i) => (
                                <li key={i}>
                                    {file.name} - {file.type}
                                </li>
                            ))} */}
                        </ul>


                    </div>
                    <div className="row mb-3" style={{ marginBottom: "30px" }}>
                        <div className="col-sm-10 offset-sm-2">
                            <div className="form-check">
                                <input className="form-check-input"
                                    type="checkbox"
                                    name="published"
                                    checked={feedback.status}
                                    title="published"
                                    onChange={e => setFeedback({
                                        ...feedback,
                                        status: e.target.value
                                    })}
                                />
                                <Form.Label className="form-check-label">
                                    Show feedback
                                </Form.Label>
                            </div>
                        </div>
                    </div >
                    <div className="text-center">
                        <Button variant="primary" type="submit" style={{ marginRight: "20px" }}>
                            Save
                        </Button>
                        <Link to='/admin/product' className="btn btn-danger ms-2">
                            Cancel and back
                        </Link>
                    </div>
                </Form >
            </div >
        </>
    )
}

export default EditFeedback;