import React, { useState } from 'react';
import utils from '../utils';
import { data } from '../data.js';
import '../styles/productBox.css';

function ProductCard(props) {
    const [item, setItem] = useState(data);
    return (
        <div className="product-card" >
            {item.map((item, index) => {
                return (
                    <div key={item.id} >
                        <figure className="card card-product" >
                            <div className="img-wrap">
                                <img src={item.imageUrl} alt="..." />
                            </div>
                            <div className="info-wrap">
                                <h5 className="title">{item.courseName}</h5>
                                <p className="des">{item.metaDescription}</p>
                                <div className="bottom-wrap">
                                    <button onClick={() => item.onAddCart(item)} className="cart btn pull-right">Add to Cart</button>
                                    <div className="price-wrap h5">
                                        <span className="price-new">{utils.formatCurrency(item.price)}</span>
                                    </div>
                                </div>
                            </div>
                        </figure>
                    </div>
                )
            })}
        </div>
    )
}

export default ProductCard;



