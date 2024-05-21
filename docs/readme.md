# Oficial ApiBee Docker Image
[![Docker Pulls](https://img.shields.io/docker/pulls/itbusina/apibee)](https://hub.docker.com/r/itbusina/apibee/)
[![Docker Stars](https://img.shields.io/docker/stars/itbusina/apibee)](https://hub.docker.com/r/itbusina/apibee/)

No-Code APIs testing tool with JSON configuration.

# Quick start
Here is 2 minimal steps to test you API.

## Create a collection.json file with API requests to test.
```json
{
    "requests": [
      {
        "path": "https://dummyjson.com/products"
      }
    ]
}
```

## Run the docker image
```shell
docker run itbusina/apibee:latest -c "$(<collection.json)"
```

## Or run the example
```shell
docker run itbusina/apibee:latest -c https://raw.githubusercontent.com/itbusina/apibee-public/main/examples/apis.json
```

# Default settings
- Requests are sent via HTTP using ```GET``` method, if not specified.
- Requests are executed sequentially. Pass ```-p``` argument to execute in parallel.
- Request is considered successful if the response contains the successful status code.
- When the directory is passed as a source, all files with ```*.json``` extension from this directory and all subdirectories are read.

# Documentation

## Collection configuration

### Collection with base address
```json
{
    "baseAddress": "https://dummyjson.com",
    "requests": [
      {
        "path": "/users"
      },
      {
        "path": "/products"
      }
    ]
}
```

### Collection without base address
```json
{
    "requests": [
      {
        "path": "https://dummyjson.com/users"
      },
      {
        "path": "https://dummyjson.com/products"
      }
    ]
}
```

### Collection with combination of base address and full url
```json
{
    "baseAddress": "https://dummyjson.com",
    "requests": [
      {
        "path": "/users"
      },
      {
        "path": "https://google.com"
      }
    ]
}
```

### Collection with dependant requests
```json
{
    "name": "Collection with dependant requests",
    "baseAddress": "https://dummyjson.com",
    "requests": [
      {
        "name": "auth",
        "path": "/auth/login",
      },
      {
        "dependsOn": "auth",
        "path": "/users/1",
      },
      {
        "dependsOn": "auth",
        "path": "/products"
      }
    ]
}
```

### Collection with sharing context between requests
```json
{
  "baseAddress": "https://dummyjson.com",
  "requests": [
    {
      "name": "auth",
      "path": "/auth/login",
      "method": "POST",
      "headers": [
        "Content-Type: application/json"
      ],
      "body": "{\"username\":\"${{ secrets.login }}\",\"password\":\"${{ secrets.password }}\"}",
      "context": [
        {
          "name": "token",
          "type": "secret",
          "pattern": "\"token\":\\s*\"([^\"]+)\""
        }
      ]
    },
    {
      "dependsOn": "auth",
      "path": "/auth/me",
      "headers": [
        "Authorization: Bearer ${{ context.token }}"
      ]
    }
  ]
}
```

### Collection with Tags
```json
{
    "name": "Collection with Tags",
    "baseAddress": "https://dummyjson.com",
    "tags": [
      "smoke",
      "regression"
    ],
    "requests": [
      {
        "path": "/users/1"
      }
    ]
}
```

### Collection with http method
```json
{
    "name": "Collection with http method",
    "baseAddress": "https://dummyjson.com",
    "requests": [
      {
        "path": "/users/1",
        "method": "DELETE"
      }
    ]
}
```

### Collection with http request headers
```json
{
    "name": "Collection with http request headers",
    "baseAddress": "https://dummyjson.com",
    "requests": [
      {
        "path": "/users/1",
        "headers": [
          "Content-Type: application/json"
        ]
      }
    ]
}
```

### Collection with multiple API requests
```json
{
    "name": "Collection with multiple API requests",
    "baseAddress": "https://dummyjson.com",
    "requests": [
      {
        "path": "/auth/login",
      },
      {
        "path": "/users/1",
      },
      {
        "path": "/products"
      }
    ]
}
```

### Collection with variables
```json
{
    "name": "Collection with variables",
    "baseAddress": "${{ vars.host }}",
    "requests": [
      {
        "path": "/products"
      }
    ]
}
```

### Collection with secrets
```json
{
    "name": "Collection with secrets",
    "baseAddress": "https://dummyjson.com",
    "requests": [
      {
        "path": "/products"
      },
      {
        "path": "/auth/login",
        "method": "POST",
        "body": "{\"username\":\"${{ secrets.login }}\",\"password\":\"${{ secrets.password }}\"}"
      }
    ]
}
```

### Collections with functions
```json
{
  "name": "Collections with functions",
  "baseAddress": "https://dummyjson.com",
  "requests": [
    {
      "path": "/comments/add",
      "method": "POST",
      "headers": [
        "Content-Type: application/json"
      ],
      "body": "{\"body\":\"This makes all sense to me! Date: ${{ func.utcnow() }}, Guid: ${{ func.guid() }}\",\"postId\":${{ func.random() }},\"userId\":5}"
    }
  ]
}
```

#### Supported functions
```text
- func.utcnow() - Curent UTC datetime
- func.random() - Random number
- func.guid() - New guid
```

### Validators

#### Validate Http Status Code
```json
{
  "name": "Validate Http Status Code",
  "baseAddress": "https://dummyjson.com",
  "requests": [
    {
      "path": "/auth/login",
      "validators": [
        {
          "type": "statuscode",
          "value": "403"
        }
      ]
    }
  ]
}
```

#### Validate Http Body (Full match)
```json
{
  "name": "Validate Http Body (Full match)",
  "baseAddress": "https://dummyjson.com",
  "requests": [
    {
      "path": "/auth/login",
      "validators": [
        {
          "type": "body",
          "value": "{\"id\":1,\"body\":\"This is some awesome thinking!\",\"postId\":100,\"user\":{\"id\":63,\"username\":\"eburras1q\"}}"
        }
      ]
    }
  ]
}
```

#### Validate Http Body (Contains)
```json
{
  "name": "Validate Http Body (Contains)",
  "baseAddress": "https://dummyjson.com",
  "requests": [
    {
      "path": "/auth/login",
      "validators": [
        {
          "type": "body-contains",
          "value": "This is some awesome thinking!"
        }
      ]
    }
  ]
}
```


## Execution

### Display help
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            --help
```

### Specify the license
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c data/collection.json \
            -l $license
```

### Run collection from file. (option 1)
```shell
docker run itbusina/apibee:latest -c "$(<collection.json)"
```

### Run collection from file. (option 2)
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c data/collection.json
```

### Run collection inline.
```shell
$collection = @'
{
  "name": "Dummy JSON collection 1",
  "baseAddress": "https://dummyjson.com",
  "requests": [
    {
      "path": "/users/1"
    }
  ]
}
'@

docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c $collection
```

### Run collections from multiple files
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c data/collection1.json data/collection2.json
```

### Run collections from directory
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c data/
```

### Run collection from URL.
```shell
docker run \
          itbusina/apibee:latest \
            -c https://raw.githubusercontent.com/itbusina/apibee-public/main/examples/apis.json
```

### Run collection from URL with authorization and required http headers.
```shell
docker run \
          itbusina/apibee:latest \
            -c https://api.github.com/repos/user/repo/contents/data/collection.json \
            -h "Authorization: Bearer ghp_dsa987dsad67d8s6a876d7as" "User-Agent:ApiBee" "Accept:application/vnd.github.raw+json"
```

### Run collection in parallel
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c data/collection.json \
            -p
```

### Run collection with multiple tags filter
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c data/collection.json \
            -t smoke regression
```

### Save report to output folder
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c data/collection.json \
            -o output
```

### Pass variables in collection
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c data/collection.json \
            -v host=https://dummyjson.com
```

### Pass secrets in collection
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c data/collection.json \
            -s login=admin,password=Welcome1!
```

### Run collection 5 times
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c data/collection.json \
            -r 5
```

### Run collection 5 times with 1s delay
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c data/collection.json \
            -r 5 \
            -d 1000
```

### Run collection in loop with 5s interval without exit
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c data/collection.json \
            -i 5000
```

### Display details about requests and responses 
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c data/collection.json \
            --verbose
```

### Save details about requests and responses to the file
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c data/collection.json \
            --verbose \
            > output.json
```

### Display and save details about requests and responses to the file
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c data/collection.json \
            --verbose \
            | tee output.json
```

## Integration with GitHub Actions

### Simple tests run
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
        docker run \
          itbusina/apibee:latest \
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

### Run API tests before deployment
```yaml
name: Run API tests before deployment

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
      uses: itbusina/apibee-action@v0.1.15-alpha
      with:
          input_dir: ./test
          output_dir: ./output
          network: container:mynewapi
          args: |
            --collections ./test/tests.json \
            --variables host=http://mynewapi:8080 \
            --output output \
            --license ${{ secrets.APIBEELICENSE }}
        
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
        name: apibee test report
        path: ./output/

```

# Features
- Run from docker.
- Integration with GitHub Actions.
- Filtering API requests using tags.
- Collections can be specified using inline script, directory path, file path, public or secured url. Multiple sources are also supported.
- Parallel/Sequential requests execution.
- Variables, secrets and functions support inside the collection.
- Support dependencies between requests.
- Support sharing context between requests.
- Ability to execute collection multiple times.
- Ability to specify the delay between runs.
- Ability to execute collection in the loop with interval.
