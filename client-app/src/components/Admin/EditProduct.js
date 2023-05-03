import React, { useEffect, useState } from "react";
import { isInteger, decode } from "../../utils/Utils";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import { Link, useParams, Navigate, useNavigate } from "react-router-dom";
import { isEmptyOrSpaces } from "../../utils/Utils";
import { addOrUpdateFood, getFoodById, getCategories } from "../../services/FoodRepository";
import { useSelector } from "react-redux";

const EditProduct = () => {
    const initialState = {
        id: 0,
        name: '',
        unit: '',
        weight: '',
        image: '',
        description: '',
        price: 0,
        categories: 0,
        showOnPage: false
    },
        [food, setFood] = useState(initialState),
        [categories, setCategories] = useState([]);
    const navigate=useNavigate();
    let { id } = useParams();
    id = id ?? 0;
    const user =useSelector((state)=>state.auth.login.currentUser);
    const token=user?.result.token;
    useEffect(() => {
        document.title = 'Add or update food';

        getFoodById(id).then(data => {
            if (data)
                setFood({
                    ...data
                });
            else
                setFood(initialState);
        });
        getCategories().then(data => {
            if (data)
                setCategories(data);
            else
                setCategories([]);
        });
    }, []);

    const handleSubmit = (e) => {
        e.preventDefault();
        let form = new FormData(e.target);
        addOrUpdateFood(form,token,navigate).then(data => {
            if (data)
                alert('Save food success!');
            else
                alert('Error!!!');
        });
    }

    if (id && !isInteger(id))
        return (
            <Navigate to={`/400?redirectTo=/admin/food`} />
        )
    return (
        <>
            <div className="head-title" style={{ padding: "40px", overflowY: "scroll" }}>
                <Form method="post" encType="multipart/form-data" onSubmit={handleSubmit}>
                    <Form.Control type="hidden" name="id" value={food.id} />
                    <div className="row mb-3" style={{ marginBottom: "30px" }}>
                        <Form.Label className="col-sm-2 col-form-label">
                            Name food
                        </Form.Label>
                        <div className="col-sm-10">
                            <Form.Control
                                type="text"
                                name="title"
                                title="Title"
                                required
                                value={food.name || ''}
                                onChange={e => setFood({
                                    ...food,
                                    name: e.target.value
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
                                value={decode(food.description || '')}
                                onChange={e => setFood({
                                    ...food,
                                    description: e.target.value
                                })}
                            />
                        </div>
                    </div>
                    <div className="row mb-3" style={{ marginBottom: "30px" }}>
                        <Form.Label className="col-sm-2 col-form-label">
                            Unit
                        </Form.Label>
                        <div className="col-sm-10">
                            <Form.Control
                                type="text"
                                name="unit"
                                title="unit"
                                required
                                value={food.unit || ''}
                                onChange={e => setFood({
                                    ...food,
                                    unit: e.target.value
                                })}
                            />
                        </div>
                    </div>
                    <div className="row mb-3" style={{ marginBottom: "30px" }}>
                        <Form.Label className="col-sm-2 col-form-label">
                            Weight
                        </Form.Label>
                        <div className="col-sm-10">
                            <Form.Control
                                type="text"
                                name="weight"
                                title="weight"
                                required
                                value={food.weight || ''}
                                onChange={e => setFood({
                                    ...food,
                                    weight: e.target.value
                                })}
                            />
                        </div>
                    </div>
                    <div className="row mb-3" style={{ marginBottom: "30px" }}>
                        <Form.Label className="col-sm-2 col-form-label">
                            Price
                        </Form.Label>
                        <div className="col-sm-10">
                            <Form.Control
                                type="number"
                                name="price"
                                title="price"
                                required
                                value={food.price || ''}
                                onChange={e => setFood({
                                    ...food,
                                    price: e.target.value
                                })}
                            />
                        </div>
                    </div>
                    <div className="row mb-3" style={{ marginBottom: "30px" }}>
                        <Form.Label className="col-sm-2 col-form-label">
                            Category
                        </Form.Label>
                        <div className="col-sm-10">
                            <Form.Select
                                name="categoryId"
                                title="categoryId"
                                required
                                value={food.categories.id}
                                onChange={e => setFood({
                                    ...food,
                                    categories: e.target.value
                                })}
                            >
                                <option value=''>-- Choose Category --</option>
                                {categories.length > 0 && categories.map((item, index) =>
                                    <option key={index} value={item.value}>{item.name}</option>)}
                            </Form.Select>
                        </div>
                    </div>
                    {!isEmptyOrSpaces(food.image) && <div className="row mb-3">
                        <Form.Label className="col-sm-2 col-form-label">
                            Picture present
                        </Form.Label>
                        <div className="col-sm-10">
                            <img src={food.image} alt={food.name} />
                        </div>
                    </div>}
                    <div className="row mb-3" style={{ marginBottom: "30px" }}>
                        <Form.Label className="col-sm-2 col-form-label">
                            Choose picture
                        </Form.Label>
                        <div className="col-sm-10">
                            <Form.Control
                                type="file"
                                name="imageFile"
                                accept="image/*"
                                title="Image file"
                                onChange={e => setFood({
                                    ...food,
                                    image: e.target.files[0]
                                })}
                            />
                        </div>
                    </div>
                    <div className="row mb-3" style={{ marginBottom: "30px" }}>
                        <div className="col-sm-10 offset-sm-2">
                            <div className="form-check">
                                <input className="form-check-input"
                                    type="checkbox"
                                    name="published"
                                    checked={food.showOnPage}
                                    title="published"
                                    onChange={e => setFood({
                                        ...food,
                                        showOnPage: e.target.value
                                    })}
                                />
                                <Form.Label className="form-check-label">
                                    Show food
                                </Form.Label>
                            </div>
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

export default EditProduct;