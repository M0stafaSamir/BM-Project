
using JopApplication.Core.Interfaces.Repositories;
using JopApplication.Core.Models;

using JopApplication.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication;

using JopApplication.Infrastructure.DBContexts;
//using JopApplication.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using JopApplication.Infrastructure.DBContext;
using MediatR;
using JopApplication.Services.Mappers;
using Serilog;
using JopApplicationMS.API.Middlewares;
using JopApplicationMS.API.Filters;
using Microsoft.AspNetCore.Mvc;
using JopApplication.Core.Responses;
using JopApplication.Core.Interfaces.Services;
using JopApplication.Services.Services;
using Microsoft.AspNetCore.Diagnostics;
using JopApplication.Services.Commands.Job;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JopApplicationMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add CORS service
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200")
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    });
            });


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            ////Register the IdentityDbContext "For Identity Management"
            builder.Services.AddDbContext<UserIdentityDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<UserIdentityDbContext>();

            ////Register the JopDbContext "For DB First"
            builder.Services.AddDbContext<JobAppDbContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

            builder.Host.UseSerilog();


            //Mediator 
            builder.Services.AddMediatR(typeof(CreateJobCommand).Assembly);


            //Register the Generic Interface
            //builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //Register Repos 
            builder.Services.AddScoped<IJobRepository, JobRepository>();
            builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();

            //Register Services
            builder.Services.AddScoped<IAuthService,AuthService>();


            //Automapper
            builder.Services.AddAutoMapper(typeof(JobProfile).Assembly);


            // JWT Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = builder.Configuration["Jwt:Issuer"],
                   ValidAudience = builder.Configuration["Jwt:Audience"],
                   IssuerSigningKey = new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
               };
           });


            // General validation filter
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    var response = ApiResponse<string>.FailResponse("Validation failed", errors);
                    return new BadRequestObjectResult(response);
                };
            });





            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exceptionHandler = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandler?.Error;
                    Log.Error(exception, "Unhandled exception");
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("An unexpected error occurred.");
                });
            });
            app.UseCors("AllowSpecificOrigin");

            app.UseMiddleware<RequestLoggingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();



            app.MapControllers();



            app.Run();
        }
    }
}
