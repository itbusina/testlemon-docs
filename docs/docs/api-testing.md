# What is API Testing?

_November 18, 2024_

_Keywords: API testing, test API endpoints, REST API, developer, QA tester, user-friendly interface_

_Read time: 30 minutes_

_Level: intermidiate, advanced_

In this article we will explorer in details what can be included in web API testing and which tools can be used to perform it.

## Pre-requisites
It is assumed that you are familiar with web protocols, HTTP terms, client-server architecture, and tools like culr, postman or TestLemon.


## What is API Testing?
Web API testing is a quality assuarance process to validate if the API satisfy functional and non-functional requirements.

## Functional requirements

### Endpoint Functionality
#### Correct response for valid requests.
#### Handling of invalid requests (e.g., missing/invalid parameters).
#### Support for all HTTP methods (GET, POST, PUT, DELETE, etc.) as per API documentation.

### Input Validation
#### Proper handling of various input types, sizes, and formats.
#### Validation of mandatory vs. optional parameters.
#### Handling of edge cases (e.g., null, empty, or large inputs).

### Authentication and Authorization
#### Verification of authentication mechanisms (e.g., API keys, OAuth tokens).
#### Proper enforcement of access controls for different user roles.

### Response Validation
#### Correctness of response data and structure (e.g., JSON, XML schema validation).
#### Adherence to data types and formats (e.g., dates in ISO 8601, numeric fields).

### Error Handling
#### Meaningful and descriptive error messages.
#### Proper use of HTTP status codes (e.g., 200 for success, 404 for not found, 500 for server errors).

### CRUD Operations
#### Validation of Create, Read, Update, and Delete functionality.
#### Idempotency for PUT and DELETE requests.

### Business Logic
#### Validation of rules and calculations implemented in the API.
#### Correct application of filters, sorting, and pagination.

### State Management
#### Proper handling of statelessness (no client data stored on the server across requests unless explicitly required).

## Non-functional requirements

### Performance
#### Response time under normal and peak loads.
#### Throughput (requests per second).
#### Latency and round-trip time (RTT).
#### Compression Enablement

### Scalability
#### API’s ability to handle increased load (horizontal/vertical scaling tests).

### Security
#### Protection against common vulnerabilities (e.g., SQL injection, XSS, CSRF).
#### Secure data transmission using HTTPS.
#### Validation of data encryption (if applicable).
#### DDoS (Distributed Denial of Service) protection.

### Reliability and Availability
#### API uptime and failure handling (e.g., graceful degradation).
#### Testing of retry mechanisms for transient errors.

### Compatibility
#### Testing across different platforms, browsers, and devices.
#### Backward compatibility with older versions.

### Interoperability
#### Ensure compatibility with supported clients, platforms, tools and protocols (HTTP, WebSocket, gRPC, MQTT, SOAP, GraphQL, etc.)

### Compliance
#### Adherence to industry standards (e.g., REST, OpenAPI, SOAP).
#### Regulatory compliance (e.g., GDPR, HIPAA).
#### Test compliance with web accessibility standards (e.g., WCAG).
#### Validate that the API generates proper audit trails for sensitive actions, such as updates or deletes.

### Usability
#### Clarity of API documentation.
#### Ease of integration for developers.

### Maintainability
#### Ability to easily update or extend the API (versioning, modularity).

### Data Integrity
#### Validation of consistent and correct data state across operations.
#### Prevention of data corruption during concurrent operations.

### Logging and Monitoring
#### Validation of proper logging of requests and errors.
#### Support for monitoring metrics (e.g., via tools like Prometheus, ELK Stack).

### Resilience
#### API behavior under adverse conditions (e.g., server crashes, network issues).
#### Failover testing for high availability setups.

### Rate Limiting and Throttling
#### Validation of API rate limits and their enforcement.
#### Testing behavior when limits are exceeded.

### Internationalization (i18n) and Localization (l10n)
#### Test response content for multi-language support.
#### Validate date, currency, and number formats according to locale.
#### Ensure correct handling of character encoding (e.g., UTF-8).

### Lifecycle Management
#### Validate APIs during CI/CD pipeline deployment.
#### Ensure APIs behave consistently across different environments.

### Concurrency and Parallelism Testing
#### Test the API’s handling of multiple simultaneous requests.
#### Verify consistency when multiple clients perform conflicting actions.
#### Ensure no deadlocks occur in resource-intensive operations.

### Caching Validation
#### Verify that APIs cache responses when expected.
#### Validate cache invalidation mechanisms when data changes.

### User Experience (UX) Testing
#### Ensure the API is intuitive for developers, with a logical structure and consistent naming conventions.
#### Check whether error messages provide sufficient information to resolve issues.

## Conclusion