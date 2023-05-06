import axios from "axios";

export async function getUsers(token, pageSize = 10, pageNumber = 1) {
    try {
        const response = await axios.get(`https://localhost:7027/api/Users/?PageSize=${pageSize}&PageNumber=${pageNumber}`, {
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

export async function getUserById(token, id = 0) {
    if (id > 0) {
        try {
            const response = await axios.get(`https://localhost:7027/api/Users/${id}`, {
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

export async function updateUser(id, formData, token, navigate) {
    try {
        fetch(`https://localhost:7027/api/Users/${id}`, {
            method: "PUT",
            headers: {
                Authorization: token,
            },
            body: formData,
        })
            .then((response) => {
                if (!response.ok) {
                    throw new Error("Network response was not ok");
                    const data = response.json();
                    if (data.isSuccess) {
                        return true;
                    } else {
                        return false;
                    }
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