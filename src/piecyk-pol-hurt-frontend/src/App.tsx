import { BrowserRouter, Routes, Route } from "react-router-dom";
import Navigation from "./Components/Navigation";
import Products from "./Pages/Products";
import Orders from "./Pages/Orders";
import Locations from "./Pages/Locations";

function App() {
  document.title = "Piecyk Pol Hurt";
  return (
    <BrowserRouter>
      <Navigation />
      <main>
        <Routes>
          <Route path="/" element={<Products />} />
          <Route path="/products" element={<Products />} />
          <Route path="/orders" element={<Orders />} />
          <Route path="/locations" element={<Locations />} />
        </Routes>
      </main>
    </BrowserRouter>
  );
}

export default App;
