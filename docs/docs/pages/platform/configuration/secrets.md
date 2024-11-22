# Using secrets in test collection

Secrets are used to securely store sensitive information such as API keys, passwords, or tokens that are essential for application functionality. It is crucial to handle secrets with care by using dedicated secret management tools or encrypted storage mechanisms. Avoid hardcoding secrets in your codebase or storing them in plain text files to prevent unauthorized access and ensure compliance with security best practices.

## Define secrets

Use ```${{ secrets.<secret name> }}``` to put a secret in the collection.

```yaml
name: Collection with secrets
baseUrl: https://dummyjson.com
tests:
- url: "/auth/login"
  method: POST
  body: '{"username":"${{ secrets.login }}","password":"${{ secrets.password }}"}'
```

## Pass secrets
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" -s login=admin,password=Welcome1!
```