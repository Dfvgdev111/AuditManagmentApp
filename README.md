# Audit Management System
# Project Description
The Audit Management System is a backend application designed to help organizations manage and track audits. It provides functionalities for creating, editing, deleting, and viewing audits.

The system is built using **ASP.NET Core** for the backend, with data stored in a database. The API is fully documented using Swagger for easy interaction and testing of endpoints.



# Features 
* User Authentication and Authorization (JWT-based)
* Create, Edit, and Delete Audit Records
* View Audit Details and Track Progress
* Generate Reports for Audits
## Technologies Used
* ASP.NET Core (C#)
* Entity Framework Core (Database ORM)
* SQL Server (or any other database you're using)
* JWT Authentication (for secure login)
( Swagger (API Documentation and Testing)

# Setup and Installation
to set up the project locally, follow these instructions


## 1.Clone the Repoistory
   ```git clone https://github.com/your-username/Audit-Management-System.git```

## 2.Setup
1. Go to the backend directory:
```
cd backend
```
2. Install the require packages
```
dotnet restore
```


3. Configure your database connection via `appsettings.json`

4. Run the application
  ``` 
  dotnet run
  ```
# Api Documentation
The api is documented using Swagger. once you get the application running you can access the Swager Ui at:
``` 
http://localhost:5000/Swagger
  ```
## Swagger UI

Swagger Ui allows you to explore all available endpoints and test them directly via the browser. You will be able too 

* View the lists of available endpoints
* See each endpoint and the parameters they accept
* Test the endpoints by sending requests and vieewing responses
* Authenticate for verfication of JWT Tokens

