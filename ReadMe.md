# Web Sandbox

> A sandbox for playing with the basics of **Blazor** web applications and **bUnit**.

## Setup

```ps
dotnet new sln -o . -n WebIntro
dotnet new blazor -o Website -n WebApp -e
dotnet sln add Website/WebApp.csproj
dotnet new bunit -o Website.Specs -n WebApp.Specs -ta net8.0 --framework xunit
```
