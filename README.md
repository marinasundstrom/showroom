# Showroom

Web portal for presenting consultants to customers.

It consists of a frontend built with Blazor (Web Assembly), and a Web API (ASP.NET Core).

Watch demo video [here](https://www.youtube.com/watch?v=bRMFgDmwXmM). Screenshots can be found below.

## Purpose

This was made as a side-project when being between assignments.

I was testing different ways of organizing code. One of my influences was Clean Architecture.

## Specification

This is the functionality available seen from the user roles:

**Managers** can:
* Manage Consultants and Clients.
* Present Consultants to Clients

**Clients** can:
* View Consultants that have been presented to them.
* Respond to a presentation ("Show iterest").

**Consultants** can:
* Edit their own profiles.

## Notes

Consultants and Managers are real persons, some of them who don't work for the company anymore.

The names of clients and their work positions are entirely made up.

## Screenshots

### Login

<img src="/Screenshots/Login.png" />

### Gallery

<img src="/Screenshots/Gallery.png" />

### Consultant Profile

<img src="/Screenshots/ConsultantProfile.png" />

### Present consultant to client

<img src="/Screenshots/PresentConsultant.png" />

## Development

### Tools

* .NET Core 5.0.2
* Node.js 15.8.0 (or later)
* Visual Studio 2019 (or later)

#### Recommended
* Docker
* Project Tye

### Build instructions

Install the required tools.

Install NPM dependencies:

```
npm install
```

From Client project directory

```
npm link ../../Brand
```

Build the solution:

```
dotnet build
```

### Using Tye

Make sure that Docker Desktop is running.

Run this when in the root directory (where the tye.yaml is situated):

```
tye run
```
