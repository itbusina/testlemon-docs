# Test collections

Test collection refers to a comprehensive set of test cases, test scripts, or test scenarios that are organized for evaluating a specific software application, feature, or system. The purpose of a test collection is to ensure thorough testing by systematically covering all functional and non-functional requirements.

Test collections for Testlemon can be defined in YAML or JSON formats.

## Examples

### Collection with base address
```yaml
baseUrl: https://dummyjson.com
tests:
  - url: /users
```

### Collection without base address
```yaml
tests:
  - url: https://dummyjson.com/users
```

### Collection with combination of base address and full url
```yaml
baseUrl: https://dummyjson.com
tests:
  - url: /users
  - url: https://google.com
```

### Collection with dependant tests

Use ```name``` and ```dependsOn``` request properties to create a dependency between tests

YAML config
```yaml
name: Collection with dependant tests
baseUrl: https://dummyjson.com
tests:
- id: auth
  url: "/auth/login"
- dependsOn: auth
  url: "/users/1"
- dependsOn: auth
  url: "/products"
```

JSON config
```json
{
    "name": "Collection with dependant tests",
    "baseUrl": "https://dummyjson.com",
    "tests": [
      {
        "id": "auth",
        "url": "/auth/login"
      },
      {
        "dependsOn": "auth",
        "url": "/users/1"
      },
      {
        "dependsOn": "auth",
        "url": "/products"
      }
    ]
}
```

### Collection with sharing context between tests

Use ```context``` property of request to save the context. The key to the context is defined by ```name``` property. Define the type, it can be ```variable``` (default) or ```secret```. Secret values are masked in the output. Use ```pattern``` property to define the regex for the value from response body to save to the context.

Use ```${{ context.<name> }}``` to use context in further tests.

```yaml
baseUrl: https://dummyjson.com
tests:
- id: auth
  url: "/auth/login"
  method: POST
  headers:
  - 'Content-Type: application/json'
  body: '{"username":"${{ secrets.login }}","password":"${{ secrets.password }}"}'
  context:
  - name: token
    type: secret
    pattern: '"token":\s*"([^"]+)"'
- dependsOn: auth
  url: "/auth/me"
  headers:
  - 'Authorization: Bearer ${{ context.token }}'
```

### Collection with Tags

Tags are used to filter tests.

```yaml
name: Collection with Tags
baseUrl: https://dummyjson.com
tests:
- url: "/users/1" 
  tags:
    - smoke
    - regression
```

### Collection with HTTP method
```yaml
name: Collection with http method
baseUrl: https://dummyjson.com
tests:
- url: "/users/1"
  method: DELETE
```

### Collection with http request headers
```yaml
name: Collection with http request headers
baseUrl: https://dummyjson.com
tests:
- url: "/users/1"
  headers:
  - 'Content-Type: application/json'
```

### Collection with multiple API tests
```yaml
name: Collection with multiple API tests
baseUrl: https://dummyjson.com
tests:
- url: "/auth/login"
- url: "/users/1"
- url: "/products"
```

### Using variables in collection

Use ```${{ vars.<variable name> }}``` to put a variable in the collection.

```yaml
name: Collection with variables
baseUrl: "${{ vars.host }}"
tests:
- url: "/products"
```

### Using secrets in collection

Use ```${{ secrets.<secret name> }}``` to put a secret in the collection.

```yaml
name: Collection with secrets
baseUrl: https://dummyjson.com
tests:
- url: "/auth/login"
  method: POST
  body: '{"username":"${{ secrets.login }}","password":"${{ secrets.password }}"}'
```
