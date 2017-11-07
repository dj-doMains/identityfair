namespace WebApi
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using AutoMapper;
    using FluentValidation.AspNetCore;
    using Newtonsoft.Json.Serialization;
    using MediatR;
    using Swashbuckle.AspNetCore.Swagger;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore(options =>
            {
                options.Filters.Add(
                    new AuthorizeFilter(
                        new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build()));
            })
            .UsePaginableHeaders()
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
            })
            .AddApiExplorer()
            .AddFluentValidation(u => u.RegisterValidatorsFromAssemblyContaining<Startup>())
            .UsePaginableHeaders(u => u.UseExpanded());

            services.AddAuthorization();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:50000";
                    options.Audience = "api";
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                c.CustomSchemaIds(x => x.FullName);

                var schema = new OAuth2Scheme
                {
                    Type = "apiKey",
                    Description = "JWT token based authorization"
                };
                //schema.Extensions.Add("in", "header");
                //schema.Extensions.Add("name", "Authorization");
                //c.AddSecurityDefinition("api_key", schema);

                //c.DocumentFilter<SwaggerAuthorizationFilter>();
            });

            services.AddAutoMapper();
            Mapper.AssertConfigurationIsValid();

            services.AddMediatR();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}