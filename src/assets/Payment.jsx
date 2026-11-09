import React, { useState,useEffect } from 'react';
import './Food.css';
export default function Payment() {

     const [paymentMethod, setPaymentMethod] = useState("UPI");
       const [orderPlaced, setOrderPlaced] = useState(false);
       const [tokenNo, setTokenNo] = useState("");

       
         // Scroll to payment section
     
       
         // Place demo order
         const placeOrder = () => {
           const token = "RB-" + Math.floor(1000 + Math.random() * 9000);
           setTokenNo(token);
           setOrderPlaced(true);
         };

  return (
    <div>
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
    </div>
  )
}
