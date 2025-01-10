using ContactManagement.Api.Filters;
using ContactManagement.Api.Middlewares;
using ContactManagement.Application.Dtos;
using ContactManagement.Application.Validators;
using FluentValidation.AspNetCore;
using System.Net;
using System.Text.Json;

namespace ContactManagement.Api.Configurations;
public static class ApiConfig
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ValidateModelStateFilter>();
        })
        .AddFluentValidation(config =>
        {
            config.RegisterValidatorsFromAssemblyContaining<ContactDtoValidator>();
        });

        services.AddEndpointsApiExplorer();

        services.AddCors(options =>
        {
            options.AddPolicy("Total",
                builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("X-Pagination"));
        });

        return services;
    }

    public static void UseApiConfiguration(this WebApplication app, IWebHostEnvironment env)
    {
        //if (app.Environment.IsDevelopment())
        //{
            app.UseSwaggerConfig();
        //}

        app.UseMiddleware<ExceptionMiddleware>();
        app.UseCustomStatusCodePages();
        app.UseHttpsRedirection();
        app.UseCors("Total");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }

    private static void UseCustomStatusCodePages(this WebApplication app)
    {
        app.UseStatusCodePages(async context =>
        {
            var response = context.HttpContext.Response;

            if (response.StatusCode == (int)HttpStatusCode.Unauthorized ||
                response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                response.ContentType = "application/json";

                var errorResponse = new ErrorResponse
                {
                    StatusCode = response.StatusCode,
                    Errors = new List<string> { "Access denied. Token missing or invalid." }
                };

                await response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            }
        });
    }
}
