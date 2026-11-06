import React from 'react'

import  './Header.css';
export default function Header() {
  return (
    <div className="topbar">
      <div className="container nav">
    <a className="brand" href="#home" aria-label="Roadside Bites home"><span className="brand-icon">🍔</span><span>Roadside <em>Bites</em><small>FRESH • TASTY • FAST</small></span></a>
    <button className="menu-toggle" id="menuToggle" aria-label="Toggle navigation">☰</button>
    <nav className="navlinks" id="navLinks">
      <a className="active" href="#home">⌂ Home</a><a href="#menu">♨ Menu</a><a href="#how">ⓘ How It Works</a><a href="#contact">☎ Contact</a>
    </nav>
    <button className="cart-nav" onClick="document.getElementById('order').scrollIntoView({behavior:'smooth'})">🛒 My Order (<span id="cartCount">0</span>)</button>
  </div>
  </div> 
  )
}
