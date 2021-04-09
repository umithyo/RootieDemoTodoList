[![.NET](https://github.com/umithyo/RootieDemoTodoList/actions/workflows/dotnet.yml/badge.svg)](https://github.com/umithyo/RootieDemoTodoList/actions/workflows/dotnet.yml)

### RootieDemoTodoList

This program is a todo-list demo made for my intervew with [Rootie](https://www.rootielearning.com)

It is a Web API that carries the purpose of receiving and processing todo items in a way that the processing runs in the background via Hangfire. And when the given schedule has been met, the program will enqueue a mailing (mock) process with RabbitMQ.

# Table of contents

1.  [How to run](#how-to-run)
2.  [Try it out](#try-it-out)
3.  [Used technologies](#used-technologies)
4.  [References](#references)

# How to run

- Install Docker or Docker Desktop (for Windows OS)

- Start powershell, command-line or your integrated terminal in the root of the project

- Manage your certificates (**NOTE: you may skip this step if you already know your dev cert password**)

	- **For Windows:**
		- The following will need to be executed from your terminal to create a cert
		- `dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p Your_password123`
		- `dotnet dev-certs https --trust`

	**NOTE: When using PowerShell, replace %USERPROFILE% with $env:USERPROFILE.**

	 - **For macOS:**

		- `dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p Your_password123`

		- `dotnet dev-certs https --trust`

	- **For Linux:**

		- `dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p Your_password123`

- Edit docker-compose.yml in the root of the project, and change "Your_password123" with the one you've provided above.

-  `docker-compose up -d`

- When everything is done, you should be good to visit the app at `https://localhost:5001`

# Try it out

- Open the app via browser at `https://localhost:5001/swagger`

- You should be able to see all the available controllers with the API testing tools.

# Used technologies

- .NET Core (Web API)

- PostgreSQL

- Hangfire

- Docker

- RabbitMQ

# References:
- [Jason Taylor's Clean Architecture](https://github.com/jasontaylordev/CleanArchitecture)
- [RabbitMQ Docker](https://hub.docker.com/_/rabbitmq)
- [PostgreSQL Docker](https://hub.docker.com/_/postgres)
- [Hangfire](https://www.hangfire.io/overview.html)
- [Mediator Wiki Page](https://en.wikipedia.org/wiki/Mediator_pattern)
- [TimeZone Ids](https://stackoverflow.com/questions/7908343/list-of-timezone-ids-for-use-with-findtimezonebyid-in-c)
