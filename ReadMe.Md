The project is building completing without any errors

To run the project inlcude the below steps 

Run the EF migrations to initial create the DB tables . I have used the Code first appraoch to create the tables .

1. dotnet ef migrations add InitialCreate
 

 2. dotnet ef database update

 3. dotnet run