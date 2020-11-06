using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Pegler.PaymentGateway.BusinessLogic.Contracts;
using Pegler.PaymentGateway.BusinessLogic.Managers;
using Pegler.PaymentGateway.BusinessLogic.Options;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Pegler.PaymentGateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AuthenticationOptions>(Configuration.GetSection(AuthenticationOptions.OptionKey));
            services.Configure<EndpointOptions>(Configuration.GetSection(EndpointOptions.OptionKey));

            services.AddHttpClient("default");

            services.AddControllers()
                .AddJsonOptions(
                options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Payment Gateway Challenge API",
                        Description = "A set of APIs to interact with with the Challenge",
                        Contact = new OpenApiContact
                        {
                            Name = "Nicholas Pegler",
                            Email = string.Empty,
                            Url = new Uri("https://github.com/nicholaspegler/checkout")
                        }
                    });
                });

            services.AddAutoMapper(typeof(Startup));

            services.AddTransient<IHttpClientManager, HttpClientManager>();
            services.AddTransient<IPaymentManager, PaymentManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(
                options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment Gateway Challenge");

                    options.RoutePrefix = string.Empty;

                });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
