# How to integrate Testlemon in any CI/CD pipeline

Testlemon has a docker image and can be integrated in any bash script and any CI/CD pipeline.

## Examples

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