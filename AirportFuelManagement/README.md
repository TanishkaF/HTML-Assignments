# Airport Fuel Inventory Management System

Welcome to the Airport Fuel Inventory Management System! This application provides an interface for managing fuel consumption at airports, including features for signing in, initializing data, managing airports, aircraft, transactions, and generating reports.

## Features

- **Sign-in**: Users can sign in with a default user account.
- **Initialize**: Data can be initialized with options to add airports, aircraft, and manage fuel transaction data.
- **Airport Management**: Allows listing and addition of airports.
- **Aircraft Management**: Facilitates listing and addition of aircraft.
- **Transaction Management**: Supports listing, addition, and reversal of transactions.
- **Reports**: Generates and displays reports, including Airport Summary Report and Fuel Consumption Report.

## Entities / Attributes

- **User**: user_id, name, email, password
- **Airport**: airport_id, airport_name, fuel_capacity, fuel_available
- **Aircraft**: aircraft_id, aircraft_no, airline, source, destination
- **Transaction**: transaction_id, transaction_date_time, transaction_type, airport_id, aircraft_id, quantity, transaction_id_parent

## Technologies / Tools

- **MVC Framework**: Utilized for application development
- **HTML/CSS Template**: Chosen based on preference
- **Database**: Utilized for data storage

## How to Run

1. Clone the repository: `git clone [repository_url]`
2. Set up the database with the provided schema and sample data.
3. Run the MVC application.
4. Sign in with the default user credentials.
5. Use the interface to manage airports, aircraft, transactions, and generate reports.

## Deliverables

- All features are fully functional.
- Object-oriented approach used with clean and modular code.
- Code comments and documentation provided where necessary.
- Unit tests included for implementation.
- README.md file created to guide users on how to run the program.
- Fixed the bugs found by my peers
