---
description: "Get started with TestLemon quickly. Learn how to create API test collections, run Docker containers, set up monitoring, and execute automated tests with simple YAML or JSON configurations."
---

# Quick start

There are multiple options how to run test collection. Follow the steps below to create a collection and run the tests.

## Create a collection file with API tests to test.

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

## Run docker image
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)"
```

## Run the example collection url
```shell
docker run itbusina/testlemon:latest -c https://raw.githubusercontent.com/itbusina/testlemon-docs/refs/heads/main/examples/quick-start.yaml
```

## Display help
```shell
docker run itbusina/testlemon:latest --help
```

## Run collection from file. (option 1)
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)"
```

## Run collection from file. (option 2)

If you would like to set the path to the file, make sure the path is visible in the docker image, in order to do that, mount a docker volume.

```shell
docker run -v ./:/app/data itbusina/testlemon:latest -c data/collection.json
```

## Specify the license
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" -l $license
```

## Run collection inline.
```shell
$collection = @'
name: Dummy JSON collection
baseUrl: https://dummyjson.com
tests:
  - url: /users/1
'@

docker run itbusina/testlemon:latest -c $collection
```

## Run collections from multiple files
```shell
docker run -v ./:/app/data itbusina/testlemon:latest -c data/collection1.json data/collection2.json
```

## Run collections from directory
```shell
docker run -v ./:/app/data itbusina/testlemon:latest -c data/
```

## Run collection from URL.
```shell
docker run itbusina/testlemon:latest -c https://raw.githubusercontent.com/itbusina/testlemon-docs/refs/heads/main/examples/quick-start.yaml
```

## Run collection from URL with authorization and required http headers.
```shell
docker run itbusina/testlemon:latest -c https://api.github.com/repos/user/repo/contents/data/collection.json -h "Authorization: Bearer ghp_dsaxxxxxxyyyyyyyzzzzz" "User-Agent:testlemon" "Accept:application/vnd.github.raw+json"
```

## Run collection in parallel
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" -p
```

## Run collection with multiple tags filter
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" -t smoke regression
```

## Save report to file
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" -o output.json
```

## Save report to file in JSON format
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" -o output.json -f json
```

## Save report to file in Xray format
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" -o output.json -f xray
```

## Pass variables in collection
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" -v host=https://dummyjson.com
```

## Pass secrets in collection
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" -s login=admin,password=Welcome1!
```

## Run collection 'N' times
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" -r 5
```

## Run collection 'N' times with 1s delay
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" -r 5 -d 1000
```

## Run collection in loop with 'X' interval without exit
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" -i 5000
```

## Display details about tests and responses 
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" --verbose
```

## Save details about tests and responses to the file
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" --verbose > output.json
```

## Display and save details about tests and responses to the file
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" --verbose | tee output.json
```