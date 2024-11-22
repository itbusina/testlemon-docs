# Test validators

Test Validators used to validate the correctness, completeness, and quality of a system or component against predefined requirements or standards. They ensure that the software behaves as expected and meets the intended purpose.

Testlemon has build-in validators to validate APIs and monitor website or domains.

## Overview

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

## Examples

### Validate Http Status Code
```yaml
name: Validate Http Status Code
baseUrl: https://dummyjson.com
tests:
- url: "/auth/login"
  validators:
  - status-code: '403'
```

### Validate Http Body (Full match)
```yaml
name: Validate Http Body (Full match)
baseUrl: https://dummyjson.com
tests:
- url: "/auth/login"
  validators:
  - body-equals: '{"id":1,"body":"This is some awesome thinking!","postId":100,"user":{"id":63,"username":"eburras1q"}}'
```

### Validate Http Body (Contains)
```yaml
name: Validate Http Body (Contains)
baseUrl: https://dummyjson.com
tests:
- url: "/auth/login"
  validators:
  - body-contains: This is some awesome thinking!
```

### Validate Response time
```yaml
name: Validate Http Body (Contains)
baseUrl: https://dummyjson.com
tests:
- url: "/auth/login"
  validators:
  - response-time: 1000
```

### Validate Response Body Sentiment
```yaml
name: Validate Sentiment
baseUrl: https://dummyjson.com
tests:
- url: "/comments?limit=1&select=body"
  validators:
  - sentiment: positive
```