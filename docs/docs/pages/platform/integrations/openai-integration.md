# How to add OpenAI API key to Testlemon

Testlemon’s is powered by OpenAI LLM capabilities. The integration is used in test collection functions and AI validators.

Note: The OpenAI fees are not included into the Testlemon license and will be charged to you based on your usage.

## Docker setup

Run docker image and pass openai api key in arguments.

```shell
docker run itbusina/testlemon:latest -c "$(<collection.json)" -openai-apikey "open-ai-api-key"
```

## SaaS portal setup

The openAI API key is set on profile page.

<img src="/images/integrations/openai/openai-1.png" alt="OpenAI api key integration in testlemon" width="800"/>