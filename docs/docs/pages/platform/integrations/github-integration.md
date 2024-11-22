# How to integrate Testlemon in GitHub pipeline

Testlemon has GitHub actions integration for seamless integration to your build pipeline.

## Examples

### Run tests in pipeline using docker image
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

### Run tests in pipeline using github actions
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

### Test API before deployment to production
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