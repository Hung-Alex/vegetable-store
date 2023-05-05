import React, { useEffect, useState } from "react";
import { GetCategory } from "../Service/Category";
import { Link } from "react-router-dom";
import ListGroup from "react-bootstrap/ListGroup";

const Category = () => {
  const [categories, setCategories] = useState([]);

  useEffect(() => {
    GetCategory().then(data => {
      if (data)
        setCategories(data);
      else
        setCategories([]);
    });
  }, []);

  console.log(categories);

  return (
    <>
      <div className="mb-4">
        <h3 className="text-success mb-2">
          Some Category
        </h3>
        {categories.length > 0 && categories.map((item, index) => {
          return (
            <ListGroup.Item key={index}>
              <Link to={`/product`}
                title={item.name}
                key={index}>
                {item.name}
                {/* <span>&nbsp;({item.postCount})</span> */}
              </Link>
            </ListGroup.Item>
          )
        })}
      </div>

    </>
  )
}

export default Category;