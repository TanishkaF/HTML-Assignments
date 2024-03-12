# Airport Fuel Inventory Management System

## Introduction

This is a web-based application designed to manage and maintain fuel consumption at airports. It provides an interface for airport administrators to track fuel levels, manage aircraft, record fuel transactions, and generate reports.

## Features

- **User Authentication**: Users can sign in using their email and password.
- **Data Initialization**: Allows initializing data by adding airports, aircraft, and removing all transactional data.
- **Airport Management**: Admins can view a list of airports, add new airports, and update existing ones.
- **Aircraft Management**: Admins can view a list of aircraft, add new aircraft, and update existing ones.
- **Transaction Management**: Admins can view a list of transactions, add new transactions, and reverse existing ones.
- **Reports**: Provides comprehensive reports including Airport Summary Report and Fuel Consumption Report.

## Technologies Used

- Ruby on Rails: Backend framework for handling server-side logic and database management.
- React.js: Frontend library for building user interfaces.
- MySQL: Relational database management system for storing application data.
- HTML/CSS: Markup and styling for the frontend views.
- JavaScript: Programming language for frontend interactivity and logic.

## Setup and Installation

1. Clone the repository from GitHub.
2. Navigate to the project directory.
3. Install dependencies using `bundle install` for Ruby gems and `npm install` for Node.js packages.
4. Configure the database connection in `config/database.yml`.
5. Run database migrations with `rails db:migrate` to set up the database schema.
6. Seed the database with sample data using `rails db:seed`.
7. Start the Rails server with `rails server`.
8. Navigate to `http://localhost:3000` in your web browser to access the application.

## Usage

- Upon accessing the application, sign in using the default user credentials.
- Navigate through the sections to perform various operations such as initializing data, managing airports, aircraft, transactions, and generating reports.
- Follow on-screen instructions and prompts to complete tasks and view reports.

## Contributing

Contributions are welcome! If you find any issues or have suggestions for improvement, please feel free to open an issue or submit a pull request.

## License

This project is licensed under the [MIT License](LICENSE).
