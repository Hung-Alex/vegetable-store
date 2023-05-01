import axios from "axios";

export async function AddToCart(token, food, navigate) {
  try {
    const formData = new FormData();
    formData.append("id", String(food.id));
    formData.append("quantity", String(food.quantity));
    console.log(formData.keys.length);
    fetch("https://localhost:7027/api/Cart/", {
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
        console.log("add to cart  successful!");
      })
      .catch((error) => {
        console.error("Error occurred during add to cart :", error);
      });

    navigate("/product");
  } catch (error) {}
}
