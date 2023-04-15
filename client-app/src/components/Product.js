import React from 'react';
import Filter from './Filter';
import Baskets from './Baskets';
import Menu from './Menu';
import ProductCard from './ProductCard';
import Subtotal from './Subtotal';

const Products = (props) => {
    return (
        <section className="product">
            <Menu />
            <Filter />
            <div className="product-container">
                <ProductCard />
            </div>
        </section>
    )
}

export default Products;