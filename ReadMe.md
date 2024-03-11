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

> It's recommended to launch the website from within the built-in terminal in VS Code rather than an external terminal. My reason for this is that, at present, the `dotnet watch` command cannot be exited from the external terminal with a simple <kbd>ctrl</kbd> + <kbd>c</kbd>. I'm not sure if this is by design, or is a bug. Rather, the whole terminal window must be shut down. However, from within the VS Code terminal, the <kbd>ctrl</kbd> + <kbd>c</kbd> shortcut correctly stops the web application and returns control back to the terminal.

While `dotnet watch` is running, you can continue with your development and any changes to your Blazor application will be automatically compiled and available in your browser. This is called **Hot Module Reload** (or *HMR*), and is one of the very helpful tools to keep developers focused on their work while observing the results. Sometimes, however, the nature of the changes to your code base will require a re-start of your application (such as adding a new *NuGet* dependency, which would change your `*.csproj`). When dotnet's HMR notices this, it will prompt you with the following in the terminal:

```ps
dotnet watch ⌚ File changed: .\WebApp.csproj.
  ❔ Do you want to restart your app - Yes (y) / No (n) / Always (a) / Never (v)?
```

> You can always force a re-build in `dotnet watch` by pressing **<kbd>ctrl</kbd> + <kbd>r</kbd>** to reload the terminal.

## Publish & Deploy to Docker

> *This portion presumes you already have **Docker Desktop** installed on your computer.*

One of the joys of deployment for web app developers these days is **Docker**. Since the release of .NET7, it's been possible to publish a deployment package directly from the CLI with `dotnet publish`. Here's an example (used for this project):

```ps
dotnet publish --os linux --arch x64 -c Release --sc -p:PublishProfile=DefaultContainer
```

This will create a release version (`-c Release`) of the application that will run on a 64-bit (`--arch x64`) version of Linux (`--os linux`). The application will be self-contained (`--sc`), meaning that the appropriate .NET runtime will be included in the application. Lastly, this will be bundled up into a docker image and added to the locally installed Docker instance (`-p:PublishProfile=DefaultContainer`).

> More information on these flags can be found in the official documentation on [`dotnet publish`](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-publish). You may also be interested in reading [Containerize a .NET app with dotnet publish](https://learn.microsoft.com/en-us/dotnet/core/docker/publish-as-container?pivots=dotnet-8-0).

When you run the `publish` command above, your output in the terminal should end with information on the resulting image that is similar to the following.

```ps
  Building image 'webapp' with tags 'latest' on top of base image 'mcr.microsoft.com/dotnet/runtime-deps:8.0'.
  Pushed image 'webapp:latest' to local registry via 'docker'.
```

Now, your local Docker instance will have an image named `webapp:latest`.

### Running Your Docker Image

Once you have a docker image, you can *run* it as a *container* in Docker. Again, this can be done from the command line using the `docker` CLI.

```ps
docker run -d --name app_in_a_box -p 54321:80 webapp:latest
```

### Adding A Shared Directory

Your docker containers run in an isolated environment within Docker, meaning that your docker-based application will not have access to the file system of your local machine (i.e.: your computer, which acts as the *host* that is running Docker). It is possible, however, to share a folder from your local computer, thereby making it accessible to your web application. In the following example, I am sharing a local folder that has a "ReadMe.md" file so that my Blazor application can read the contents.

```ps
--mount type=bind,source="/mnt/c/GH/SomePath/DevJournal",target="/dgilleland"
```
