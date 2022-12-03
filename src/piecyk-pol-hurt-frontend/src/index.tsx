import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.scss';
import App from './App';
import { Auth0Provider } from '@auth0/auth0-react';
import { Auth0 } from './API/Auth0/Auth0';

const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement);
root.render(
  <Auth0Provider
    domain={Auth0.domain}
    clientId={Auth0.clientID}
    audience={Auth0.audience}
    scope={Auth0.scope}
    redirectUri={window.location.origin}
  >
    <React.StrictMode>
      <App />
    </React.StrictMode>
  </Auth0Provider>
);
