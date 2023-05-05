import React, { useState, useEffect } from "react";
import Filter from "./Filter";
import Category from "./Category";
import Product from "./Product";
import { getFoods } from "../Service/FoodService";
import { GetCategory } from "../Service/Category";
import { useLocation } from "react-router-dom";

const ListProduct = () => {
  const [category, setCategory] = useState([]);
  const [products, setProducts] = useState([]);
  const [pagingdata, setPagingdata] = useState({});

  function useQuery() {
    const { search } = useLocation();
    return React.useMemo(() => new URLSearchParams(search), [search]);
  }
  let query = useQuery(),
    cate = query.get('category') ?? '';

  useEffect(() => {
    getFoods('', '', cate, 10, 1, '', '').then((data) => {
      if (data) {
        setProducts(data.items);
      } else {
        setProducts([]);
      }
    });
    GetCategory().then((data) => {
      if (data) {
        setCategory(data);
      } else {
        setCategory([]);
      }
    });
  }, cate);
  return (
    <div className="container-fluid">
      <div className="row no-gutters">
        <div className="col-12 col-sm-6 col-md-8">
          <Filter />
          <div className="List-Product">
            {products.map((item, index) => {
              return <Product key={index} item={item} />;
            })}
          </div>
        </div>
        <div className="col-6 col-md-4">
          <Category />
        </div>
      </div>
    </div>
  );
};
export default ListProduct;
