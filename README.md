This project aims to be a netcore base to start any data driven application. Though there are some nice things like entity framework being used, I do intend to keep things as lightweight as possible while still keeping modern practices. 

## Project setup
*Before running the project for the first time set your values in appsettings.Development.json*

1. Copy contents of appsettings.Development.example.json into appsettings.Development.json
2. Set a value for your admin password. This will be the password/secret we use for our main api access.
3. Set a value for a jwtKey, make it secure.
4. Set Mysqlconnection with your connection string. Format - server=localhost;user={username};database={dbname};port=3306;password={dbpassword}