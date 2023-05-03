import React, { useEffect, useState } from "react";
import { GetCategory } from "../Service/Category";
import { Link } from "react-router-dom";

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
      {categories.length > 0 && categories.map((item, index) =>
        <div className="p-3" key={index} style={{ padding: "16px 12px", borderLeft: "0.5px solid black", fontSize: "20px" }}>
          <ul>
            <li>
              <Link to="/product">{item.name}</Link>
            </li>
          </ul>
        </div>
      )}

    </>
  )
}

export default Category;