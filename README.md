# New Version Link : https://github.com/NlabsGlobalFullStack/CleanEHospitalApp
# eHospital - Hospital Information System

Welcome to eHospital, a comprehensive Hospital Information System.

## Features
- User Authentication (Login and Registration)
- Business Logic Implementation
- Data Access Layers
- Utilizes AutoMapper and FluentValidation
- Full Stack Development with ASP.NET Core

## Technologies Used
- Backend: ASP.NET Core
- Libraries: AutoMapper, FluentValidation
- Layers: Business, DataAccess, Entities, WebAPI

## Controllers

**AuthController**

- **Login**
  - *Endpoint:* `/api/auth/login`
  - *Method:* POST
  - *Response:* Detailed login response.

- **GetTokenByRefreshToken**
  - *Endpoint:* `/api/auth/refresh-token/{refreshToken}`
  - *Method:* GET
  - *Response:* Detailed response for token retrieval.

- **SendConfirmMail**
  - *Endpoint:* `/api/auth/send-confirm-mail/{mail}`
  - *Method:* GET
  - *Response:* Detailed response for email confirmation.

- **ConfirmMail**
  - *Endpoint:* `/api/auth/confirm-mail/{emailConfirmCode}`
  - *Method:* GET
  - *Response:* Detailed response for email confirmation.

**UsersController**

- **Create**
  - *Endpoint:* `/api/users/create`
  - *Method:* POST
  - *Request Body:*
    ```json
    {
      "username": "example",
      "password": "password123",
      "email": "example@example.com"
    }
    ```
  - *Response:* Detailed response for user creation.

**Response:** Detailed response for user creation.

## Getting Started
To get started with the project, follow these steps:

1. Clone the repository: `git clone https://github.com/NlabsGlobalFullStack/eHospital.git`
2. Navigate to the project folder: `cd eHospital`
3. Install dependencies: `dotnet restore`
4. Run the application: `dotnet run`

## Contribution
We welcome contributions! If you would like to contribute, please fork the repository and submit a pull request.

## Issues
If you encounter any issues or have questions, please open an [issue](https://github.com/NlabsGlobalFullStack/eHospital/issues).

## License
This project is licensed under the [MIT License](LICENSE).

Feel free to explore the code and contribute to the project.

Thank you for your interest in eHospital!
