# piecyk-pol-hurt

## This project consist of three main parts

* API - backend API project
* Frontend - frontend React project
* DataImport - module responsible for data import from spreadsheet files

## API project

API project consist of following project:
 * API - endpoints project
 * DataImport project - responsible for communication using gRPC
 * Model project - containig entities, dtos, queries, commands classes and enums that are used by the rest of solution projects
 * DataLayer project - persistance layer, containing DbContext and its migration classes and also unit of work pattern repositories
 * ApplicationLogic project - services layer project
 * Mappings project - contains automapper profile classes
 * Validation project - containing validator classes created with FluentValidation library

In order to run application correctly it is required to start API and DataImport projects.

Before you run projects you need fill data in appsettings.json 
```json
{
  "Auth": {
   "Domain": "AUTH0_DOMAIN",
   "Audience": "AUTH_AUDIENCE"
  },
  "ConnectionStrings": {
    "DefaultConnection": "CONNECTION_STRING"
  },
  "Azure": {
    "SendEmailUrl": "AZURE_LOGIC_APP_EMAIL_URL"
  }
}
```

## Getting Started with Piecyk-Pol-Hurt frontend

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
  "clientID": "CLIENT_ID",
  "scope": "openid email profile",
}
```

### In src/config json you need add information about backend url 
```json
{
  "BASE_URL": "BACKEND_URL",
}
```
