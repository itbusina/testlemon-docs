# What is API Testing: Comprehensive Guide for Developers and Testers.

_November 20, 2024_

_Keywords: API testing, test API endpoints, REST API, developer, QA tester, user-friendly interface_

_Read time: 30 minutes_

_Level: intermidiate, advanced_

API testing is a critical phase in the software development lifecycle, especially for systems that rely on REST APIs for communication between components. In this guide, tailored for senior and lead software developers and testers, we will explore the essential requirements of API testing, encompassing functional and non-functional aspects. We’ll also provide practical examples using TypeScript and cURL commands for clarity.

## Pre-requisites
It is assumed that you are familiar with web protocols, HTTP terms, client-server architecture, and tools like culr, postman or TestLemon.

## Test Functional requirements

### Endpoint Functionality
#### Correct response for valid requests.
The cornerstone of API testing involves validating that the API endpoints return the expected results for valid inputs.

Example using cURL:

```bash
curl -X GET https://api.example.com/users/123 -H "Authorization: Bearer <token>"
```

Expected Response:

```json
{
  "id": 123,
  "name": "John Doe",
  "email": "johndoe@example.com"
}
```

#### Handling of invalid requests (e.g., missing/invalid parameters).
APIs should gracefully handle errors caused by invalid inputs, returning meaningful error messages.

Example Test Case:
- Input: Invalid User ID (abc instead of numeric).
- Expected Output: HTTP Status Code: 400 Bad Request
- Error Response:

```json
{ "error": "Invalid User ID format" }
```

#### Support for all HTTP methods (GET, POST, PUT, DELETE, etc.) as per API documentation.
APIs should support the HTTP methods specified in their documentation.

Example using TypeScript:

```typescript
const axios = require('axios');
async function testHttpMethods() {
    const methods = ['GET', 'POST', 'PUT', 'DELETE'];
    for (const method of methods) {
        try {
            const response = await axios({ method, url: 'https://api.example.com/resource' });
            console.log(`${method} response:`, response.status);
        } catch (error) {
            console.error(`${method} failed:`, error.response?.status || error.message);
        }
    }
}
testHttpMethods();
```

### Input Validation
#### Proper handling of various input types, sizes, and formats.
Test inputs of different types, such as strings, numbers, dates, and booleans, to ensure correct processing.

Example Test:
For a date field:

```bash
curl -X POST https://api.example.com/events \
-H "Content-Type: application/json" \
-d '{"name": "Launch", "date": "invalid-date-format"}'
```

Expected Response:
- Status Code: 400 Bad Request

#### Validation of mandatory vs. optional parameters.
Ensure that mandatory parameters are enforced while optional ones have default behaviors.

Example Test:
Missing mandatory field:

```bash
curl -X POST https://api.example.com/events \
-H "Content-Type: application/json" \
-d '{"date": "2024-01-01"}'
```

Expected Error: "error": "Field 'name' is required."

#### Handling of edge cases (e.g., null, empty, or large inputs).

```bash
curl -X GET https://api.example.com/users/null'
```

Expected Response:
- Status Code: 400 Bad Request

### Authentication and Authorization
#### Verification of authentication mechanisms (e.g., API keys, OAuth tokens).
APIs often use tokens or keys for authentication. Ensure the mechanisms work as intended.

Example: Test with an expired OAuth token.

```bash
curl -X GET https://api.example.com/secure-data -H "Authorization: Bearer expired_token"
```

Expected Response: 
- Status Code: 401 Unauthorized

#### Proper enforcement of access controls for different user roles.

Request example:
```bash
curl -X GET https://api.example.com/secure-data -H "Authorization: Bearer admin_token"
```

Expected Response:
- Status Code: 200 OK
- Body:  { "secured-data" }

Request example:
```bash
curl -X GET https://api.example.com/secure-data -H "Authorization: Bearer user_token"
```

Expected Response:
- Status Code: 401 Unauthorized
- Body:  { "empty" }

### Response Validation
#### Correctness of response data and structure (e.g., JSON, XML schema validation).
The API response should match the expected schema. Use JSON Schema validation tools like AJV for verification.

Example using TypeScript with AJV:

```typescript
const Ajv = require('ajv');
const ajv = new Ajv();
const schema = { type: 'object', properties: { id: { type: 'number' }, name: { type: 'string' } }, required: ['id', 'name'] };
const validate = ajv.compile(schema);
const response = { id: 1, name: 'Sample' };
console.log(validate(response) ? "Valid response" : "Invalid response");
```

#### Adherence to data types and formats (e.g., dates in ISO 8601, numeric fields).
APIs must strictly enforce the use of correct data types and formats for input and output to ensure consistency and avoid errors.

##### Validating Date Format
Test Input (cURL):
```bash
curl -X POST https://api.example.com/events \
-H "Content-Type: application/json" \
-d '{"name": "Conference", "date": "2024-11-20T10:30:00Z"}'
```

Expected Output:
```json
{
  "id": 101,
  "name": "Conference",
  "date": "2024-11-20T10:30:00Z"
}
```

##### Invalid Date Format

Test Input (cURL):
```bash
curl -X POST https://api.example.com/events \
-H "Content-Type: application/json" \
-d '{"name": "Conference", "date": "20-11-2024"}'
```

Expected Response:
- Status Code: 400 Bad Request
- Body: { "error": "Invalid date format. Use ISO 8601 (YYYY-MM-DDTHH:mm:ssZ)." }

### Error Handling
#### Meaningful and descriptive error messages.
Error messages should clearly explain the problem, enabling developers to identify and fix issues quickly.

A good error message provides:
- Context: What went wrong (e.g., invalid input, missing field).
- Resolution Guidance: Suggestions for correcting the issue.
- HTTP Status Code: Properly aligned with the error type (e.g., 400 Bad Request, 404 Not Found).

Why It Matters
- Reduces debugging time.
- Improves the developer experience.
- Enhances API documentation clarity.

Example: Invalid Field Input

Test Input (cURL):
```bash
curl -X POST https://api.example.com/users \
-H "Content-Type: application/json" \
-d '{"username": "", "email": "invalid-email-format"}'
```

Expected Response:
- Status Code: 400 Bad Request
- Error Message:

```json
{
  "error": {
    "message": "Invalid input data.",
    "details": [
      { "field": "username", "issue": "Username cannot be empty." },
      { "field": "email", "issue": "Email must be in a valid format." }
    ]
  }
}
```

This error message:
1. Describes the overall issue (“Invalid input data”).
2. Specifies which fields are problematic and why.
3. Guides developers toward resolving the error.

#### Proper use of HTTP status codes (e.g., 200 for success, 404 for not found, 500 for server errors).
APIs should adhere to standard HTTP status codes to communicate the outcome of a request effectively. Correct usage improves clarity, consistency, and the developer experience.

Common HTTP Status Codes
- 200 OK: Request succeeded, and the server returned the expected response.
- 201 Created: Resource successfully created (e.g., POST request).
- 400 Bad Request: The server cannot process the request due to client-side errors (e.g., validation failure).
- 401 Unauthorized: Authentication is required or has failed.
- 404 Not Found: The requested resource does not exist.
- 500 Internal Server Error: The server encountered an error it couldn’t handle.

Why It Matters
- Provides clear communication about the request’s outcome.
- Helps developers quickly identify the root cause of issues.
- Aligns with RESTful principles and best practices.

### CRUD Operations
#### Validation of Create, Read, Update, and Delete functionality.
Ensure that repeating the same request does not change the outcome.

#### Idempotency for PUT and DELETE requests.
1. Execute a DELETE request twice.
2. Verify that the first request deletes the resource, and the second returns a 404 Not Found.

### Business Logic
#### Validation of rules and calculations implemented in the API.
APIs often include business logic, such as applying rules, constraints, or performing calculations. Validating these ensures the API correctly enforces logic and produces accurate results.

Example

Scenario: Discount Calculation API

Test Input (cURL):
```bash
curl -X POST https://api.example.com/discounts/calculate \
-H "Content-Type: application/json" \
-d '{"subtotal": 100, "discountCode": "SAVE10"}'
```

Expected Response:
```json
{
  "subtotal": 100,
  "discount": 10,
  "total": 90
}
```

#### Correct application of filters, sorting, and pagination.
APIs often provide filters, sorting, and pagination to manage large datasets effectively. Validating these features ensures the API delivers accurate, organized, and manageable results based on client requests.

Example:

Scenario: Retrieving a List of Users with Filters, Sorting, and Pagination

Test Input (cURL):
```bash
curl -X GET "https://api.example.com/users?role=admin&sort=name&order=asc&page=2&limit=5"
```

Expected Response:
- Status Code: 200 OK

```json
{
  "data": [
    { "id": 6, "name": "Alice", "role": "admin" },
    { "id": 7, "name": "Bob", "role": "admin" },
    { "id": 8, "name": "Charlie", "role": "admin" },
    { "id": 9, "name": "David", "role": "admin" },
    { "id": 10, "name": "Eve", "role": "admin" }
  ],
  "meta": {
    "page": 2,
    "limit": 5,
    "totalPages": 4,
    "totalItems": 20
  }
}
```

### State Management
#### Proper handling of statelessness (no client data stored on the server across requests unless explicitly required).
APIs adhering to REST principles should be stateless, meaning each request should contain all the information needed for the server to process it. No client-specific data should persist on the server between requests unless explicitly required (e.g., sessions or transactions).

Why It Matters
- Ensures scalability by avoiding server-side dependency on client state.
- Simplifies debugging and testing by making requests independent.
- Aligns with RESTful architecture principles.

Example

Scenario: Stateless API for User Authentication

First Request: Login and Obtain Token
Test Input (cURL):
```bash
curl -X POST https://api.example.com/auth/login \
-H "Content-Type: application/json" \
-d '{"username": "john", "password": "password123"}'
```

Expected Response:
```json
{ "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..." }
```

Second Request: Fetch User Details Using Token
Test Input (cURL):
```bash
curl -X GET https://api.example.com/users/me \
-H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
```

Expected Response:
```json
{ "id": 1, "username": "john", "email": "john@example.com" }
```

Validation:
- Ensure the second request is processed independently of the first request’s context.
- Confirm that user details are accessible solely using the token, with no reliance on server-side state.

## Test Non-functional requirements

### Performance
#### Response time under normal and peak loads.
Test API latency with tools like JMeter, Postman or Testlemon.

#### Throughput (requests per second).
Throughput measures the number of requests an API can handle per second under normal or peak conditions. It is a key performance metric that determines the system’s ability to process high volumes of traffic efficiently.

#### Latency and round-trip time (RTT).
Latency refers to the time it takes for a client’s request to reach the server, while round-trip time (RTT) includes the complete journey of a request to the server and back with a response. These metrics are critical for assessing the API’s responsiveness.

#### Compression Enablement
Compression reduces the size of data transmitted between the server and client by using algorithms like Gzip or Brotli. Enabling compression improves API performance by reducing bandwidth usage and speeding up data transfer.

### Scalability
#### API’s ability to handle increased load (horizontal/vertical scaling tests).
Scalability testing ensures that an API can handle increased traffic without degradation in performance. Horizontal scaling involves adding more server instances to distribute the load, while vertical scaling involves increasing the resources (CPU, memory) of existing servers.

### Security
#### Protection against common vulnerabilities (e.g., SQL injection, XSS, CSRF).
Run automated scans with tools like OWASP ZAP to detect vulnerabilities.

#### Secure data transmission using HTTPS.
Verify that sensitive data is not transmitted over HTTP.

Example Test:

```bash
curl -X GET http://api.example.com/secure-data
```

Expected Response:
- Status Code: 403 Forbidden

#### Validation of data encryption (if applicable).
Data encryption ensures that sensitive information transmitted between the client and server is secure and protected from unauthorized access. Validating encryption ensures that data, such as authentication tokens, personal information, or payment details, is encrypted both in transit (using protocols like HTTPS) and at rest (if applicable).

#### DDoS (Distributed Denial of Service) protection.
DDoS protection testing involves simulating high volumes of traffic to assess the API’s ability to withstand malicious attempts to overwhelm its resources. This testing ensures that the API can maintain service availability under heavy, distributed attack scenarios.

### Reliability and Availability
#### API uptime and failure handling (e.g., graceful degradation).
API uptime testing ensures that the API remains available and operational, even during high traffic or system failures. Failure handling, such as graceful degradation, ensures that if certain features or services become unavailable, the API continues to function with reduced capabilities rather than failing completely.

#### Testing of retry mechanisms for transient errors.
Retry mechanisms ensure that the API can automatically recover from temporary issues, such as network glitches or brief service unavailability. Testing these mechanisms involves simulating transient errors (e.g., timeouts or server unavailability) to verify that the API correctly retries the request without overwhelming the system.

### Compatibility
#### Testing across different platforms, browsers, and devices.
Use tools like BrowserStack for cross-platform testing.

#### Backward compatibility with older versions.
Validate that newer API versions do not break integrations with older clients.

### Interoperability
#### Ensure compatibility with supported clients, platforms, tools and protocols (HTTP, WebSocket, gRPC, MQTT, SOAP, GraphQL, etc.)
Compatibility testing ensures that the API functions seamlessly across different clients, platforms, tools, and communication protocols. This includes verifying that the API supports standard protocols like HTTP, WebSocket, gRPC, MQTT, SOAP, and GraphQL, and that it interacts correctly with various platforms (e.g., mobile apps, web clients, third-party integrations).

### Compliance
#### Adherence to industry standards (e.g., REST, OpenAPI, SOAP).
Adherence to industry standards ensures that the API is built using widely accepted practices and frameworks, making it easier to integrate, maintain, and extend. Common standards include REST (for designing APIs with standard HTTP methods), OpenAPI (for defining and documenting RESTful APIs), and SOAP (a protocol for exchanging structured information in web services).

#### Regulatory compliance (e.g., GDPR, HIPAA).
Regulatory compliance testing ensures that the API meets legal and industry-specific requirements related to data privacy and security. For example, GDPR (General Data Protection Regulation) governs data protection and privacy in the European Union, while HIPAA (Health Insurance Portability and Accountability Act) sets standards for protecting sensitive patient data in the U.S.

#### Test compliance with web accessibility standards (e.g., WCAG).
Testing compliance with web accessibility standards ensures that the API and its associated user interfaces are usable by people with disabilities. The WCAG (Web Content Accessibility Guidelines) provides a set of guidelines to ensure web content is accessible to a wide range of users, including those with visual, auditory, motor, or cognitive disabilities.

#### Validate that the API generates proper audit trails for sensitive actions, such as updates or deletes.
Audit trail validation ensures that the API correctly logs and tracks sensitive actions, such as data updates or deletions. An audit trail provides a detailed record of who performed an action, when it was performed, and what data was affected, which is essential for security, compliance, and troubleshooting.

### Usability
#### Clarity of API documentation.
Clarity in API documentation ensures that developers can easily understand how to use the API, its endpoints, methods, parameters, and expected responses. Well-documented APIs provide clear, concise, and comprehensive explanations, including examples and error handling guidance, to help developers integrate and utilize the API effectively.

#### Ease of integration for developers.
Ease of integration refers to how simple it is for developers to incorporate the API into their applications or systems. This includes clear documentation, well-structured endpoints, easy-to-use authentication methods, and minimal setup requirements, ensuring a smooth integration process with minimal friction.

### Maintainability
#### Ability to easily update or extend the API (versioning, modularity).
The ability to easily update or extend an API ensures that changes can be made over time without disrupting existing functionality for consumers. Key practices like versioning and modularity allow developers to introduce new features or improvements while maintaining backward compatibility.

### Data Integrity
#### Validation of consistent and correct data state across operations.
Validating the consistency and correctness of data across operations ensures that the API maintains accurate and reliable data after performing actions like create, read, update, and delete (CRUD). This ensures that all database transactions or changes made through the API are correctly reflected in the system and that there is no data corruption or inconsistency.

#### Prevention of data corruption during concurrent operations.
Preventing data corruption during concurrent operations ensures that the API can handle multiple requests that may attempt to modify the same data simultaneously without causing inconsistencies or errors. This typically involves implementing mechanisms like locking, transaction management, and atomic operations to maintain data integrity.

### Logging and Monitoring
#### Validation of proper logging of requests and errors.
Check that all requests and responses are appropriately logged, without exposing sensitive data like passwords.

Example Test:
Log analysis for failed requests.

#### Support for monitoring metrics (e.g., via tools like Prometheus, ELK Stack).
Support for monitoring metrics involves enabling the API to produce measurable data that can be collected and analyzed to track performance, usage, and potential issues. Tools like Prometheus (for system metrics) and the ELK Stack (Elasticsearch, Logstash, Kibana) are commonly used to collect, visualize, and analyze these metrics in real-time.

### Resilience
#### API behavior under adverse conditions (e.g., server crashes, network issues).
Testing API behavior under adverse conditions ensures that the API can handle unexpected failures, such as server crashes or network interruptions, gracefully without causing data loss or major disruptions. This includes verifying that the API can recover quickly and maintain its functionality even when faced with challenges like downtime, poor connectivity, or partial service failures.

#### Failover testing for high availability setups.
Failover testing ensures that an API remains available even if one or more components of the system fail. In a high availability (HA) setup, failover mechanisms are in place to automatically redirect traffic to backup servers or systems when the primary ones go down. Testing verifies that these mechanisms work as intended, ensuring minimal disruption during server or service failures.

### Rate Limiting and Throttling
#### Validation of API rate limits and their enforcement.
Validation of API rate limits ensures that the API enforces restrictions on how many requests a client can make within a certain time period. This is important for preventing abuse, ensuring fair usage, and protecting the API from being overwhelmed by excessive requests. Rate limiting can be implemented using techniques like token buckets, leaky buckets, or fixed windows.

#### Testing behavior when limits are exceeded.
Testing behavior when API rate limits are exceeded ensures that the API properly handles clients who send too many requests in a given time period. It’s essential to verify that the API responds appropriately, using standard HTTP status codes and providing clear guidance on how users can resolve or wait for the limits to reset.

### Internationalization (i18n) and Localization (l10n)
#### Test response content for multi-language support.
Testing response content for multi-language support ensures that the API correctly handles and returns content in different languages based on the user’s locale or language preferences. This includes verifying that the API can return translated text, currency formats, date and time formats, and other locale-specific elements.

#### Validate date, currency, and number formats according to locale.
Validating date, currency, and number formats according to locale ensures that the API returns correctly formatted data based on the user’s region or language preferences. Different regions have unique conventions for formatting dates, numbers, and currencies, and it’s important for the API to respect these standards for clarity and consistency.

#### Ensure correct handling of character encoding (e.g., UTF-8).
Ensuring correct handling of character encoding guarantees that the API can accurately process and return text data, including special characters and symbols from various languages. UTF-8 is the most widely used encoding standard, supporting a vast range of characters, symbols, and scripts.

### Lifecycle Management
#### Validate APIs during CI/CD pipeline deployment.
Validating APIs during Continuous Integration/Continuous Deployment (CI/CD) pipeline deployment ensures that the API behaves as expected after every update or change, providing automated checks to catch issues early in the development cycle. This step integrates automated tests into the CI/CD process to verify that the API is functioning correctly before being deployed to production.

#### Ensure APIs behave consistently across different environments.
Ensuring APIs behave consistently across different environments (e.g., development, staging, production) is critical for maintaining the reliability and predictability of the API. Each environment might have different configurations, resources, or data, but the API should perform the same way regardless of where it is deployed.

### Concurrency and Parallelism Testing
#### Test the API’s handling of multiple simultaneous requests.
Testing the API’s handling of multiple simultaneous requests ensures that the API can efficiently manage concurrent traffic without degrading performance, causing errors, or leading to inconsistent data. APIs often experience high traffic with multiple users or services interacting at the same time, so it’s essential to validate that the system can handle these situations properly.

#### Verify consistency when multiple clients perform conflicting actions.
Verifying consistency when multiple clients perform conflicting actions ensures that the API maintains data integrity and behaves correctly when multiple users or systems interact with the same resource simultaneously. Conflicting actions could include scenarios where different clients attempt to modify, delete, or update the same data at the same time, which could result in race conditions or data inconsistency.

#### Ensure no deadlocks occur in resource-intensive operations.
Ensuring no deadlocks occur in resource-intensive operations is critical for maintaining the stability and responsiveness of an API under heavy load. A deadlock occurs when two or more processes or threads are blocked forever, waiting for each other to release resources, which can cause the API to freeze or become unresponsive. This issue is particularly common in scenarios where the API performs complex operations involving multiple resources (e.g., databases, files, external services).

### Caching Validation
#### Verify that APIs cache responses when expected.
Verifying that APIs cache responses when expected ensures that the API uses caching mechanisms appropriately to improve performance and reduce unnecessary load on the server. Caching can help speed up response times for frequently requested data by storing responses in temporary storage (e.g., memory, Redis, or HTTP cache) and serving them directly without querying the backend every time.

#### Validate cache invalidation mechanisms when data changes.
Validating cache invalidation mechanisms when data changes ensures that the API serves up-to-date content by properly invalidating or updating cached responses whenever the underlying data is modified. Without effective cache invalidation, clients might receive stale or outdated data, which could lead to inconsistencies or incorrect information being presented.

## Conclusion
API testing is a multifaceted process that requires attention to both functional and non-functional requirements. By leveraging tools like Testlemon, Postman, JMeter, AJV, and automated scripts in TypeScript or cURL, testers can ensure the robustness, security, and reliability of APIs. Developers and testers should embed these practices into CI/CD pipelines to maintain high-quality standards.