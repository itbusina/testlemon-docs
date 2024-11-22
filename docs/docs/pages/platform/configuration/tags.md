# Using variables in test collection

Tags are used to categorize and filter tests within a test collection, making it easier to organize and execute specific subsets of tests. By assigning tags to tests, you can run targeted test groups based on criteria such as functionality, priority, or environment. This approach streamlines test management and enhances the efficiency of the testing process.

## Define tags

Tags are used to filter tests.

```yaml
name: Collection with Tags
baseUrl: https://dummyjson.com
tests:
- url: "/users/1" 
  tags:
    - smoke
    - regression
```

## Pass tags
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" -t smoke regression
```