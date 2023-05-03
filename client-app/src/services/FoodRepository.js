import axios from "axios";
import { get_api, post_api } from "./Methods";

export async function getFoods(pageSize = 10, pageNumber = 1) {
    try {
        const response = await axios.get(`https://localhost:7027/api/Foods/?PageSize=${pageSize}&PageNumber=${pageNumber}`);
        const data = response.data;
        if (data.isSuccess)
            return data.result;
        else
            return null;
    } catch (error) {
        console.log('Error', error.messge);
        return null;
    }
}

export async function getFoodById(id = 0) {
    if (id > 0)
        return get_api(`https://localhost:7027/api/Foods/${id}`);
    return null;
}

export function getCategories(formData) {
    return get_api("https://localhost:7027/api/categories/", formData);
}

export async function addOrUpdateFood(formData,token, navigate) {
    try {
      const formData = new FormData();
     
      fetch("https://localhost:7027/api/Foods/", {
        method: "POST",
        headers: {
          Authorization: token,
        },
        body: formData,
      })
        .then((response) => {
          if (!response.ok) {
            throw new Error("Network response was not ok");
          }
          console.log("add to food  successful!");
        })
        .catch((error) => {
          console.error("Error occurred during add to food :", error);
        });
  
      
    } catch (error) {}
  }