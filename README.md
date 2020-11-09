# checkout
This is a .NET Core API application written to act as a Payment Gateway, this includes a Swagger UI.

## Getting Started
Project can be built locally using Visual Studio (F5)

## Considerations for Expansion
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
- Data Layer:
  - A data layer with an EntityFrameworkCore will need to be added to interact with the DB.
- Logging:
  - Currently a basic log to file has been added.
  - This should be expanded to include a middleware to log each request, with sensitive data redacted.
  - The logging as it is is for tracing errors and is by no means complete.
- Intergration Testing:
  - A test project will need to be added to run through an end to end process.
- CI/CD
  - An automated build should be implimented which will run Unit Tests on every commit, followed by a deployment to a non-production environment to allow Intergration tests to be run.
  - In the past I've used Bamboo and Octopus.
- Expansion on Enums:
  - There are only a few enums in place as a placeholder and this will need to be extended to cover the full market.

## Assumptions
- I took a best guess at what the payload for Bank would require to make a payment.
- Guids/UUIDs have been used for the Id, though I do note that the a string could be used in its place to account for different Id formats.
  - The idea would be that the DB would store the real Id so as to not expose it and it could be retrieved when required.
- The Appsettings was used in this instance for the Bank Url and Credentials, this should be switched out to another form of storage which would be able to handle multiple banks or credentials.
  - A DB could be used, how ever it maynot be the most efficient storage method.
- Initially I was going to go down the route of implementing OAuth2 Bearer Token to the request, though this may not be suitable for all banks or entities.
- Model validation was done where possible
  - The card number and cvv validation should be extended. 
  - Cvv is validated using basic regex. 
  - Cardnumber is using the built in CreditCard Attribute, a dedicated Luhm formula for vaidate it is actually a card with something to chec kthe 

# 
