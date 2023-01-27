import { BrowserRouter, Routes, Route } from "react-router-dom";
import Navigation from "./Components/Navigation";
import Products from "./Pages/Products";
import Orders from "./Pages/Orders";
import Locations from "./Pages/Locations";
import { useAuth0 } from "@auth0/auth0-react";
import axios from "axios";
import { useState, useEffect } from "react";

const App = () => {
  document.title = "Piecyk Pol Hurt";

  const { isLoading, getAccessTokenSilently, isAuthenticated } = useAuth0();
  const [isAcccessTokenSet, setIsAcccessTokenSet] = useState<boolean>(false);

  const getAccessToken = async () => {
    const accessToken = await getAccessTokenSilently();

    axios.interceptors.request.use((config) => {
      if (config && config.headers) {
        config.headers["Authorization"] = `Bearer ${accessToken}`;
      }
      return config;
    });

    setIsAcccessTokenSet(true);
  };

  useEffect(() => {
    if (isAuthenticated) {
      getAccessToken();
    }
  }, [isAuthenticated]);

  if (isLoading || (!isLoading && isAuthenticated && !isAcccessTokenSet)) {
    return <></>;
  }

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
