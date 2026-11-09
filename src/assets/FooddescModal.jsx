import React from 'react';

export default function FooddescModal({ selectedFood, cart, onClose, onAddToCart, onChangeQty }) {
  // If no food is selected, don't render anything
  if (!selectedFood) return null;

  // Check if this item is already in the cart
  const cartEntry = cart[selectedFood.foodID];
  const qty = cartEntry ? cartEntry.qty : 0;

  return (
    <div className="modal-overlay" onClick={onClose}>
      {/* Stop propagation so clicking inside doesn't close the modal */}
      <div className="modal-content" onClick={(e) => e.stopPropagation()}>
        
        {/* LEFT SIDE: Image */}
        <div className="modal-image-container">
          <img src={selectedFood.imageURL} alt={selectedFood.foodName} className="modal-image" />
        </div>

        {/* RIGHT SIDE: Details */}
        <div className="modal-details">
          <div className="modal-header">
            <span className="modal-category">{selectedFood.categoryName}</span>
            <h2 className="modal-title">{selectedFood.foodName}</h2>
            <div className="modal-price">₹{selectedFood.price}</div>
          </div>

          <div className="modal-body">
            <h4>Description</h4>
            <p>{selectedFood.description || "No description available for this item."}</p>
          </div>

          <div className="modal-footer">
            {/* If item is in cart, show + and - buttons */}
            {cartEntry ? (
              <div className="modal-qty-controls" style={{ display: 'flex', alignItems: 'center', gap: '10px' }}>
                <button className="qty-btn" onClick={() => onChangeQty(selectedFood.foodID, -1)}>−</button>
                <span className="qty">{qty}</span>
                <button className="qty-btn" onClick={() => onChangeQty(selectedFood.foodID, 1)}>+</button>
                <span className="modal-line-total" style={{ marginLeft: '10px', fontWeight: 'bold' }}>
                  ₹{selectedFood.price * qty}
                </span>
              </div>
            ) : (
              /* If not in cart, show Add button */
              <button className="modal-add-btn" onClick={() => onAddToCart(selectedFood)}>
                ＋ Add to Order (₹{selectedFood.price})
              </button>
            )}
            
            <button className="modal-close-btn" onClick={onClose} style={{ marginLeft: '20px' }}>
              Close
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}