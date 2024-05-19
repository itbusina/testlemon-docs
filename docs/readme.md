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
docker run -v ./data:/app/data itbusina/apibee:latest -c data/collection.json
```

# Default settings
- Requests are sent via HTTP using 'GET' method, if not specified.
- Requests are executed sequentially. Pass '-p' argument to execute in parallel.
- Request is considered successful if the response contains the successful status code.
- When the directory is passed as a source, all files with '*.json' extension from this directory and all subdirectories are read.

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
- ${{ func.utcnow() }} - DateTime.UtcNow.ToString("o")
- ${{ func.random() }} - new Random().Next().ToString()
- ${{ func.guid() }} - Guid.NewGuid().ToString()

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

### Run collection from file.
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c data/collection.json -l eyJdsa77b2FkIjp7IklkIjoiZ...
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
            -c $collection -l eyJdsa77b2FkIjp7IklkIjoiZ...
```

### Run collections from multiple files
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c data/collection1.json data/collection2.json -l eyJdsa77b2FkIjp7IklkIjoiZ...
```

### Run collections from directory
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c data/ -l eyJdsa77b2FkIjp7IklkIjoiZ...
```

### Run collection from URL.
```shell
docker run \
          itbusina/apibee:latest \
            -c https://raw.githubusercontent.com/itbusina/apibee-public/main/examples/apis.json -l eyJdsa77b2FkIjp7IklkIjoiZ...
```

### Run collection from URL with authorization and required http headers.
```shell
docker run \
          itbusina/apibee:latest \
            -c https://api.github.com/repos/user/repo/contents/data/collection.json \
            -h "Authorization: Bearer ghp_dsa987dsad67d8s6a876d7as" "User-Agent:ApiBee" "Accept:application/vnd.github.raw+json" \
            -l eyJdsa77b2FkIjp7IklkIjoiZ...
```

### Run collection in parallel
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c data/collection.json -p -l eyJdsa77b2FkIjp7IklkIjoiZ...
```

### Run collection with multiple tags filter
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c data/collection.json -t smoke regression -l eyJdsa77b2FkIjp7IklkIjoiZ...
```

### Save report to output folder
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c data/collection.json -o output -l eyJdsa77b2FkIjp7IklkIjoiZ...
```

### Pass variables in collection
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c data/collection.json -v host=https://dummyjson.com -l eyJdsa77b2FkIjp7IklkIjoiZ...
```

### Pass secrets in collection
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c data/collection.json -s login=admin,password=Welcome1! -l eyJdsa77b2FkIjp7IklkIjoiZ...
```

### Run collection 5 times
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c data/collection.json -r 5 -l eyJdsa77b2FkIjp7IklkIjoiZ...
```

### Run collection 5 times with 1s delay
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c data/collection.json -r 5 -d 1000 -l eyJdsa77b2FkIjp7IklkIjoiZ...
```

### Run collection in loop with 5s interval without exit
```shell
docker run \
          -v ./:/app/data \
          itbusina/apibee:latest \
            -c data/collection.json -i 5000 -l eyJdsa77b2FkIjp7IklkIjoiZ...
```

## Integration with GitHub Actions
```yaml
name: CI Integration

on:
  push:
    branches: [ "main" ]
  workflow_dispatch:

jobs:
  tests:
    runs-on: ubuntu-latest
    
    steps:
    - uses: actions/checkout@v4
        
    - name: Execute API Requests
      run: |
        docker run \
          -v ./data:/app/data \
          -v ./output:/app/output \
          itbusina/apibee:latest \
            -c data/collection.json -p -o output -l eyJdsa77b2FkIjp7IklkIjoiZ...

    - name: Archive output results
      if: always()
      uses: actions/upload-artifact@v4
      with:
        name: report
        path: ./output/

```

# Features
- Local run from docker.
- Integration with GitHub.
- Filtering collection requests by tags.
- Collections configuration in the inline script, directory, file, public or authorized url. Multiple sources are also supported.
- Parallel/Sequential requests execution.
- Support of variables and secrets in the collection.
- Support of functions in the collection.
- Support dependencies between requests.
- Support using context between requests.
- Ability to execute multiple runs for the collection.
- Ability to specify the delay between runs.
- Ability to execute collection in the loop with interval.