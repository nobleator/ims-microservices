using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;

namespace transaction_service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            // AppSecrets.json is used for microservice-specific debugging, env vars for Heroku and Docker use
            var Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSecrets.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            string dbServerName;
            int portNumber;
            string databaseName;
            string databaseUsername;
            string databasePassword;

            if (Configuration.GetChildren().Any(item => item.Key == "IMS_DBHOST"))
            {
                dbServerName = Configuration["IMS_DBHOST"];
                portNumber = int.Parse(Configuration["IMS_DBPORT"]);
                databaseName = Configuration["IMS_DBNAME"];
                databaseUsername = Configuration["IMS_DBUSER"];
                databasePassword = Configuration["IMS_DBPASS"];

                // Docker support
                if (dbServerName == "localhost" && Configuration["DOTNET_RUNNING_IN_CONTAINER"] == "true")
                {
                    Console.WriteLine("Resetting dbServerName for Docker service");
                    dbServerName = "db";
                }
            }
            else
            {
                var databaseUrl = Configuration["DATABASE_URL"];
                var databaseUri = new Uri(databaseUrl);
                var userInfo = databaseUri.UserInfo.Split(':');

                dbServerName = databaseUri.Host;
                portNumber = databaseUri.Port;
                databaseUsername = userInfo[0];
                databasePassword = userInfo[1];
                databaseName = databaseUri.LocalPath.TrimStart('/');
            }

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = dbServerName,
                Port = portNumber,
                Username = databaseUsername,
                Password = databasePassword,
                Database = databaseName
            };

            DbConnectionString = builder.ToString();
            MapboxToken = Configuration["MapboxToken"];
        }

        public IConfiguration Configuration { get; }

        public static string DbConnectionString { get; set; }

        public static string MapboxToken { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
