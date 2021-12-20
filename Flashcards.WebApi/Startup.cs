using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flashcards_backend.Core.IServices;
using Flashcards.DataAccess;
using Flashcards.DataAccess.Repositories;
using Flashcards.Domain.IRepositories;
using Flashcards.Domain.Services;
using Flashcards.Security;
using Flashcards.Security.Helpers;
using Flashcards.Security.IServices;
using Flashcards.Security.Models;
using Flashcards.Security.Repositories;
using Flashcards.Security.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Flashcards.WebApi
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
            //  var value = Configuration["JwtConfig:secret"];
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Flashcards.WebApi", Version = "v1"});
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using Bearer scheme"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            services.AddScoped<IDeckService, DeckService>();
            services.AddScoped<IDeckRepository, DeckRepository>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<IAttemptService, AttemptService>();
            services.AddScoped<IAttemptRepository, AttemptRepository>();

            services.AddDbContext<MainDbContext>(options => { options.UseSqlite("Data Source=main.db"); });


            services.AddAuthentication(authenticationOptions =>
                {
                    authenticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authenticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(Configuration["Jwt:Secret"])),
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = Configuration["Jwt:Audience"],
                        ValidateLifetime = true
                    };
                });
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();

            services.AddDbContext<SecurityContext>(options => { options.UseSqlite("Data Source = auth.db"); });

            services.AddCors(options =>
            {
                options.AddPolicy("Dev-cors", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
                
                options.AddPolicy("production-cors", policy =>
                {
                    policy
                        /*.WithOrigins("https://morizeea.web.app")*/
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
                
            });
            
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SecurityContext securityContext,
            MainDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                new SecurityMemoryInitializer().Initialize(securityContext);
                new DbSeeder(context).SeedDevelopment();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Flashcards.WebApi v1"));
                app.UseCors("Dev-cors");
            }
            else
            {
                app.UseCors("production-cors");
                new SecurityMemoryInitializer().Initialize(securityContext);
                new DbSeeder(context).SeedDevelopment();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}