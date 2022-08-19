# WebhooksTutorial

Content created during the course Webhooks with .NET5 on Udemy: https://www.udemy.com/course/webhooks-with-dotnet-5/

![image](https://user-images.githubusercontent.com/110463777/185569358-17de742e-9220-479c-bc3d-a7790059a5f2.png)

In this hands-on, "no fluff / no filler" course we take a practical approach to building a working solution that uses Webhooks. We'll cover some theory to start, but very quickly we jump into the practical step by step solution build which forms the vast majority of the course.

During the build, we will create 3 separate .NET Projects to simulate a fictional airline and its travel-agent customers. This approach will give students a real-world grounding in the use of webhooks and the value they bring to industry. The projects we build are:

Airline Web

- Webhook Registration REST API
- Flight Details REST API - used to trigger webhook by publishing to RabbitMQ message bus
- Simple HTML / JavaScript / Bootstrap Web Client to make webhook registration API calls

Airline Send Agent
- Stand alone "agent" used to send webhooks "en-mass"
- Dependency Injection enabled
- RabbitMQ Subscriber / Consumer with event based message delivery
- Uses HttpClient and HttpClientFactory

Travel Agent Web
- Simple Webhook POST Endpoint
- Uses SQL Server backend to retrieve webhook "secret" to authenticate webhooks

We also use Docker Compose to set up and run following solution fabric:
- RabbitMQ Server
- Microsoft SQL Server

Students should be aware that we use VSCode as the development tool of choice, so students wanting to learn with Visual Studio should consider this carefully before purchasing.

Source Code is downloadable as a Lecture Resource.

Slideware is downloadable as a Lecture Resource
