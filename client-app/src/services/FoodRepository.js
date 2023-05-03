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

export function addOrUpdateFood(formData) {
    return post_api("https://localhost:7027/api/Foods/", formData);
}

export function getCategories(formData) {
    return get_api("https://localhost:7027/api/categories/", formData);
}