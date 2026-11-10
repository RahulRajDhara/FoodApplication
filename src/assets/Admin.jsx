import React, { useState } from 'react';
import './Admin.css'; // We will create this CSS file next

export default function Admin() {
  // --- STATE FOR CATEGORIES ---
  const [categories, setCategories] = useState([
    { id: 1, name: 'Burger', icon: '🍔', status: 'Active' },
    { id: 2, name: 'Pizza', icon: '🍕', status: 'Active' },
  ]);
  const [catForm, setCatForm] = useState({ id: null, name: '', icon: '', status: 'Active' });

  // --- STATE FOR FOOD ITEMS ---
  const [foods, setFoods] = useState([
    { id: 1, name: 'Fresh chicken burger', category: 'Burger', price: 120, image: '', status: 'Active' },
  ]);
  const [foodForm, setFoodForm] = useState({ id: null, name: '', category: '', price: '', image: '', status: 'Active' });
  const [showFoodForm, setShowFoodForm] = useState(false);

  // ==============================
  // CATEGORY HANDLERS
  // ==============================
  const handleCatSubmit = (e) => {
    e.preventDefault();
    if (catForm.id) {
      // Update
      setCategories(categories.map(c => c.id === catForm.id ? catForm : c));
    } else {
      // Insert
      setCategories([...categories, { ...catForm, id: Date.now() }]);
    }
    setCatForm({ id: null, name: '', icon: '', status: 'Active' }); // Reset form
  };

  const editCategory = (cat) => setCatForm(cat);
  
  const deleteCategory = (id) => {
    if (window.confirm("Are you sure you want to delete this category?")) {
      setCategories(categories.filter(c => c.id !== id));
    }
  };

  // ==============================
  // FOOD HANDLERS
  // ==============================
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
    <section className="admin-wrap" id="admin">
      <div className="container">
        <div className="admin-top">
          <div>
            <h2>⚙ Admin Dashboard</h2>
            <p>Manage food categories and menu items for your fast-food center.</p>
          </div>
          <span className="admin-badge">DEMO ADMIN PANEL</span>
        </div>

        <div className="admin-layout">
          {/* SIDEBAR */}
          <aside className="admin-sidebar">
            <h3>🍔 Roadside Bites</h3>            
            <a className="admin-side-link active" href="#categoryManager">▤ Manage Categories</a>
            <a className="admin-side-link" href="#foodManager">🍟 Manage Menu</a>
            <a className="admin-side-link" href="#admin">▦ Dashboard</a>
          </aside>

          <div className="admin-content">
            {/* CATEGORY MANAGER */}
            <div className="admin-card" id="categoryManager">
              <div className="admin-card-head">
                <h3>Manage Food Categories</h3>
                <button className="btn btn-red btn-small" onClick={() => setCatForm({ id: null, name: '', icon: '', status: 'Active' })}>＋ Add Category</button>
              </div>
              <div className="admin-table-wrap">
                <table>
                  <thead><tr><th>#</th><th>Category Name</th><th>Icon</th><th>Status</th><th>Action</th></tr></thead>
                  <tbody>
                    {categories.map((cat, index) => (
                      <tr key={cat.id}>
                        <td>{index + 1}</td>
                        <td>{cat.name}</td>
                        <td>{cat.icon}</td>
                        <td>{cat.status}</td>
                        <td>
                          <button className="action-btn edit" onClick={() => editCategory(cat)}>Edit</button>
                          <button className="action-btn delete" onClick={() => deleteCategory(cat.id)}>Delete</button>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
              
              {/* Category Form */}
              <div className="admin-form-container">
                <h3>{catForm.id ? 'Edit Category' : 'Add Category'}</h3>
                <form onSubmit={handleCatSubmit}>
                  <div className="form-group">
                    <label>Category Name *</label>
                    <input required value={catForm.name} onChange={e => setCatForm({...catForm, name: e.target.value})} placeholder="e.g. Momos" />
                  </div>
                  <div className="form-group">
                    <label>Icon / Emoji</label>
                    <input value={catForm.icon} onChange={e => setCatForm({...catForm, icon: e.target.value})} placeholder="e.g. 🥟" />
                  </div>
                  <div className="form-group">
                    <label>Status</label>
                    <select value={catForm.status} onChange={e => setCatForm({...catForm, status: e.target.value})}>
                      <option value="Active">Active</option>
                      <option value="Inactive">Inactive</option>
                    </select>
                  </div>
                  <div className="form-actions">
                    <button className="btn btn-red" type="submit">💾 Save Category</button>
                  </div>
                </form>
              </div>
            </div>

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

          </div>
        </div>
      </div>
    </section>
  );
}