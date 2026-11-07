import React from 'react';
import './Food.css';

export default function Footer() {
  return (
    <footer className="footer" id="contact">
      <div className="container">
        <div className="footer-main">
          <div>
            <a className="brand" href="#home">
              <span className="brand-icon">🍔</span>
              <span>Roadside <em>Bites</em><small>FRESH • TASTY • FAST</small></span>
            </a>
          </div>
          
          <div>
            <h4>Quick Links</h4>
            <div className="footer-links">
              <a href="#home">Home</a>
              <a href="#menu">Menu</a>
              <a href="#how">How It Works</a>
              <a href="#contact">Contact</a>
            </div>
          </div>
          
          <div>
            <h4>Follow Us</h4>
            {/* Changed to compact circular buttons */}
            <div className="footer-socials">
              <a href="#" aria-label="Facebook">ⓕ</a>
              <a href="#" aria-label="Instagram">◎</a>
              <a href="#" aria-label="YouTube">▶</a>
            </div>
            <p>Fresh food. Happy mood.</p>
          </div>
          
          <div>
            <h4>Contact Us</h4>
            <p>📍 Kona, Howrah</p>
            <p>☎ +91 79807 71109</p>
            <p>Open daily · 10 AM – 10 PM</p>
          </div>
        </div>
        
        <div className="footer-bottom">
          © 2026 Roadside Bites. All rights reserved. · Created By Rahul
        </div>
      </div>
    </footer>
  );
}