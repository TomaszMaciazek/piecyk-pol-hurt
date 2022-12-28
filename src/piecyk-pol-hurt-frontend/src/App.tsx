import React, { useEffect, useState } from 'react';
import logo from './logo.svg';
import { useAuth0 } from '@auth0/auth0-react';
import axios from 'axios';
import { getPermissions } from './API/Endpoints/User/User';

function App() {
  const { isAuthenticated, isLoading, loginWithRedirect, logout, getAccessTokenSilently } = useAuth0();

  const [isAccessTokenSet, setIsAccessTokenSet] = useState<boolean>(false);
  document.title = 'Piecyk Pol Hurt';

  if (!isLoading && !isAuthenticated) {
    loginWithRedirect();
  }

  const getAccessToken = async () => {
    const accessToken = await getAccessTokenSilently();

    axios.interceptors.request.use((config) => {
      if (config && config.headers) {
        config.headers['Authorization'] = `Bearer ${accessToken}`;
      }
      return config;
    });

    setIsAccessTokenSet(true);
  };

  useEffect(() => {
    if (!isAccessTokenSet && isAuthenticated) {
      getAccessToken();
    }
  }, [isAccessTokenSet, isAuthenticated]);

  if (!isAccessTokenSet) {
    return <></>;
  }

  return (
    <div className='App'>
      <button
        onClick={() =>
          getPermissions().then((data) => {
            console.log(data);
          })
        }
      >
        Test
      </button>
      <header className='App-header'>
        <button onClick={() => logout({ returnTo: window.location.origin })}>Wyloguj</button>
        <img src={logo} className='App-logo' alt='logo' />
        <p>
          Edit <code>src/App.tsx</code> and save to reload.
        </p>
        <a className='App-link' href='https://reactjs.org' target='_blank' rel='noopener noreferrer'>
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
