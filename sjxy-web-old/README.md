# sjxy-web

Website of math&computer-science faculty.

## web

The `Modulars` folder contains the source code of the page components necessary for HTML files in the top-level directory, and the site components are not automatically embedded when previewing directly as html.

## CSTools

`CSTools` directory contains some tools written with DotNet Core.

### DevelopServer

`DevelopServer` is a ASP.Net Core server app that serves as a development server.

`cd` into its directory and hit `dotnet run` in command line, you will get it run.

Then visit

- `/index.html`
- `/list.html`
- `/list-single.html`
- `/post.html`
  to preview each page.

It does not cache anything. So all you need to do is to refresh in your browser after editting some files and you will get the new effect. Happy coding! 😘

Compile it with the latest [DotNet Core SDK](https://dotnet.microsoft.com).
