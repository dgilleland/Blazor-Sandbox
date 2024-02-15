# Web Sandbox

> A sandbox for playing with the basics of **Blazor** web applications and **bUnit**.

## Setup

```ps
dotnet new sln -o . -n WebIntro
dotnet new blazor -o Website -n WebApp -e
dotnet sln add Website/WebApp.csproj
dotnet new bunit -o Website.Specs -n WebApp.Specs -ta net8.0 --framework xunit
dotnet sln add Website.Specs/WebApp.Specs.csproj
cd Website.Specs
dotnet add reference ../Website/WebApp.csproj
```

## Testing

To run tests from withing Visual Studio Code, you need to a) perform a **build** (<kbd>ctrl</kbd> + <kbd>shift</kbd> + <kbd>b</kbd>) and then b) run the tests (<kbd>ctrl</kbd> + <kbd>;</kbd>, <kbd>a</kbd>).

> Note that if you simply try to run the tests before building, then the build is implied, but the tests will not necessarily be run right away if there were changes to the tests themselves (meaning you have to attempt the run a second time).

## Launching

To launch (i.e.: *run*) the web application from within Visual Studio Code, run the following from the terminal in VS Code.

```ps
cd Website
dotnet watch
```

At this point, the Blazor application will be compiled and launched by executing the application's `Program.cs` code. Note that the settings used for launching are in the `Website/Properties/launchSettings.json` file.
