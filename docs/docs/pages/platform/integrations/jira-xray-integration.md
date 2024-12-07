# How to integrate Testlemon with XRay for JIRA

Testlemon’s Jira XRay integration enables teams to import Testlemon tests results to XRay and track test executions and results in Jira

## Important
Currently Jira XRay integration is available only with Testlemon Docker CLI tool.

## How to setup Testlemon
Testlemon integrates with Jira XRay using XRay import [REST API](https://docs.getxray.app/display/XRAY/Import+Execution+Results+-+REST#ImportExecutionResultsREST-XrayJSONresults)

To generate the test results output in XRay format use ```-f xray``` flag.

```bash
docker run itbusina/testlemon:latest -c "$(<collection.json)" -o output.json -f xray
```

## Automatic XRay tests provisioning
<img src="/images/integrations/xray/xray-auto-provisioning.png" alt="Jira XRay automatic provisioning with Testlemon" width="800"/>

1. Implement Testlemon test collection.
2. Optionally put the reference to the Jira issues (ex, Story, Epic, Task, etc. ) using issue keys.
3. Execute tests and generate the test results in XRay format ```-f xray```.
4. Report execution results using the Jira XRay REST API and create our update existing Tests in Xray.

In the first iteration, Tests will be automatically created in Xray and the Generic Test Definition field will act as the test identifier.
In second and onward iterations, existing Tests will be updated; Xray will use the Generic Test Definition field as the test identifier in order to find the existing Test in JIRA.

Example:

```yaml
name: Auto XRay provisioning collection
baseUrl: https://dummyjson.com
metadata:
  - execution-description: "Auto XRay provisioning test execution"
    revision: 1
    version: 1.0.0
    test-plan-key: "XSP-88"
    environments: "DesktopChrome"
tests:
  - url: /products
    name: Get all products
    metadata:
      - project-key: XSP
        definition: Validate that GET all products API returns 200 OK status
  - url: /products/1
    name: Get product by ID=1
    metadata:
      - project-key: XSP
        definition: Validate that GET product by ID API returns 200 OK status
```

After Tests are created in Jira, they can be managed in Jira. Thus, users can add additional information to them, associate them with other entities (e.g. requirements, Test Sets, etc)..

## Manual XRay tests provisioning
<img src="/images/integrations/xray/xray-manual-provisioning.png" alt="Jira XRay manual provisioning with Testlemon" width="800"/>

1. Create a Generic Test in Jira.
    - The test will be uniquely identified by the issue key; however, the Generic Test Definition custom field may be used as a more friendly way to identify the test.
    - The test can be automatically created in Jira when importing test results; as mentioned in the previous point, the Generic Test Definition field acts as the test identifier.
2. Implement the Testlemon test collection, store it in the source control system, and put the reference to the Test in Jira (i.e., the issue key).
3. Execute tests in the CI environment.
4. Report execution results using Xray the REST API.

Example:

```yaml
name: Manual XRay provisioning collection
baseUrl: https://dummyjson.com
metadata:
  - execution-description: "Manual XRay provisioning test execution"
    revision: 1
    version: 1.0.0
    test-plan-key: "XSP-88"
    environments: "DesktopChrome"
tests:
  - url: /users
    metadata:
      - test-key: XSP-85
  - url: /users/1
    metadata:
      - test-key: XSP-86
```

## Test collection Metadata fields
```metadata``` fields on test collection level and test level are used to add aditional information to XRay report.

Description:

```yaml
name: # test collection name
baseUrl: # base URL
metadata:
  - execution-description: # tests execution description
    execution-summary: # tests execution summary
    revision: # tests execution revision (ex. 1)
    version: # tests version. Should correspond to Jira fix version (ex. 1.0.0)
    test-plan-key: # issue key to test plan (ex. PBI-50)
    environments: # array of XRay test environments (ex. Chrome, IOS, Android)
tests:
  - url: # url for testing
    method: # http method
    id: # test id
    name: # test name
    metadata:
      - test-key: # XRay test issue key (used in case of manual test provisioning, ex. PBI-100)
        project-key: # Jira project key (used in case of automatic test provisioning, ex. PBI)
        requirement-keys: # array of requirements to link the test (ex. PBI-1, PBI-2, PBI-10)
        summary: # test summary
        definition: # test definition
```

Notes:

1. If ```execution-summary``` is not specified, test collection ```name``` will be used for execution summary.
2. If ```execution-description``` is not specified, test collection ```name``` will be used for execution summary.
3. If ```summary``` is not specified on the test level, the first not empty value will be used from the test: ```name```, ```id```, ```http method + url```
4. If ```definition``` is not specified on the test level, the first not empty value will be used from the test: ```name```, ```id```, ```http method + url```

## CI/CD integration
When Testlemon tests report is ready, it can be imported to XRay cloud or server versions using XRay REST API. It can be done on any CI/CD tools which support scripting (bash or powershell).

Here is a GitHub example how to import Testlemon report to Jira XRay:

```yaml
name: XRay automatic tests provisioning

on:
  workflow_dispatch:

jobs:
  build:
    runs-on: 'ubuntu-latest'

    steps:
    - name: Code checkout
      uses: actions/checkout@v4

    - name: Run tests with auto provisioning
      run: |
        docker run --name testlemon itbusina/testlemon:latest -c "$(<./examples/xray/auto-xray-provisioning.yaml)" -o output.json -f xray
        docker cp testlemon:/app/output.json output.json
        docker rm testlemon

    - name: Upload test results to XRay
      shell: pwsh
      run: |
        $client_id = "${{ secrets.XRAY_CLIENT_ID }}"
        $client_secret = "${{ secrets.XRAY_CLIENT_SECRET }}"
        $jira_base_url = "${{ secrets.XRAY_JIRA_BASE_URL }}"
        $test_results = Get-Content "output.json"
        ./scripts/xray-cloud-import.ps1 -client_id $client_id -client_secret $client_secret -jira_base_url $jira_base_url -test_results $test_results
        
```

Pipeline example can be found [here](https://github.com/itbusina/testlemon-docs/blob/main/.github/workflows/xray-auto.yml)

Test collection example can be found [here](https://github.com/itbusina/testlemon-docs/blob/main/examples/xray/auto-xray-provisioning.yaml)

## Step by step tutorial
Visit [How to import tests to Jira XRay](/pages/learning/testing/how-to-import-test-results-to-jira-xray) tutorial to see how to configure Testlemon reporting to Jira Xray.