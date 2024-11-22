# Chaining tests in test collection

Test chaining is essential for verifying complex scenarios where one test’s outcome directly influences another or when executing end-to-end workflows. By configuring test dependencies, it’s possible to seamlessly pass data between tests, enabling a cohesive and comprehensive testing process.

## Overview

### Define test dependencies

Use ```name``` and ```dependsOn``` fields in tests to create a dependency.

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

### Share context between tests

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