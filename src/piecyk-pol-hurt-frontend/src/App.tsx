/* eslint-disable react-hooks/exhaustive-deps */
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Navigation from "./Components/Navigation";
import Shop from "./Pages/Shop";
import Locations from "./Pages/Locations";
import { useAuth0 } from "@auth0/auth0-react";
import axios from "axios";
import { useState, useEffect } from "react";
import Products from "./Pages/Products";
import ShoppingCart from "./Pages/ShoppingCart";
import { useDispatch, useSelector } from "react-redux";
import { updateEmail } from "./Redux/Reducers/ShoppingCartReducer";
import LocationChoosing from "./Pages/LocationChoosing";
import { RootState } from "./Redux/store";
import Orders from "./Pages/Orders";
import { getPermissions } from "./API/Endpoints/User";

const App = () => {
  document.title = "Piecyk Pol Hurt";

  const { isLoading, getAccessTokenSilently, isAuthenticated, user } =
    useAuth0();
  const [isAcccessTokenSet, setIsAcccessTokenSet] = useState<boolean>(false);
  const dispatch = useDispatch();
  const chosenSendPoint = useSelector((state: RootState) => state.shoppingCarts.chosenSendPoint);

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

  useEffect(() => {
    if (user) {
      dispatch(updateEmail(user.email));

      getPermissions().then((data) => {
        dispatch(updatePermission)
      })
    }
  }, [user?.email]);

  if (isLoading || (!isLoading && isAuthenticated && !isAcccessTokenSet)) {
    return <></>;
  }

  return (
    <BrowserRouter>
      <Navigation />
      <main>
        <Routes>
          <Route path="/" element={<Navigate to={chosenSendPoint === undefined ? "/lokalizacja" : "/sklep"} />}/>
          <Route path="/lokalizacja" element={<LocationChoosing />} />
          <Route path="/sklep" element={<Shop />} />
          <Route path="/produkty" element={<Products />} />
          <Route path="/lokalizacje" element={<Locations />} />
          <Route path="/koszyk" element={<ShoppingCart />} />
          <Route path="/zamowienia" element={<Orders />} />
        </Routes>
      </main>
    </BrowserRouter>
  );
};

export default App;
