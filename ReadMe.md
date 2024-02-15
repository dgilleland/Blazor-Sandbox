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

If you prefer running the tests from the terminal, you may find that using an external terminal (rather than VS Code's built-in terminal) is easier to work with because you can readily keep that terminal maximized which makes it easier to read details about failing tests; all you would need to do is <kbd>alt</kbd> + <kbd>tab</kbd> to switch between VS Code and the terminal.

From within the terminal, you can build/run the tests each time with `dotnet test`.

> **Note on `dotnet watch test`:** Prior to the recent release of .NET 8, running tests in watch mode listed the results of the tests every time the code changed. This is broken at present, but is expected to be fixed in an upcoming update.

## Launching

To launch (i.e.: *run*) the web application from within Visual Studio Code, run the following from the terminal in VS Code.

```ps
cd Website
dotnet watch
```

At this point, the Blazor application will be compiled and launched by executing the application's `Program.cs` code. Note that the settings used for launching are in the `Website/Properties/launchSettings.json` file.

> It's recommended to launch the website from within the built-in terminal in VS Code rather than an external terminal. The reason for this is that, at present, the `dotnet watch` command cannot be exited from the external terminal with a simple <kbd>ctrl</kbd> + <kbd>c</kbd>. Rather, the whole terminal window must be shut down. However, from within the VS Code terminal, the <kbd>ctrl</kbd> + <kbd>c</kbd> shortcut correctly stops the web application and returns control back to the terminal.
