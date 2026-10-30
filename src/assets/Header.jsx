import React from 'react'

import  './Header.css';
export default function Header() {
  return (
    <div className="topbar">
      <div class="container nav">
    <a class="brand" href="#home" aria-label="Roadside Bites home"><span class="brand-icon">🍔</span><span>Roadside <em>Bites</em><small>FRESH • TASTY • FAST</small></span></a>
    <button class="menu-toggle" id="menuToggle" aria-label="Toggle navigation">☰</button>
    <nav class="navlinks" id="navLinks">
      <a class="active" href="#home">⌂ Home</a><a href="#menu">♨ Menu</a><a href="#how">ⓘ How It Works</a><a href="#contact">☎ Contact</a>
    </nav>
    <button class="cart-nav" onclick="document.getElementById('order').scrollIntoView({behavior:'smooth'})">🛒 My Order (<span id="cartCount">0</span>)</button>
  </div>
  </div>
  )
}
