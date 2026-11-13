
import React, { useState,useEffect } from 'react';
import './Food.css';
import FooddescModal from "./FooddescModal.jsx";
import { API_BASE_URL } from "./Config.js"
export default function Food({ setAppCartCount }) {

// const APikey='https://localhost:44326/api/';

  const [categories, setCategories] = useState([]);
const [activeCategoryId, setActiveCategoryId] = useState(0);
   const [foodItems, setFoodItems] = useState([]);
   const [cart, setCart] = useState({});
   const [selectedFood, setSelectedFood] = useState(null); // Tracks the item for the modal
    const goToPayment = () => {
           document.getElementById("payment").scrollIntoView({
             behavior: "smooth"
           });
         };
  useEffect(() => {
    Getallfoodcategories();
     GetFoodItems(0);
}, []);
 
/**---------------------- food catagory-------------------- */
const Getallfoodcategories = async () => {
    try {     
        const response = await fetch(
            `${API_BASE_URL}Food/Getallfoodcaragories`
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
const GetFoodItems= async(categoryId)=>{
try{
 
    const response = await fetch(
      `${API_BASE_URL}Food/Getfooditem?Categoyid=${categoryId}`,
      {
       method:"GET",
        headers:{
          "Content-Type":"application/json"
        }       
    }
    );
    const result = await response.json();   
   
    setFoodItems(result.data || []);
  }
  catch(error){
       console.error("Error fetching food items:", error);
  }
};
/*-------------------------------------------------------------- */
const handleCategoryClick = (id) => {
        setActiveCategoryId(id); // Updates the red button
        GetFoodItems(id);        // Fetches new items from DB
    };
    /*---------------------Add to cart---------------------- */
    const cartItems = Object.values(cart);
const cartCount = cartItems.reduce((sum, item) => sum + item.qty, 0);
const cartTotal = cartItems.reduce((sum, item) => sum + (Number(item.price) * item.qty), 0);
 useEffect(() => {
    // This sends the count up to App.jsx every time it changes
    setAppCartCount(cartCount);
  }, [cartCount, setAppCartCount]);
    function addToCart(item){
      
      setCart((prevCart)=>{
         const existingItem =prevCart[item.foodID];
         if(existingItem){
          return {
          ...prevCart,
          [item.foodID]: { ...existingItem, qty: existingItem.qty + 1 }
          };
         }
         else{
          return {
                ...prevCart,
               [item.foodID]: { ...item, qty: 1 }
                };
         }
      })
    }

    function changeqty(id,delta){ 
     
      setCart((prevCart)=>{
        const item=prevCart[id];
        if(!item)
          return prevCart;

        const newQty=item.qty+delta;
        if(newQty<=0){
          const newCart={ ...prevCart};
          delete newCart[id];
          return newCart;
        }
        return{
          ...prevCart,
          [id]:{...item,qty:newQty}
        }

      })
    }
    function removeFromCart(id) {
  setCart((prevCart) => {
    const newCart = { ...prevCart };
    delete newCart[id];
    return newCart;
  });
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

             <div className="food-grid" id="foodGrid">
             {
          foodItems.length > 0 ? (  // <--- 1. ADDED THE '?' HERE
            foodItems.map((item) => (
          <div className="food-card" key={item.foodID}>
            {/* 1. The Food Image */}
            <img src={item.imageURL} alt={item.foodName} className="food-image" />
            
            {/* 2. Text Details */}
            <div className="food-info">
                <h4 className="">{item.foodName}</h4>
                {/* <p className="food-desc">{item.description}</p> */}
                    <span className="details-btn" onClick={() => 
                    {
                       console.log("Details clicked:", item);
                      setSelectedFood(item)
                      }}>Details</span>
                {/* 3. Price and Add to Cart */}
                <div className="food-meta">
                    <span className="price">₹{item.price}</span>
                     <span className="food-tag">{item.categoryName}</span>
                  </div>
                    <button  className="add-btn" onClick={() => addToCart(item)}>
                    ＋ Add to Order
                    </button>
                 
                
            </div>
          </div>
      ))
       ) : (  // <--- This colon is correct
          /* This shows if the category has no items, or while loading */
              <div className="no-items">
              <p>No items found in this category. 🍽️</p>
            </div>
           ) 
        }
</div>
{/*------------------------- MODAL OVERLAY ---------------------*/}
{/* MODAL OVERLAY */}
    <FooddescModal
  selectedFood={selectedFood}
  cart={cart}
  onClose={() => setSelectedFood(null)}
  onAddToCart={addToCart}
  onChangeQty={changeqty}
/>
{/*----------------------- end -----------------------------------------*/}
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

              <div className="cart-title">🛒 Your Order({cartCount})</div>

              <div className="cart-body">
              {/* RENDER CART ITEMS HERE */}
                <div id="cartItems">
                {cartItems.length === 0 ? (
                  <div className="cart-empty">
                    Your cart is empty.<br />
                    Choose something delicious!
                  </div>
                  ) : (
            cartItems.map((x) => (
              
          <div className="cart-item" key={x.foodID}>
            <div>
              <div className="cart-item-name">{x.foodName}</div>
              <div className="cart-item-sub">₹{x.price} each</div>  
              <div className="cart-controls">
                <button className="qty-btn" onClick={() => changeqty(x.foodID, -1)}>−</button>
                <span className="qty">{x.qty}</span>
                <button className="qty-btn" onClick={() => changeqty(x.foodID, 1)}>+</button>
                <button className="remove-btn" onClick={() => removeFromCart(x.foodID)}>🗑</button>
              </div>
            </div>
            <div className="cart-line-total">₹{x.price * x.qty}</div>
          </div>
        ))
      )}
    </div>
                <div className="cart-total">
                  <span>Total Amount</span>
                  <strong id="cartTotal">₹{cartTotal}</strong>
                </div>

                <button
                  className="btn btn-red"
                  id="proceedBtn"
                   onClick={goToPayment}
                 disabled={cartItems.length === 0}

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

       

      </main>

    </div>
  );
}
