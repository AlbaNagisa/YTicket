# YTicket Client

## Description

YTicket client is a new way to manage your helping tickets. It is a simple and easy to use application that allows you
to answer tickets. It also allows you to view all the tickets that people have created on different platform like
discord telegram... to ask your help.

## Requirements

- .NET 8.0
- Windows / Linux or MacOS
- A YTicket account
- Telegram or Discord account

## Installation

```bash
git clone https://github.com/YTicket-Company/YTicket-Client
cd YTicket-Client
```

## Run in development

```bash
dotnet restore
dotnet run Program.cs
```

## Build for Windows

```bash
dotnet restore
dotnet publish -c Release -r win-x64 --self-contained
```

## Build for Linux

```bash
dotnet restore
dotnet publish -c Release -r linux-x64 --self-contained
```

## Differents Design patterns used

- Singleton
- Factory
- Observer
- Polymorphism
- Pooling

## Invite Link for the discord bot

- [Invite the Discord bot](https://discord.com/api/oauth2/authorize?client_id=1198921038986215555&permissions=8&scope=bot%20applications.commands)  
- [Invite the Telegram bot](https://t.me/YTicketBot)

## Authors

- [AlbaNagisa](https://github.com/albanagisa)
- [MazBaz](https://github.com/MazbazDev)
- [Envel](https://github.com/envel69)
