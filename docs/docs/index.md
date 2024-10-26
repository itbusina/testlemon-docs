# Documentation

## Overview
Web monitoring and APIs test automation platform.

### SaaS App
[Test Lemon Portal](https://app.testlemon.com)

![Alt text](https://raw.githubusercontent.com/itbusina/testlemon-docs/refs/heads/main/docs/docs/images/hero-black.png "Image showing a screenshot from TestLemon dashboard")

### Docker CLI
[![Docker Pulls](https://img.shields.io/docker/pulls/itbusina/testlemon)](https://hub.docker.com/r/itbusina/testlemon)

### Features

- Web uptime monitoring
- SSL monitoring
- Domain monitoring
- DNS monitoring
- API test automation
- Functional testing
- Sentiment validation
- AI assertions
- Sitemap validators
- GitHub Actions Integration
- Email notifications
- Slack notifications 
- YAML & JSON configs
- Secrets and variables
- Tests dependencies
- SaaS App
- SaaS APIs
- CLI tool
- Available in docker
- Maintenance window
- Status page
- Project badges
- Tagging for tests suites
- Online documentation
  
### Quick start

#### Create a collection file with API tests to test.

YAML config
```yaml
tests:
- url: https://dummyjson.com/products
```

JSON config
```json
{
    "tests": [
      {
        "url": "https://dummyjson.com/products"
      }
    ]
}
```

#### Run docker image
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)"
```

#### Run the example collection url
```shell
docker run itbusina/testlemon:latest -c https://raw.githubusercontent.com/itbusina/testlemon-docs/refs/heads/main/examples/quick-start.yaml
```

#### Display help
```shell
docker run itbusina/testlemon:latest --help
```

#### Run collection from file. (option 1)
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)"
```

#### Run collection from file. (option 2)

If you would like to set the path to the file, make sure the path is visible in the docker image, in order to do that, mount a docker volume.

```shell
docker run \
          -v ./:/app/data \
          itbusina/testlemon:latest \
            -c data/collection.json
```

#### Specify the license
```shell
docker run itbusina/testlemon:latest \
            -c "$(<collection.json)" \
            -l $license
```

#### Run collection inline.
```shell
$collection = @'
name: Dummy JSON collection
baseUrl: https://dummyjson.com
tests:
  - url: /users/1
'@

docker run itbusina/testlemon:latest -c $collection
```

#### Run collections from multiple files
```shell
docker run -v ./:/app/data itbusina/testlemon:latest -c data/collection1.json data/collection2.json
```

#### Run collections from directory
```shell
docker run -v ./:/app/data itbusina/testlemon:latest -c data/
```

#### Run collection from URL.
```shell
docker run itbusina/testlemon:latest -c https://raw.githubusercontent.com/itbusina/testlemon-docs/refs/heads/main/examples/quick-start.yaml
```

#### Run collection from URL with authorization and required http headers.
```shell
docker run itbusina/testlemon:latest -c https://api.github.com/repos/user/repo/contents/data/collection.json -h "Authorization: Bearer ghp_dsaxxxxxxyyyyyyyzzzzz" "User-Agent:testlemon" "Accept:application/vnd.github.raw+json"
```

#### Run collection in parallel
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" -p
```

#### Run collection with multiple tags filter
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" -t smoke regression
```

#### Save report to output folder
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" -o output
```

#### Pass variables in collection
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" -v host=https://dummyjson.com
```

#### Pass secrets in collection
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" -s login=admin,password=Welcome1!
```

#### Run collection 'N' times
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" -r 5
```

#### Run collection 'N' times with 1s delay
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" -r 5 -d 1000
```

#### Run collection in loop with 'X' interval without exit
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" -i 5000
```

#### Display details about tests and responses 
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" --verbose
```

#### Save details about tests and responses to the file
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" --verbose > output.json
```

#### Display and save details about tests and responses to the file
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" --verbose | tee output.json
```

## Configurations

### Collection

#### Collection with base address
```yaml
baseUrl: https://dummyjson.com
tests:
  - url: /users
```

#### Collection without base address
```yaml
tests:
  - url: https://dummyjson.com/users
```

#### Collection with combination of base address and full url
```yaml
baseUrl: https://dummyjson.com
tests:
  - url: /users
  - url: https://google.com
```

#### Collection with dependant tests

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

#### Collection with sharing context between tests

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

#### Collection with Tags

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

#### Collection with HTTP method
```yaml
name: Collection with http method
baseUrl: https://dummyjson.com
tests:
- url: "/users/1"
  method: DELETE
```

#### Collection with http request headers
```yaml
name: Collection with http request headers
baseUrl: https://dummyjson.com
tests:
- url: "/users/1"
  headers:
  - 'Content-Type: application/json'
```

#### Collection with multiple API tests
```yaml
name: Collection with multiple API tests
baseUrl: https://dummyjson.com
tests:
- url: "/auth/login"
- url: "/users/1"
- url: "/products"
```

#### Using variables in collection

Use ```${{ vars.<variable name> }}``` to put a variable in the collection.

```yaml
name: Collection with variables
baseUrl: "${{ vars.host }}"
tests:
- url: "/products"
```

#### Using secrets in collection

Use ```${{ secrets.<secret name> }}``` to put a secret in the collection.

```yaml
name: Collection with secrets
baseUrl: https://dummyjson.com
tests:
- url: "/auth/login"
  method: POST
  body: '{"username":"${{ secrets.login }}","password":"${{ secrets.password }}"}'
```

### Functions

Use ```${{ func.<function name> }}``` to put a function result in the collection.

#### All Supported functions

| Function Name                | Description |
| --------                     | -------     |
| ```${{ func.utcnow() }}```   | Returns curent UTC datetime.|
| ```${{ func.random() }}```   | Returns random number.|
| ```${{ func.guid() }}```     | Returns new GUID.|
| ```${{ gpt-4o.text(100) }}```| Returns text from OpenAI API (GPT-4o) with ```100``` tokens.|

Notes: make sure to specify the OpenAPI key and endpoint to use 'gpt-' function.

#### Function examples

##### Basic functions
```yaml
name: Collections with functions
baseUrl: https://dummyjson.com
tests:
- url: "/comments/add"
  method: POST
  headers:
  - 'Content-Type: application/json'
  body: '{"body":"This makes all sense to me! Date: ${{ func.utcnow() }}, Guid: ${{
    func.guid() }}","postId":${{ func.random() }},"userId":5}'
```

##### LLM functions
Currently you can use any OpenAI models and gemma:2b model from Ollama.

```yaml
name: Collections with functions
baseUrl: https://dummyjson.com
tests:
- url: "/comments/add"
  method: POST
  headers:
  - 'Content-Type: application/json'
  body: '{"body":"${{ gpt-4o.text(50) }}","postId":1,"userId":5}'
- url: "/comments/add"
  method: POST
  headers:
  - 'Content-Type: application/json'
  body: '{"body":"${{ gpt-4.text(50) }}","postId":1,"userId":5}'
- url: "/comments/add"
  method: POST
  headers:
  - 'Content-Type: application/json'
  body: '{"body":"${{ gemma:2b.text(50) }}","postId":1,"userId":5}'
```

### Validators

#### All supported validators

| Validator Name                                       | Description |
| --------                                             | -------     |
| is-successful: true &#124; false                     | Validates if the HTTP response is successful or not. When ```bool``` value is ```true``` status code is checked to be in the range 200-299.|
| status-code: 200 &#124; 301 &#124; 404               | Validates the HTTP status code of the response.|
| body-equals: keyword                                 | Validates that the response body exactly matches the provided ```keyword``` value. |
| body-not-equals: keyword                             | Validates that the response body does not matche the provided ```keyword``` value. |
| body-contains: keyword                               | Validates if the response body contains the ```text``` value.|
| response-time: number                                | Validates that the response time is less than the ```time``` value in milliseconds.|
| sentiment: negative &#124; neutral &#124; positive   | Validates that the response time is less than the ```time``` value in milliseconds.|
| dns-dkim-exists: true &#124; false                   | Validates that the DNS has DKIM record.|
| dns-spf-exists: true &#124; false                    | Validates that the DNS has SPF record.|
| dns-dmarc-exists: true &#124; false                  | Validates that the DNS has DMARC record.|
| dns-dmarc-single-record: true &#124; false           | Validates that the DNS has only one DMARC record.|
| dns-dmarc-strict-policy: true &#124; false           | Validates that the DNS has DMARC policy set to ```reject``` or ```quarantine```.|
| dns-record-exists: TXT:name:value                    | Validates that the DNS has ```TXT``` record with ```name``` and ```value```.|
| prompt: gpt-4o:keyword                               | Validates that the response body text is ```keyword``` using ```gpt-4o``` LLM model.|
| sitemap-any-body-contains: keyword                   | Validates that any page from all pages in sitemap contain ```keyword``` in the body.|
| sitemap-any-body-not-contains: keyword               | Validates that any page from all pages in sitemap do not contain ```keyword``` in the body.|
| sitemap-each-body-contains: keyword                  | Validates that all pages in sitemap contain ```keyword```.|
| sitemap-each-body-not-contains: keyword              | Validates that all pages in sitemap do not contain ```keyword```.|
| ssl-expiration-after: 30d                            | Validates that domain SSL certificate expiration date after ```30``` days from now.|
| ssl-expiration-before: 30d                           | Validates that domain SSL certificate expiration date before ```30``` days from now.|
| domain-expiration-after: 30d                         | Validates that domain name expiration date after ```30``` days from now.|
| domain-expiration-before: 30d                        | Validates that domain name expiration date before ```30``` days from now.|
| domain-expiration-before: 30d                        | Validates that domain name expiration date before ```30``` days from now.|
| tls-version-equals: Tls12 &#124; Tls13               | Validates that the connection can be established using ```Tls12``` or ```Tls13``` TLS version protocol.|
| tls-version-not-equals: Tls12 &#124; Tls13           | Validates that the connection can not be established using ```Tls12``` or ```Tls13``` TLS version protocol.|

#### Validators examples

##### Validate Http Status Code
```yaml
name: Validate Http Status Code
baseUrl: https://dummyjson.com
tests:
- url: "/auth/login"
  validators:
  - status-code: '403'
```

##### Validate Http Body (Full match)
```yaml
name: Validate Http Body (Full match)
baseUrl: https://dummyjson.com
tests:
- url: "/auth/login"
  validators:
  - body-equals: '{"id":1,"body":"This is some awesome thinking!","postId":100,"user":{"id":63,"username":"eburras1q"}}'
```

##### Validate Http Body (Contains)
```yaml
name: Validate Http Body (Contains)
baseUrl: https://dummyjson.com
tests:
- url: "/auth/login"
  validators:
  - body-contains: This is some awesome thinking!
```

##### Validate Response time
```yaml
name: Validate Http Body (Contains)
baseUrl: https://dummyjson.com
tests:
- url: "/auth/login"
  validators:
  - response-time: 1000
```

##### Validate Response Body Sentiment
```yaml
name: Validate Sentiment
baseUrl: https://dummyjson.com
tests:
- url: "/comments?limit=1&select=body"
  validators:
  - sentiment: positive
```

## Integrations

### GitHub Actions

#### Run tests in pipeline using docker image
```yaml
name: Simple tests

on:
  push:
    branches: [ "main" ]
  workflow_dispatch:

jobs:
  tests:
    runs-on: ubuntu-latest
    
    steps:
    - uses: actions/checkout@v4
        
    - name: Execute API tests
      run: |
        docker run itbusina/testlemon:latest \
            -c "$(<data/collection.json)" \
            -p \
            -o output \
            -l $license \
            > simple_test.txt

    - name: Archive output results
      if: always()
      uses: actions/upload-artifact@v4
      with:
        name: report
        path: simple_test.txt

```

#### Run tests in pipeline using github actions
```yaml
name: Simple tests

on:
  push:
    branches: [ "main" ]
  workflow_dispatch:

jobs:
  tests:
    runs-on: ubuntu-latest
    
    steps:
    - uses: actions/checkout@v4
        
    - name: Test API 
      uses: itbusina/testlemon-action@v0.2
      with:
          input_dir: ./test
          output_dir: ./output
          args: |
            --collections ./test/tests.json \
            --variables host=http://mynewapi:8080 \
            --output output \
            --license ${{ secrets.LICENSE }}

```

#### Example how to test api before deployment to production
```yaml
name: Run tests before live deploy

on:
  push:
    branches:
      - main
  workflow_dispatch:

jobs:
  build:
    runs-on: 'ubuntu-latest'

    steps:
    - name: Code checkout
      uses: actions/checkout@v4

    - name: Log in to Registry
      uses: docker/login-action@v3.1.0
      with:
        registry: https://index.docker.io/v1/
        username: ${{ vars.DOCKERUSERNAME }}
        password: ${{ secrets.DOCKERACCESSTOKEN }}

    - name: Build Container Image
      uses: docker/build-push-action@v5.3.0
      with:
        push: false
        load: true
        tags: mynewapi:${{ github.sha }}
        file: ./src/webapp/Dockerfile

    - name: Create Docker Network
      run: docker network create vnet
        
    - name: Run mynewapi in docker for testing
      run: |
        docker run -d \
          -p 8080:8080 \
          --name mynewapi \
          --network vnet \
          mynewapi:${{ github.sha }}
        
    - name: Test Mock API 
      uses: itbusina/testlemon-action@v0.2
      with:
          input_dir: ./test
          output_dir: ./output
          network: container:mynewapi
          args: |
            --collections ./test/tests.json \
            --variables host=http://mynewapi:8080 \
            --output output \
            --license ${{ secrets.LICENSE }}
        
    - name: Build image and push
      uses: docker/build-push-action@v5.3.0
      with:
        push: true
        tags: |
          index.docker.io/${{ vars.DOCKERUSERNAME }}/apimock:latest
        file: ./src/webapp/Dockerfile

    - name: Archive output results
      if: always()
      uses: actions/upload-artifact@v4
      with:
        name: testlemon test report
        path: ./output/

```
