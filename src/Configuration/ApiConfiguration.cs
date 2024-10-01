using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Domain.Helpers;

namespace Configuration;

/// <summary>
/// Class responsible for configuring API services and applications
/// </summary>
public static class ApiConfiguration
{
    #region Main

    /// <summary>
    /// Method responsible for configuring services
    /// </summary>
    /// <param name="builder">WebApplicationBuilder parameter</param>
    public static void Configure(WebApplicationBuilder builder)
    {
        ConfigureServices(builder.Services);
        ConfigureContexts(builder.Configuration);
    }

    /// <summary>
    /// Method responsible for configuring the application
    /// </summary>
    /// <param name="app">WebApplication parameter</param>
    public static void Configure(WebApplication app)
    {
        ConfigureApp(app);
    }

    #endregion

    #region Configuration

    /// <summary>
    /// Method responsible for configuring contexts
    /// </summary>
    /// <param name="configuration">IConfiguration parameter</param>
    private static void ConfigureContexts(IConfiguration configuration)
    {
        Domain.Helpers.Configuration.AddSettings(configuration);
    }

    /// <summary>
    /// Method responsible for configuring the application
    /// </summary>
    /// <param name="app">WebApplication parameter</param>
    private static void ConfigureApp(WebApplication app)
    {
        app.UseHttpsRedirection();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API - v1.0");
            c.RoutePrefix = string.Empty;
        });

        app.UseDeveloperExceptionPage();

        app.UseRouting();

        app.UseCors("CorsPolicy");

        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        //app.UseSession();
        app.Run();
    }

    /// <summary>
    /// Method responsible for configuring services
    /// </summary>
    /// <param name="services">IServiceCollection parameter</param>
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(config =>
        {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            config.Filters.Add(new AuthorizeFilter(policy));
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );
        });


        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "API - ",
                    Version = "v1",
                    Description = "",
                    Contact = new OpenApiContact
                    {
                        Name = "",
                        Email = ""
                    },
                });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = @"Bearer {token}.",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                 {
                     new OpenApiSecurityScheme
                     {
                         Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                     },
                     Array.Empty<string>()
                 }
            });
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Api.xml"));
        });
    }

    #endregion
}

