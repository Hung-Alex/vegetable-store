import React, { useEffect, useState } from "react";
import { isInteger, decode } from "../../utils/Utils";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import { Link, useParams, Navigate, useNavigate } from "react-router-dom";
import { isEmptyOrSpaces } from "../../utils/Utils";
import {
  addOrUpdateFood,
  getFoodById,
  getCategories,
} from "../../services/FoodRepository";
import { useSelector } from "react-redux";
import config from "../../config";

const AddProduct = () => {
  const initialState = {
      id: 0,
      name: "",
      unit: "",
      weight: "",
      image: "",
      description: "",
      price: 0,

      categoriesId: 0,
      showOnPage: false,
    },
    [food, setFood] = useState(initialState),
    [categories, setCategories] = useState([]);
  const navigate = useNavigate();

  const baseUrl = config.i18n.baseUrl.ApiUrl;
  const token = localStorage.getItem("token");

  useEffect(() => {
    document.title = "Add  food";

    getCategories().then((data) => {
      if (data) setCategories(data);
      else setCategories([]);
    });
  }, []);

  const handleSubmit = (e) => {
    e.preventDefault();

    let form = new FormData();
    form.append("id", String(food.id));
    form.append("name", String(food.name));
    form.append("description", String(food.description));
    form.append("weight", String(food.weight));
    form.append("unit", String(food.unit));
    form.append("imageFile", food.image);
    form.append("categoriesId", String(food.categoriesId));
    form.append("price", String(food.price));
    form.append("showOnPage", String(food.showOnPage));

    addOrUpdateFood(form, token, navigate).then((data) => {
      if (data === true) {
        alert("Success!");
        navigate("/admin/product");
      } else {
        alert("You cannot addd permission");
      }
    });
  };

  return (
    <>
      <div
        className="head-title"
        style={{ padding: "40px", overflowY: "scroll" }}
      >
        <Form
          method="post"
          encType="multipart/form-data"
          onSubmit={handleSubmit}
        >
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
                value={food.name || ""}
                onChange={(e) =>
                  setFood({
                    ...food,
                    name: e.target.value,
                  })
                }
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
                value={decode(food.description || "")}
                onChange={(e) =>
                  setFood({
                    ...food,
                    description: e.target.value,
                  })
                }
              />
            </div>
          </div>
          <div className="row mb-3" style={{ marginBottom: "30px" }}>
            <Form.Label className="col-sm-2 col-form-label">Unit</Form.Label>
            <div className="col-sm-10">
              <Form.Control
                type="text"
                name="unit"
                title="unit"
                required
                value={food.unit || ""}
                onChange={(e) =>
                  setFood({
                    ...food,
                    unit: e.target.value,
                  })
                }
              />
            </div>
          </div>
          <div className="row mb-3" style={{ marginBottom: "30px" }}>
            <Form.Label className="col-sm-2 col-form-label">Weight</Form.Label>
            <div className="col-sm-10">
              <Form.Control
                type="text"
                name="weight"
                title="weight"
                required
                value={food.weight || ""}
                onChange={(e) =>
                  setFood({
                    ...food,
                    weight: e.target.value,
                  })
                }
              />
            </div>
          </div>
          <div className="row mb-3" style={{ marginBottom: "30px" }}>
            <Form.Label className="col-sm-2 col-form-label">Price</Form.Label>
            <div className="col-sm-10">
              <Form.Control
                type="number"
                name="price"
                title="price"
                required
                value={food.price || ""}
                onChange={(e) =>
                  setFood({
                    ...food,
                    price: e.target.value,
                  })
                }
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
                onChange={(e) =>
                  setFood({
                    ...food,
                    categoriesId: e.target.value,
                  })
                }
              >
                <option value="">-- Choose Category --</option>
                {categories.length > 0 &&
                  categories.map((item, index) => (
                    <option key={index} value={item.id}>
                      {item.name}
                    </option>
                  ))}
              </Form.Select>
            </div>
          </div>
          {!isEmptyOrSpaces(food.image) && (
            <div className="row mb-3">
              <Form.Label className="col-sm-2 col-form-label">
                Picture present
              </Form.Label>
              <div className="col-sm-10">
                <img src={food.image} alt={food.name} />
              </div>
            </div>
          )}
          <div className="row mb-3" style={{ marginBottom: "30px" }}>
            <Form.Label className="col-sm-2 col-form-label">
              Choose picture
            </Form.Label>
            <div className="col-sm-10">
              <Form.Control
                type="file"
                name="image"
                accept="image/*"
                title="Image file"
                onChange={(e) =>
                  setFood({
                    ...food,
                    image: e.target.files[0],
                  })
                }
              />
            </div>
          </div>
          <div className="row mb-3" style={{ marginBottom: "30px" }}>
            <div className="col-sm-10 offset-sm-2">
              <div className="form-check">
                <input
                  className="form-check-input"
                  type="checkbox"
                  name="published"
                  checked={food.showOnPage}
                  title="published"
                  onChange={(e) =>
                    setFood({
                      ...food,
                      showOnPage: e.target.value,
                    })
                  }
                />
                <Form.Label className="form-check-label">Show food</Form.Label>
              </div>
            </div>
          </div>
          <div className="text-center">
            <Button
              variant="primary"
              type="submit"
              style={{ marginRight: "20px" }}
            >
              Save
            </Button>
            <Link to="/admin/product" className="btn btn-danger ms-2">
              Cancel and back
            </Link>
          </div>
        </Form>
      </div>
    </>
  );
};

export default AddProduct;
