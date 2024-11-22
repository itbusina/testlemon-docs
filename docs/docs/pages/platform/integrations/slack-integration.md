# How to integrate Testlemon in GitHub pipeline

Testlemon’s Slack integration enables teams to receive instant notifications about issues and test failures, ensuring timely awareness and quicker resolution.

## How to setup

1. Go to your slack apps: [https://api.slack.com/apps](https://api.slack.com/apps)
2. Open Incoming Webhooks section
<br/>
<img src="/images/integrations/slack/slack-1.png" alt="Incoming webhooks configuration in slack" width="800"/>

3. Click on "Add New Webhook to Workspace" button in the bottom
4. Select the channel for incoming messages
<br/>
<img src="/images/integrations/slack/slack-2.png" alt="Select slack channel for incoming webhook messages" width="800"/>

5. Click "Allow" button.
6. Find the created webhook URL and copy it
<br/>
<img src="/images/integrations/slack/slack-3.png" alt="Slack webhook URL" width="800"/>

7. Go to Testlemon SaaS portal to project settings
8. Pass the copied webhook URL to the advance configuration section.
<br/>
<img src="/images/integrations/slack/slack-4.png" alt="Slack webhook URL in Testlemon project configuration" width="800"/>

When everything is done properly, you will recieve the notifications about failures in that Slack channel.