import React, { useState, useEffect } from 'react';
import './Admin.css';
import { API_BASE_URL } from './Config.js';

export default function Admin() {
  // ---------- CATEGORY STATE ----------
  const [categories, setCategories] = useState([]);
  const [catForm, setCatForm] = useState({ id: null, name: '', icon: '', status: 'Active' });
  const [showCatForm, setShowCatForm] = useState(false);
  const [selectedImageFile, setSelectedImageFile] = useState(null);
  const [imagePreview, setImagePreview] = useState(null);

  // ---------- FOOD STATE ----------
  const [foods, setFoods] = useState([
    { id: 1, name: 'Fresh chicken burger', category: 'Burger', price: 120, image: '', status: 'Active' },
  ]);
  const [foodForm, setFoodForm] = useState({ id: null, name: '', category: '', price: '', image: '', status: 'Active' });
  const [showFoodForm, setShowFoodForm] = useState(false);

  // ---------- LOAD CATEGORIES ----------
  useEffect(() => {
    Getallfoodcategories();
  }, []);

  const Getallfoodcategories = async () => {
    try {
      const response = await fetch(`${API_BASE_URL}Food/Getallfoodcaragories`);
      const result = await response.json();
      console.log(result.data);
      setCategories(result.data || []);
    } catch (error) {
      console.error('Error fetching food categories:', error);
    }
  };

  // ---------- CATEGORY HANDLERS ----------
  const openAddCategoryForm = () => {
    setCatForm({ id: null, name: '', icon: '', status: 'Active' });
    setSelectedImageFile(null);
    setImagePreview(null);
    setShowCatForm(true);
  };

  const editCategory = (cat) => {
    setCatForm({
      id: cat.categoryId,
      name: cat.categoryName,
      icon: cat.imagePath || '',
      status: cat.isActive === 1 || cat.isActive === true ? 'Active' : 'Inactive',
    });
    setImagePreview(cat.imagePath || null);
    setSelectedImageFile(null);
    setShowCatForm(true);
  };

  const deleteCategory = async (id) => {
    if (!window.confirm('Are you sure you want to delete this category?')) return;
    try {
      const response = await fetch(`${API_BASE_URL}Food/DeleteFoodCategory/${id}`, {
        method: 'DELETE',
      });
      if (response.ok) {
        setCategories((prev) => prev.filter((c) => c.categoryId !== id));
      } else {
        const result = await response.json().catch(() => ({}));
        alert(result.message || 'Delete failed');
      }
    } catch (error) {
      console.error(error);
      alert('Something went wrong while deleting');
    }
  };

  const handleImageChange = (e) => {
    const file = e.target.files[0];
    if (file) {
      setSelectedImageFile(file);
      setImagePreview(URL.createObjectURL(file));
    }
  };

  const handleCatSubmit = async (e) => {
    e.preventDefault();
    console.log('catForm.id =', catForm.id);

    try {
      let imagePath = catForm.icon || '';

      // 1. Upload image (if a new file was chosen)
      if (selectedImageFile) {
        const formData = new FormData();
        formData.append('image', selectedImageFile);

        const uploadResponse = await fetch(`${API_BASE_URL}Food/UploadFoodImage`, {
          method: 'POST',
          body: formData,
        });
        const uploadResult = await uploadResponse.json();
        console.log("uploadResponse",uploadResult)
        if (!uploadResponse.ok) {
          alert(uploadResult.message || 'Image upload failed');
          return;
        }
        imagePath = uploadResult.data;
      }

      // 2. Save category
      const categoryData = {
        categoryId: catForm.id || 0,
        categoryName: catForm.name,
        imagePath: imagePath,
        description:"",
        isActive: catForm.status === 'Active' ? 1 : 0,
      };

      const response = await fetch(`${API_BASE_URL}Food/AddEditFoodCategory`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(categoryData),
      });
      const result = await response.json();

      if (response.ok) {
        console.log('Category saved successfully!',result.status);
        setShowCatForm(false);
        setSelectedImageFile(null);
        setImagePreview(null);
        setCatForm({ id: null, name: '', icon: '', status: 'Active' });
        Getallfoodcategories();
      } else {
        alert(result.message || 'Category save failed');
      }
    } catch (error) {
      console.error(error);
      alert('Something went wrong');
    }
  };

  const closeCatForm = () => {
    setShowCatForm(false);
    setSelectedImageFile(null);
    setImagePreview(null);
    setCatForm({ id: null, name: '', icon: '', status: 'Active' });
  };

  // ---------- RENDER ----------
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
                <button className="btn btn-red btn-small" onClick={openAddCategoryForm}>
                  ＋ Add Category
                </button>
              </div>

              <div className="admin-table-wrap">
                <table>
                  <thead>
                    <tr>
                      <th>#</th>
                      <th>Category Name</th>
                      <th>Image</th>
                      <th>Status</th>
                      <th>Action</th>
                    </tr>
                  </thead>
                  <tbody>
                    {categories.length === 0 ? (
                      <tr>
                        <td colSpan="5" style={{ textAlign: 'center', padding: '20px' }}>
                          No categories found
                        </td>
                      </tr>
                    ) : (
                      categories.map((cat, index) => (
                        <tr key={cat.categoryId}>
                          <td>{index + 1}</td>
                          <td>{cat.categoryName}</td>
                          <td>
                            {cat.imagePath ? (
                              <img src={cat.imagePath} alt={cat.categoryName} className="admin-table-img" />
                            ) : (
                              <span>—</span>
                            )}
                          </td> 
                          <td>
                            <span className={`status ${cat.isActive === 1 || cat.isActive === true ? 'Active' : 'inactive'}`}>
                              {cat.isActive === 1 || cat.isActive === true ? 'Active' : 'Inactive'}
                            </span>
                          </td>
                          <td>
                            <button className="action-btn edit" onClick={() => editCategory(cat)}>
                              Edit
                            </button>
                            <button className="action-btn delete" onClick={() => deleteCategory(cat.categoryId)}>
                              Delete
                            </button>
                          </td>
                        </tr>
                      ))
                    )}
                  </tbody>
                </table>
              </div>
            </div>

            {/* CATEGORY FORM */}
            {showCatForm && (
              <div className="admin-form-container">
                <h3>{catForm.id ? 'Edit Category' : 'Add Category'}</h3>
                <form onSubmit={handleCatSubmit}>
                  <div className="form-group">
                    <label>Category Name *</label>
                    <input
                      required
                      value={catForm.name}
                      onChange={(e) => setCatForm({ ...catForm, name: e.target.value })}
                      placeholder="e.g. Momos"
                    />
                  </div>

                  <div className="form-group">
                    <label>Category Image</label>
                    <input
                      type="file"
                      accept="image/*"
                      onChange={handleImageChange}
                      style={{ padding: '8px', border: '1px dashed #ccc', background: '#f9f9f9' }}
                    />

                     <span className="selected-file-name">
                       {selectedImageFile? selectedImageFile.name: catForm.imagePath ? catForm.imagePath.split("/").pop()
                        : "No file chosen"} </span>
                    {imagePreview && (
                      <img
                        src={imagePreview}
                        alt="Preview"
                        style={{
                          width: '50px',
                          height: '50px',
                          marginTop: '10px',
                          borderRadius: '50%',
                          objectFit: 'cover',
                        }}
                      />
                    )}
                  </div>

                  <div className="form-group">
                    <label>Status</label>
                    <select
                      value={catForm.status}
                      onChange={(e) => setCatForm({ ...catForm, status: e.target.value })}
                    >
                      <option value="Active">Active</option>
                      <option value="Inactive">Inactive</option>
                    </select>
                  </div>

                  <div className="form-actions">
                    <button className="btn btn-red" type="submit">
                      💾 Save Category
                    </button>
                    <button className="btn btn-outline" type="button" onClick={closeCatForm}>
                      Cancel
                    </button>
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