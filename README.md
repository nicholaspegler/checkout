# checkout
This is a .NET Core API application written to act as a Payment Gateway, this includes a Swagger UI.

## Getting Started
Project can be built locally using Visual Studio (F5)

## Considerations for expansion
There are several items which would expand this project

- Authentication:
 - I would add an IdentityServer4 project under the services Folder
 - This will act as the Auth service for the Gateway.
 - It could be further extened with a SQL Database and an Admin Portal UI.
- Data Storage:
 - A SQL Database could be added to assist with data storage.
 - This could store records of requests for audit and traceability.
 - Important consideration should be placed on what should be stored as the content of the Gateway is sensitive. 
 - Ability to remove and report on records for GDPR should be added.
- Logging:
 - Currently a basic log to file has been added.
 - This should be expanded to include a middleware to log each request, with sensitive data redacted.
 - The logging as it is is for tracing errors and is by no means complete.
- Intergration Testing:
 - A test project will need to be added to run through an end to end process.
- CI/CD
 - An automated build should be implimented which will run Unit Tests on every commit, followed by a deployment to a non-production environment to allow Intergration tests to be run.
 - In the past I've used Bamboo and Octopus.



