# checkout
This is a .NET Core API application written to act as a Payment Gateway, this includes a Swagger UI and a Mock Bank Api.

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
- Integration Testing:
  - A test project will need to be added to run through an end to end process.
- CI/CD
  - An automated build should be implemented which will run Unit Tests on every commit, followed by a deployment to a non-production environment to allow Integration tests to be run.
  - In the past I've used Bamboo and Octopus.
- Expansion on Enums:
  - There are only a few enums in place as a placeholder and this will need to be extended to cover the full market.

## Assumptions
- I took a best guess at what the payload for Bank would require to make a payment.
- Guids/UUIDs have been used for the Id, though I do note that a string could be used in its place to account for different Id formats.
  - The idea would be that the DB would store the real Id so as to not expose it and it could be retrieved when required.
- The Appsettings was used in this instance for the Bank Url and Credentials, this should be switched out to another form of storage which would be able to handle multiple banks or credentials.
  - A DB could be used, how ever it maynot be the most efficient storage method.
- Initially I was going to go down the route of implementing OAuth2 Bearer Token to the request, though this may not be suitable for all banks or entities.
- Model validation was done where possible
  - The card number and cvv validation should be extended. 
  - Cvv is validated using basic regex. 
  - Cardnumber is using the built in CreditCard Attribute, a dedicated Luhm formula for vaidate it is actually a card with something to chec kthe 

# Endpoints

These endpoints follow a REST format

## POST

`Request Payload:`
```json
{
  "currency": "EUR",
  "amount": 0,
  "cardDetails": {
    "nameOnCard": "string",
    "cardType": "Credit",
    "issuer": "Amex",
    "cardnumber": "string",
    "cvv": "string",
    "expiryMonth": 0,
    "expiryYear": 0
  },
  "recipientDetails": {
    "name": "string",
    "sortCode": "string",
    "accountnumber": "string",
    "paymentRefernce": "string"
  }
}
```

All inputs are Required unless marked as optional.

**PaymentRequest**
| Input | Type | Note |
| ----- | ----- | ----- |
| Currency | enum | CurrenyCode |
| Amount | double | Must be greater then zero |
| CardDetails | object | PaymentCardRequest |
| RecipientDetails | object | PaymentRecipientRequest |

**CurrencyCode**
| Code | Note |
| ----- | ----- |
| EUR | Euro |
| GDP | British Pound |
| USD | United States Dollar |

**PaymentCardRequest**
| Input | Type | Note |
| ----- | ----- | ----- |
| NameOnCard | string | |
| CardType | enum | CardType |
| Issuer | enum | Issuer |
| Cardnumber | string | CreditCard Attribute |
| Cvv | string | Regex, 3 or 4 digit based on Issuer |
| ExpiryMonth | int | Range 1-12 |
| ExpiryYear | int | May not be in the past |

**CardType**
| Code | Note |
| ----- | ----- |
| Credit | Credit card |
| Debit | Debit card |

**Issuer**
| Code | Note |
| ----- | ----- |
| Amex | American Express |
| MasterCard | Master Card |
| Visa | Visa |

**PaymentRecipientRequest**
| Input | Type | Note |
| ----- | ----- | ----- |
| Name | string | |
| SortCode | string |  |
| AccountNumber | string |  |
| PaymentReference | string | optional |

`Response:`
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "href": "string",
  "status": "string"
}
```

**PaymentCreatedResponse**
| Input | Type | Note |
| ----- | ----- | ----- |
| Id | string | Id to use in the GET |
| Status | string | Payment status |
| Href | string | GET Self link |


## GET

Using the Id returned in the GET a request can be made to get the payment.

`Response:`
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "status": "string",
  "currency": "string",
  "amount": 0,
  "cardDetails": {
    "nameOnCard": "string",
    "cardType": "string",
    "issuer": "string",
    "cardnumberLast4": "string",
    "cvv": "string",
    "expiryMonth": 0,
    "expiryYear": 0
  }
}
```

**PaymentResponse**
| Input | Type | Note |
| ----- | ----- | ----- |
| Id | Guid | PaymentId |
| Status | string | Payment status |
| Currency | string |  |
| Amount | double |  |
| CardDetails | object | PaymentCardResponse |

**PaymentCardResponse**
| Input | Type | Note |
| ----- | ----- | ----- |
| NameOnCard | string | |
| CardType | string |  |
| Issuer | string |  |
| CardnumberLast4 | string | Returns only the last 4 characters of the cardnumber |
| Cvv | string | Returns *** |
| ExpiryMonth | int |  |
| ExpiryYear | int |  |
