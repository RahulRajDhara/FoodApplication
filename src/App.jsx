import React, { useState } from 'react';
import Header from './assets/Header'
import Food from './assets/Food'
import Footer from './assets/Footer'
import Payment from './assets/Payment'
import Admin from './assets/Admin'
function App() {
 const [cartCount, setCartCount] = useState(0);

  return (
    <>
      <Header cartCount={cartCount} />
      <Food setAppCartCount={setCartCount}/>
      <Payment/>
      <Footer/> 
      <Admin/>
    </>
  )
}

export default App
