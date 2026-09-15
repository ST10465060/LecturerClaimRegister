# Lecturer Claim Register

An ASP.NET Core MVC web application that allows lecturer claims to be captured,
validated and listed, with the same data exposed through a RESTful JSON endpoint.

Built for PROG6212 ICE Task 3. This project introduces the structure that will
later be expanded into the Contract Monthly Claim System (CMCS).

---

## Technologies

- ASP.NET Core MVC (.NET 8)
- C#
- Razor views with tag helpers
- Bootstrap 5
- Swashbuckle (Swagger) for API documentation

---

## Understanding MVC in this application

MVC separates an application into three responsibilities. Keeping them apart
means a change to how data looks does not force a change to how data is stored
or how requests are handled.

### Model

The Model holds the data and the rules that data must follow. In this project
`Models/Claim.cs` defines a claim's properties and carries the validation
attributes that enforce them — a lecturer name and module code are required,
hours must fall between 1 and 160, and the hourly rate must be greater than
zero. It also exposes a calculated `TotalAmount` property that multiplies hours
by rate on every read, so the total can never drift out of step with the values
it is derived from. `Models/ClaimStore.cs` acts as the in-memory data source.

### View

The View is what the user actually sees. `Views/Claims/Index.cshtml` renders the
claim list as a table with currency-formatted totals and colour-coded status
badges, while `Views/Claims/Create.cshtml` renders the capture form. Views
contain presentation logic only — they display the model they are handed and do
not decide what that model should contain.

### Controller

The Controller receives the incoming request, works with the model and chooses
what to send back. `Controllers/ClaimsController.cs` handles the web workflow:
`Index` passes the claim list to its view, the GET `Create` action returns an
empty form, and the POST `Create` action checks `ModelState.IsValid` before
saving. Valid claims are added to the store and the user is redirected to the
list; invalid claims are returned to the form with their input intact and the
validation messages displayed.

---

## Two useful ASP.NET Core features

### 1. Model binding with data annotation validation

ASP.NET Core automatically maps posted form fields onto a strongly typed object
by matching names, then evaluates the data annotation attributes declared on
that model. A single `[Range(1, 160)]` attribute on `HoursWorked` produces both
the server-side check exposed through `ModelState.IsValid` and the client-side
`data-val` attributes that the jQuery validation scripts read. One declaration
covers both layers, which removes the risk of the rules drifting apart and keeps
the controller free of manual parsing and if-statements.

### 2. Razor tag helpers

Tag helpers let server-side behaviour be attached to ordinary HTML elements.
`<input asp-for="HourlyRate" />` generates the correct `name`, `id`, current
value and validation attributes from the model property, `<label asp-for="..." />`
pulls its text from the `[Display]` attribute, and `<a asp-action="Index">`
builds its URL from the routing table rather than a hardcoded string. Because
the markup stays valid HTML, it remains readable, and route or property renames
are reflected automatically instead of silently breaking links.

---

## Features

- Claim list showing lecturer, module, month, hours, rate, total and status
- Create form with full client-side and server-side validation
- Totals displayed in currency format, plus a grand total row
- New claims default to a `Draft` status, enforced server-side
- RESTful `GET api/claims` endpoint returning all records as JSON
- `GET api/claims/{id}` for retrieving a single claim
- Swagger UI available in development for testing the API

---

## Running the application

1. Clone the repository.
2. Open `LecturerClaimRegister.slnx` in Visual Studio 2022.
3. Press `Ctrl+F5` to run without debugging.
4. The application opens on the claim register, seeded with two sample claims.

### Testing the API

Endpoint	Description
GET /api/claims	Returns all claims as JSON
GET /api/claims/{id}	Returns a single claim, or 404 if not found

### Project structure

```
LecturerClaimRegister/ ├── Controllers/ │ ├── ClaimsController.cs # MVC actions: Index, Create (GET/POST) │ ├── ClaimsApiController.cs # RESTful endpoint on api/claims │ └── HomeController.cs ├── Models/ │ ├── Claim.cs # Claim entity with validation │ ├── ClaimStore.cs # Static in-memory data store │ └── ErrorViewModel.cs ├── Views/ │ ├── Claims/ │ │ ├── Index.cshtml # Claim list │ │ └── Create.cshtml # Capture form │ └── Shared/_Layout.cshtml # Navigation └── Program.cs # Service registration and routing
```
###References

Microsoft, 2024. Overview of ASP.NET Core MVC. [online]
Available at: https://learn.microsoft.com/en-us/aspnet/core/mvc/overview

Microsoft, 2024. Tag Helpers in ASP.NET Core. [online]
Available at: https://learn.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/intro

Microsoft, 2024. Model validation in ASP.NET Core MVC. [online]
Available at: https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation

Troelsen, A. and Japikse, P., 2022. Pro C# 10 with .NET 6: Foundational
Principles and Practices in Programming. 11th ed. Berkeley: Apress.

W3Schools, 2024. C# Tutorial. [online]
Available at: https://www.w3schools.com/cs/index.php
