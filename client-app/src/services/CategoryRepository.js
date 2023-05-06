import axios from "axios";

export async function getCategoryList(token) {
    try {
        const response = await axios.get(`https://localhost:7027/api/categories/`, {
            headers: {
                Authorization: token
            }
        });
        const data = response.data;
        if (data.isSuccess === true) {
            return data.result;
        }
        else {
            return null;
        }

    } catch (error) {
        console.log('Error', error.messge);
        return null;
    }
}

export async function getCategoryById(token, id = 0) {
    if (id > 0) {
        try {
            const response = await axios.get(`https://localhost:7027/api/categories/${id}`, {
                headers: {
                    Authorization: token
                }
            });
            const data = response.data;
            if (data.isSuccess === true) {
                return data.result;
            }
            else {
                return null;
            }

        } catch (error) {
            console.log('Error', error.messge);
            return null;
        }
    }
    else {
        return null;
    }
}

export async function updateCategory(formData, token, navigate) {
    try {
        fetch(`https://localhost:7027/api/categories/`, {
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
                console.log("add to food successful!");
                return true;
            })
            .catch((error) => {
                console.error("Error occurred during add to food :", error);
                return false;
            });
    } catch (error) {
        return false;
    }
}