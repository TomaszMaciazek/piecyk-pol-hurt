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
