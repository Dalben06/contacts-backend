# Contacts Backend

This is the backend for a contact management system.

## Technologies Used

- **C#** - Main programming language.
- **.NET Core** - Framework for building APIs.
- **Docker** - Application containerization.
- **SQL Server** - Database for storing contacts.

## Features

- CRUD operations for contacts (Create, Read, Update, Delete).
- User Authentication and Authorization.
- API Documentation with Swagger.

## Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/Dalben06/contacts-backend.git

2. Configure the database in `appsettings.json`.
3. Run the application:
    ```bash
    dotnet run

## Docker

To run the application using Docker:

1. Build the image:
   ```bash
   docker build -t contacts-backend .

3. Run the container:
    ```bash
    docker run -p 5000:5000 contacts-backend


## Contributing
Contributions are welcome! Feel free to open issues and pull requests.

## License
This project is licensed under the MIT License. See the LICENSE file for details.