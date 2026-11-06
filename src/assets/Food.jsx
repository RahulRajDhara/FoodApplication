
import React, { useState,useEffect } from 'react';
import './Food.css';
export default function Food() {

  const [paymentMethod, setPaymentMethod] = useState("UPI");
  const [orderPlaced, setOrderPlaced] = useState(false);
  const [tokenNo, setTokenNo] = useState("");
  const [categories, setCategories] = useState([]);
const [activeCategoryId, setActiveCategoryId] = useState(0);
   const [foodItems, setFoodItems] = useState([]);

  // Scroll to payment section
  const goToPayment = () => {
    document.getElementById("payment").scrollIntoView({
      behavior: "smooth"
    });
  };

  // Place demo order
  const placeOrder = () => {
    const token = "RB-" + Math.floor(1000 + Math.random() * 9000);
    setTokenNo(token);
    setOrderPlaced(true);
  };
  useEffect(() => {
    Getallfoodcategories();
     GetFoodItems(0);
}, []);
/**---------------------- food catagory-------------------- */
const Getallfoodcategories = async () => {
    try {     
        const response = await fetch(
            "https://localhost:44326/api/Food/Getallfoodcaragories"
        ); 
        const result = await response.json();   
            setCategories(result.data || []);
       
    }
    catch (error) {
        console.error("Error fetching food categories:", error);
    }
};
/**--------------------------------------- */
/*------------------------get food item--------------- */
const GetFoodItems= async(cartagoryId)=>{
try{
    const response = await fetch(`https://localhost:44326/api/Food/Getfooditem/${categoryId}`);
    const result = await response.json();   
    console.log("fooditems-",result.data)
    setFoodItems(result.data || []);
  }
  catch(error){
      console.error("Error fetching food items:", error);
  }
}
  return (
    <div>

      <section className="hero" id="home">
        <div className="container hero-content">
          <h1>Good Food <span>Good Mood</span></h1>

          <p>
            Delicious fast food, fresh ingredients, and quick service —
            right at your roadside stop!
          </p>

          <a className="btn btn-yellow" href="#menu">
            🍟 Order Now &nbsp; →
          </a>
        </div>
      </section>

      <div className="container features">

        <div className="feature">
          <div className="emoji">🍅</div>
          <strong>Fresh & Tasty</strong>
          <small>Quality ingredients</small>
        </div>

        <div className="feature">
          <div className="emoji">⏱️</div>
          <strong>Fast Service</strong>
          <small>No long wait</small>
        </div>

        <div className="feature">
          <div className="emoji">🛡️</div>
          <strong>Hygienic Food</strong>
          <small>Made fresh for you</small>
        </div>

        <div className="feature">
          <div className="emoji">📍</div>
          <strong>Roadside Location</strong>
          <small>Easy to find</small>
        </div>

      </div>

      <main>

        {/* Food Menu */}
        <section className="section container" id="menu">

          <h2 className="section-heading">🍴 Our Menu</h2>
          <div className="order-layout">

            <div>
              {/* Food Categories */}
              <div  className="category-pills" id="categoryPills" aria-label="Food categories">
                {/* All Category */}
                 <button type="button" className={`category-pill ${activeCategoryId === 0 ? "active" : ""}`} onClick={() => handleCategoryClick(0)}>
                    🍽️ All
                </button>
                  {/* Dynamic Categories */}
                   {categories.map((cat) => (
                    <button type="button" key={cat.categoryId} 
                        className={`category-pill ${activeCategoryId  === cat.categoryId  ? "active" : ""}`}
                        onClick={() => handleCategoryClick(cat.categoryId)}>
                        {cat.imagePath ? (<img src={cat.imagePath}  alt=""  className="category-icon"/>) : ("🍴")}{cat.categoryName}
                    </button>
                ))}
              </div>

              <div className="food-grid" id="foodGrid"></div>

              <div className="promo">
                <h2>
                  Tasty Food<br />
                  <span>Always a Good Idea</span>
                </h2>

                <p>Your favorite food, just a few clicks away!</p>

                <a className="btn btn-yellow" href="#menu">
                  View Full Menu →
                </a>
              </div>
            </div>

            {/* Cart */}
            <aside className="cart-panel" id="order">

              <div className="cart-title">🛒 Your Order</div>

              <div className="cart-body">

                <div id="cartItems">
                  <div className="cart-empty">
                    Your cart is empty.<br />
                    Choose something delicious!
                  </div>
                </div>

                <div className="cart-total">
                  <span>Total Amount</span>
                  <strong id="cartTotal">₹0</strong>
                </div>

                <button
                  className="btn btn-red"
                  id="proceedBtn"
                  onClick={goToPayment}
                >
                  ▣ Proceed to Payment →
                </button>

                <div className="cart-note">
                  ⓘ You can review your order before making the payment.
                </div>

              </div>
            </aside>

          </div>
        </section>

        {/* How It Works */}
        <section
          className="section"
          id="how"
          style={{ background: "#fafbfc" }}
        >

          <div className="container">

            <h2 className="section-heading">How It Works</h2>

            <div className="how-grid">

              <div className="how-card">
                <div className="emoji">🍔</div>
                <strong>1. Browse Menu</strong>
                <small>Choose your favorite food</small>
              </div>

              <div className="how-card">
                <div className="emoji">🛍️</div>
                <strong>2. Add to Cart</strong>
                <small>Select items and quantity</small>
              </div>

              <div className="how-card">
                <div className="emoji">💳</div>
                <strong>3. Make Payment</strong>
                <small>Select a payment method</small>
              </div>

              <div className="how-card">
                <div className="emoji">🎟️</div>
                <strong>4. Get Your Token</strong>
                <small>Show it to the seller</small>
              </div>

            </div>
          </div>
        </section>

        {/* Payment */}
        <section className="payment-section section" id="payment">

          <div className="container">

            <h2 className="section-heading">💳 Payment Method</h2>

            <div className="payment-options">

              <label className="payment-option">
                <input
                  type="radio"
                  name="paymentMethod"
                  value="UPI"
                  checked={paymentMethod === "UPI"}
                  onChange={(e) => setPaymentMethod(e.target.value)}
                />

                <span className="pay-icon">📲</span>

                <span>
                  <strong>UPI</strong>
                  <small>Google Pay, PhonePe, Paytm</small>
                </span>
              </label>

              <label className="payment-option">
                <input
                  type="radio"
                  name="paymentMethod"
                  value="Card"
                  checked={paymentMethod === "Card"}
                  onChange={(e) => setPaymentMethod(e.target.value)}
                />

                <span className="pay-icon">💳</span>

                <span>
                  <strong>Credit / Debit Card</strong>
                  <small>Visa, Mastercard, RuPay</small>
                </span>
              </label>

              <label className="payment-option">
                <input
                  type="radio"
                  name="paymentMethod"
                  value="Cash"
                  checked={paymentMethod === "Cash"}
                  onChange={(e) => setPaymentMethod(e.target.value)}
                />

                <span className="pay-icon">💵</span>

                <span>
                  <strong>Cash at Counter</strong>
                  <small>Pay directly to the seller</small>
                </span>
              </label>

            </div>

            <div className="pay-bottom">

              <button
                className="btn btn-red"
                id="payBtn"
                onClick={placeOrder}
              >
                🔒 Confirm Demo Order · ₹0
              </button>

            </div>

            {/* Order Token */}
            {orderPlaced && (

              <div className="token-result">

                <div className="token-left">

                  <div className="check-circle">✓</div>

                  <div>
                    <h3>Your Order Has Been Placed!</h3>

                    <div style={{ margin: "5px 0 8px" }}>
                      Your Token No. is
                      <span className="token-no"> {tokenNo}</span>
                    </div>

                    <small>
                      Please show this token to the seller to collect your food.
                    </small>
                  </div>

                </div>

                <div className="token-items">

                  <h4>Order Details</h4>

                  <div className="token-row">
                    <strong>Total</strong>
                    <strong>₹0</strong>
                  </div>

                  <div className="token-row">
                    <span>Payment</span>
                    <span>{paymentMethod}</span>
                  </div>

                  <button
                    className="btn btn-outline"
                    style={{ width: "100%", marginTop: "10px" }}
                    onClick={() => window.print()}
                  >
                    🖨 Print Token
                  </button>

                </div>

              </div>

            )}

          </div>
        </section>

      </main>

    </div>
  );
}
