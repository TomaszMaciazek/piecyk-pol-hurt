import React from "react";
import ReactDOM from "react-dom/client";
import "./index.scss";
import App from "./App";
import { Auth0Provider } from "@auth0/auth0-react";
import { Auth0 } from "./API/Auth0/Auth0";
import "leaflet/dist/leaflet.css";
import "./MUI/Mui.scss";
import locale from "date-fns/locale/pl";
import { ThemeProvider } from "@emotion/react";
import { DateFnsProvider } from "./MUI/DateFnsProvider";
import { theme } from "./MUI/Theme";
import "@fontsource/roboto/300.css";
import "@fontsource/roboto/400.css";
import "@fontsource/roboto/500.css";
import "@fontsource/roboto/700.css";
import { persistor, store } from "./Redux/store";
import { Provider } from 'react-redux';
import { PersistGate } from 'redux-persist/integration/react';

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <Auth0Provider
    domain={Auth0.domain}
    clientId={Auth0.clientID}
    audience={Auth0.audience}
    scope={Auth0.scope}
    redirectUri={window.location.origin}
    cacheLocation="localstorage"
  >
    <Provider store={store}>
      <PersistGate loading={null} persistor={persistor}>
        <ThemeProvider theme={theme}>
          <DateFnsProvider adapterLocale={locale}>
            <React.StrictMode>
              <App />
            </React.StrictMode>
          </DateFnsProvider>
        </ThemeProvider>
      </PersistGate>
    </Provider>
  </Auth0Provider>
);
