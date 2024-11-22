# Using functions in test collections

Testlemon has build-in functions to extend the tests collections.

## Overview

Use ```${{ func.<function name> }}``` to put a function result in the collection.

| Function Name                | Description |
| --------                     | -------     |
| ```${{ func.utcnow() }}```   | Returns curent UTC datetime.|
| ```${{ func.random() }}```   | Returns random number.|
| ```${{ func.guid() }}```     | Returns new GUID.|
| ```${{ gpt-4o.text(100) }}```| Returns text from OpenAI API (GPT-4o model in that case) with ```100``` tokens.|

Notes: make sure to specify the OpenAPI key and endpoint to use 'gpt-' function.

## Examples

### Basic functions
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

### LLM functions
Currently you can use OpenAI models in functions.

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
```