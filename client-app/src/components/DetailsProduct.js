import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getFoodById } from "../services/FoodRepository";
import config from "../config";

const DeTailProduct = () => {
    const baseUrl = config.i18n.baseUrl.ApiUrl;
    const [quantity, setQuantity] = useState(1);

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
        [food, setFood] = useState(initialState)
    //console.log(food);
    let { id } = useParams();
    id = id ?? 0;

    useEffect(() => {
        document.title = 'Add or update food';

        getFoodById(id).then(data => {
            if (data)
                setFood({
                    ...data,
                });
            else
                setFood(initialState);
        });
    }, []);
    return (
        <>
            <div class="container" id="product-section" style={{ marginTop: "20px" }}>
                <div class="row">
                    <div class="col-md-6">
                        <img
                            src={baseUrl + food.image}
                            alt={food.name}
                            class="image-responsive"
                            style={{ objectFit: "cover", width: "650px", height: "800px", paddingRight: "20px" }}
                        />
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-12">
                                <h1>{food.name}</h1>
                            </div>
                            <div class="col-md-12">
                                <p class="description">
                                    {food.description}
                                </p>
                            </div>
                            <div class="col-md-12 bottom-rule">
                                <h2 class="product-price">{food.price} VND</h2>
                            </div>
                            <div class="col-md-12 bottom-rule" style={{ marginBottom: "10px" }}>
                                <input type="number" name="quantity" defaultValue={1} min="1" max="5" onChange={(e) => setQuantity(Number.parseInt(e.target.value))} />
                            </div>
                            <div class="col-md-4">
                                <button type="submit" className="btn btn-primary">Add to cart</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}

export default DeTailProduct;