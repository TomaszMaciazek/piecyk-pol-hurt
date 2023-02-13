# Getting Started with Piecyk-Pol-Hurt frontend

### In the project directory to install packages you have to run:

### `npm install`

### To start the app in the development mode you have to run:

### `npm start`
### Application will be visible on the http://localhost:3000 in your browser. The page will reload if you make edits. You will also see any lint errors in the console.

### Before you start you need fill Object in src/Api.Auth0/Auth0.ts.  All neccessary information you will find in Auth0 site 
```json
{
  "domain": "DOMAIN",
  "audience": "AUDIENCE",
  "clientID": "CLIENT_ID,
  "scope": 'openid email profile',
}
```

### In src/config json you need add information about backend url 
```json
{
  "BASE_URL": "BACKEND_URL",
}
```
