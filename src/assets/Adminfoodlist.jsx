import React from 'react'
import './Admin.css'; // We will create this CSS file next
export default function Adminfoodlist() {
    const handleFoodSubmit = (e) => {
    e.preventDefault();
    const foodData = { ...foodForm, price: Number(foodForm.price) };
    
    if (foodForm.id) {
      // Update
      setFoods(foods.map(f => f.id === foodForm.id ? foodData : f));
    } else {
      // Insert
      setFoods([...foods, { ...foodData, id: Date.now() }]);
    }
    
    // Reset and close
    setFoodForm({ id: null, name: '', category: '', price: '', image: '', status: 'Active' });
    setShowFoodForm(false);
  };

  const editFood = (food) => {
    setFoodForm(food);
    setShowFoodForm(true);
  };

  const deleteFood = (id) => {
    if (window.confirm("Are you sure you want to delete this food item?")) {
      setFoods(foods.filter(f => f.id !== id));
    }
  };
  return (
    <>
    {/* FOOD MANAGER */}
            
            <div className="admin-card" id="foodManager" style={{ gridColumn: '1 / -1' }}>
              <div className="admin-card-head">
                <h3>Manage Menu Items</h3>
                <button className="btn btn-red btn-small" onClick={() => { setFoodForm({ id: null, name: '', category: '', price: '', image: '', status: 'Active' }); setShowFoodForm(true); }}>＋ Add Food Item</button>
              </div>
              <div className="admin-table-wrap">
                <table>
                  <thead><tr><th>Item</th><th>Category</th><th>Price</th><th>Status</th><th>Action</th></tr></thead>
                  <tbody>
                    {foods.map((food) => (
                      <tr key={food.id}>
                        <td>{food.name}</td>
                        <td>{food.category}</td>
                        <td>₹{food.price}</td>
                        <td>{food.status}</td>
                        <td>
                          <button className="action-btn edit" onClick={() => editFood(food)}>Edit</button>
                          <button className="action-btn delete" onClick={() => deleteFood(food.id)}>Delete</button>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>

              {/* Food Form */}
              {showFoodForm && (
                <div className="admin-form-container" style={{ borderTop: '1px solid var(--line)', marginTop: '18px', paddingTop: '18px' }}>
                  <h3>{foodForm.id ? 'Edit Food Item' : 'Add Food Item'}</h3>
                  <form onSubmit={handleFoodSubmit}>
                    <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(160px, 1fr))', gap: '12px' }}>
                      <div className="form-group">
                        <label>Food Name *</label>
                        <input required value={foodForm.name} onChange={e => setFoodForm({...foodForm, name: e.target.value})} placeholder="e.g. Paneer Burger" />
                      </div>
                      <div className="form-group">
                        <label>Category *</label>
                        <select required value={foodForm.category} onChange={e => setFoodForm({...foodForm, category: e.target.value})}>
                          <option value="">Select Category</option>
                          {categories.map(c => <option key={c.id} value={c.name}>{c.name}</option>)}
                        </select>
                      </div>
                      <div className="form-group">
                        <label>Price (₹) *</label>
                        <input type="number" required value={foodForm.price} onChange={e => setFoodForm({...foodForm, price: e.target.value})} placeholder="e.g. 80" />
                      </div>
                      <div className="form-group">
                        <label>Image URL (optional)</label>
                        <input type="url" value={foodForm.image} onChange={e => setFoodForm({...foodForm, image: e.target.value})} placeholder="https://..." />
                      </div>
                      <div className="form-group">
                        <label>Status</label>
                        <select value={foodForm.status} onChange={e => setFoodForm({...foodForm, status: e.target.value})}>
                          <option value="Active">Active</option>
                          <option value="Inactive">Inactive</option>
                        </select>
                      </div>
                    </div>
                    <div className="form-actions">
                      <button className="btn btn-red" type="submit">💾 Save Food Item</button>
                      <button className="btn btn-outline" type="button" onClick={() => setShowFoodForm(false)}>Cancel</button>
                    </div>
                  </form>
                </div>
              )}
            </div>
    </>
  )
}
