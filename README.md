# TechnicalRadiation

Large Assignment I in T-514-VEFT, Web services. Reykjavik University, fall 2021.

A RESTful web API with information about news articles, containing authors and categories.
Follows HATEOAS for navigation purposes. Each element contains a _links attribute for easier
navigation.
Uses an SQLite database to store the data, the db file is in the WebApi root directory.
The authorized endpoints can be accessed by setting the authorization header to 'secret'.
A more thorough description is found in 'Large-Assignment-I.pdf'.

Uses a global exception handler to handle invalid input, trying to fetch items with a non existing ID etc. 
This does make the running terminal display the stack trace, but it always returns the correct status code.

To run: cd TechnicalRadiation/TechnicalRadiation.WebApi
dotnet run
  
