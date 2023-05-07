import axios from "axios";

export async function getFeedbackList(token) {
    try {
        const response = await axios.get(`https://localhost:7027/api/Feedbacks/all`, {
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

export async function getFeedbackById(token, id = 0) {
    if (id > 0) {
        try {
            const response = await axios.get(`https://localhost:7027/api/Feedbacks/${id}`, {
                headers: {
                    Authorization: token
                }
            });
            const data = response.data;
            if (data.isSuccess) {
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


export async function updateFeedback(id, formData, token, navigate) {
    try {
        fetch(`https://localhost:7027/api/Feedbacks/${id}/feedback`, {
            method: "PUT",
            headers: {
                Authorization: token,
            },
            body: formData,
        })
            .then((response) => {
                if (!response.ok) {
                    throw new Error("Network response was not ok");
                }
                console.log("add to feedback successful!");
                return true;
            })
            .catch((error) => {
                console.error("Error occurred during add to feedback :", error);
                return false;
            });
    } catch (error) {
        return false;
    }
}