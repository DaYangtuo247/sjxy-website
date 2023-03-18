using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Text;

namespace DevelopServer
{
    public class Startup
    {
        public static string FindProjectRoot()
        {
            var path = typeof(Program).Assembly.Location;
            while (Path.GetFileName(path) != "sjxy-web-old")
            {
                try
                {
                    path = Path.GetDirectoryName(path);
                }
                catch (Exception)
                {
                    Console.Error.WriteLine("Failed to find the 'sjxy-web-old' directory (project root) from all the ancestor directory of this assembly.");
                    Environment.Exit(1);
                }
            }
            return path;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var projectRoot = FindProjectRoot();

            var webRoot = Path.Combine(projectRoot, "web");

            var moduleResolver = new ModuleResolver(Path.Combine(webRoot, "Modulars"));

            void UseStaticFiles(string dir)
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(webRoot, dir)),
                    RequestPath = "/" + dir,
                });
            }

            UseStaticFiles("js");
            UseStaticFiles("css");
            UseStaticFiles("imgs");

            app.Map("/apic", (builder) =>
            {
                builder.Run(async (context) =>
                {
                    var image = await File.ReadAllBytesAsync(Path.Combine(env.ContentRootPath, "apic.jpg"));
                    context.Response.ContentType = "image/jpeg";
                    context.Response.ContentLength = image.Length;
                    await context.Response.Body.WriteAsync(image);
                });
            });

            void MapPage(string pageName, string fileName)
            {
                app.Map(pageName, builder =>
                {
                    builder.Run(async (context) =>
                    {
                        var s = PageGenerator.Generate(webRoot, fileName, moduleResolver);
                        var data = Encoding.UTF8.GetBytes(s);
                        context.Response.ContentType = "text/html";
                        context.Response.ContentLength = data.Length;
                        await context.Response.Body.WriteAsync(data);
                    });
                });
            };

            MapPage("/index.html", "index.html");
            MapPage("/list.html", "list.html");
            MapPage("/list-single.html", "list-single.html");
            MapPage("/post.html", "post.html");
            MapPage("/teachers.html", "teachers.html");
            MapPage("", "index.html");
        }
    }
}
