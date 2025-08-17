---
description: "Configure and manage variables in TestLemon for dynamic testing. Learn how to use environment variables, test data, and parameterized testing to create flexible and reusable test collections."
---

# Using variables in test collection

Variables are used to define predefined values once and reuse them multiple times throughout a project. They are ideal for storing environment-specific values, URLs, default names, and similar data. However, it is strongly recommended **not** to use variables for storing secrets, passwords, or other sensitive information to maintain security best practices.

## Define variables

Use ```${{ vars.<variable name> }}``` to put a variable in the collection.

```yaml
name: Collection with variables
baseUrl: "${{ vars.host }}"
tests:
- url: "/products"
```

## Pass variables
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" -v host=https://dummyjson.com
```