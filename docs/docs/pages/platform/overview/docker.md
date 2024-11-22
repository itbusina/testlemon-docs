# Docker image

Testlemon can be executed locally or integrated in a shell script with docker image.

## Overview
Docker image is available in docker hub: [![Docker Pulls](https://img.shields.io/docker/pulls/itbusina/testlemon)](https://hub.docker.com/r/itbusina/testlemon)

## Examples

### Run tests from test collection file
```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)"
```

### Run tests from collection from url
```shell
docker run itbusina/testlemon:latest -c https://raw.githubusercontent.com/itbusina/testlemon-docs/refs/heads/main/examples/quick-start.yaml
```