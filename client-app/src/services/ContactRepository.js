import axios from "axios";

export async function AddToContact(formData, token, navigate) {
    try {
        fetch("https://localhost:7027/api/Feedbacks/", {
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
                console.log("add to contact successful!");
                return true;
            })
            .catch((error) => {
                console.error("Error occurred during add to contact:", error);
                return false;
            });
    } catch (error) {
        return false;
    }
}