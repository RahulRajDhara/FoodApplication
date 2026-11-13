import React, { useState,useEffect } from 'react';
import './Admin.css'; // We will create this CSS file next
import { API_BASE_URL } from "./Config.js"

export default function Admin() {
   
  // 1. Store the ACTUAL FILE object here, not a base64 string
  const [selectedImageFile, setSelectedImageFile] = useState(null);
    const [imagePreview, setImagePreview] = useState(null);
    const [categories, setCategories] = useState([]);
  // --- STATE FOR CATEGORIES ---


  // --- STATE FOR FOOD ITEMS ---
  const [foods, setFoods] = useState([
    { id: 1, name: 'Fresh chicken burger', category: 'Burger', price: 120, image: '', status: 'Active' },
  ]);
  const [foodForm, setFoodForm] = useState({ id: null, name: '', category: '', price: '', image: '', status: 'Active' });
  const [showFoodForm, setShowFoodForm] = useState(false);
 const [showCatForm, setShowCatForm] = useState(false);
  // ==============================
  // CATEGORY HANDLERS
  // ==============================
  useEffect(() => {
    Getallfoodcategories();    
}, []);
const Getallfoodcategories = async () => {
    try {     
        const response = await fetch(
            `${API_BASE_URL}Food/Getallfoodcaragories`
        ); 
       
        const result = await response.json();   
         console.log(result.data)
            setCategories(result.data || []);
       
    }
    catch (error) {
        console.error("Error fetching food categories:", error);
    }
};
  const editCategory = (cat) => setCatForm(cat);
  
  const deleteCategory = (id) => {
    if (window.confirm("Are you sure you want to delete this category?")) {
      setCategories(categories.filter(c => c.id !== id));
    }
  };
  // 1. NEW FUNCTION: Handle File Upload
  const handleImageUpload = (e) => {
    const file = e.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onloadend = () => {
        // reader.result contains the Base64 string of the image
        setCatForm({ ...catForm, icon: reader.result });
      };
      reader.readAsDataURL(file);
    }
  };

    const handleImageChange = (e) => {
    const file = e.target.files[0];
    if (file) {
      setSelectedImageFile(file); // Save the actual file for the API
      setImagePreview(URL.createObjectURL(file)); // Create a temporary URL just to show the user
    }
  };
  // ... YOUR EXISTING FORM LOGIC ...
  const [catForm, setCatForm] = useState({ id: null, name: '', icon: '', status: 'Active' });
  // 3. OPEN FORM FUNCTION
  const openAddCategoryForm = () => {
    setCatForm({ categoryId: null, categoryName: '', status: 'Active' });
    setSelectedImageFile(null);
    setImagePreview(null);
    setShowCatForm(true);
  };
  const handleCatSubmit = (e) => {
    e.preventDefault();
    if (catForm.id) {
      // Update existing
      setCategories(categories.map(c => c.id === catForm.id ? catForm : c));
    } else {
      // Insert new
      setCategories([...categories, { ...catForm, id: Date.now() }]);
    }
    // Reset form
    setCatForm({ id: null, name: '', icon: '', status: 'Active' });
  };
  // ==============================
  // FOOD HANDLERS
  // ==============================
  

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
                <button className="btn btn-red btn-small" onClick={openAddCategoryForm}>＋ Add Category</button>
              </div>
              <div className="admin-table-wrap">
                <table>
                  <thead><tr><th>#</th><th>Category Name</th><th>Image</th><th>Status</th><th>Action</th></tr></thead>
                  <tbody>
                    {categories.map((cat, index) => (
                      <tr key={cat.categoryId}>
                        <td>{index + 1}</td>
                        <td>{cat.categoryName}</td>
                       <td> <img src={cat.imagePath} alt="" className="admin-table-img" /></td>
                      
                        <td>
                        <span className={`status ${cat.isActive === 1 || cat.isActive === true ? 'Active' : 'inactive'}`}>
                          {cat.isActive === 1 || cat.isActive === true ? 'Active' : 'Inactive'}
                        </span>
                      </td>
                        <td>
                          <button className="action-btn edit" onClick={() => editCategory(cat.categoryId)}>Edit</button>
                          <button className="action-btn delete" onClick={() => deleteCategory(cat.categoryId)}>Delete</button>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div> 
            </div>
                 {/* Category Form */}
               {showCatForm && (
              <div className="admin-form-container">
                <h3>{catForm.id ? 'Edit Category' : 'Add Category'}</h3>
                <form onSubmit={handleCatSubmit}>
                  <div className="form-group">
                    <label>Category Name *</label>
                    <input required value={catForm.name} onChange={e => setCatForm({...catForm, name: e.target.value})} placeholder="e.g. Momos" />
                  </div>
                  <div className="form-group">
                    <label>Category Image</label>
                     {/* File Upload Input */}
                    <input 
                      type="file" 
                      accept="image/*" 
                      onChange={handleImageChange} 
                      style={{ padding: '8px', border: '1px dashed #ccc', background: '#f9f9f9' }}
                    />
                    {/* Show Preview */}
                    {imagePreview && (
                      <img src={imagePreview} alt="Preview" style={{ width: '50px', height: '50px', marginTop: '10px', borderRadius: '50%', objectFit: 'cover' }} />
                    )}
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
                    <button class="btn btn-outline" onClick={() => setShowCatForm(false)} type="button" >Cancel</button>
                  </div>
                </form>
              </div>
              )} 
            

          </div>
        </div>
      </div>
    </section>
  );
}