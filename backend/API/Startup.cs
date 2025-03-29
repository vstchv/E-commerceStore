using System;
using API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using API.Middleware;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Collections.Generic;
using API.RequestHelpers;

namespace API
{
    public class Startup
    {
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {

            services.AddControllers();
            services.AddAutoMapper( typeof( MappingProfiles ).Assembly );
            services.AddSwaggerGen( c =>
             {
                 c.SwaggerDoc( "v1", new OpenApiInfo { Title = "API", Version = "v1" } );

                 //send bearer token that we get back from login, parse it in header 
                 c.AddSecurityDefinition( "Bearer", new OpenApiSecurityScheme
                 {
                     Description = "Jwt auth header",
                     Name = "Authorization",
                     In = ParameterLocation.Header,
                     Type = SecuritySchemeType.ApiKey,
                     Scheme = "Bearer"
                 } );
                 c.AddSecurityRequirement( new OpenApiSecurityRequirement
                 {
                     {
                         new OpenApiSecurityScheme
                         {
                             Reference = new OpenApiReference
                             {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer"
                             },
                             Scheme = "oauth2",
                             Name = "Bearer",
                             In = ParameterLocation.Header
                         },
                         new List<string>()
                     }
                 } );
             } );

          services.AddDbContext<Contexts>(options =>
             {
    options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
             } );


            services.AddCors();

            services.AddIdentityCore<User>( options =>
             {
                 options.User.RequireUniqueEmail = true;
             } )
                .AddRoles<Role>()
                .AddEntityFrameworkStores<Contexts>();

            services.AddAuthentication( JwtBearerDefaults.AuthenticationScheme )
                .AddJwtBearer( options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false, //validation from API 
                        ValidateAudience = false,  // validation from local host
                        ValidateLifetime = true, //bc out token has exp. date
                        ValidateIssuerSigningKey = true,  // checks validation key and secret key 
                        IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8
                        .GetBytes( Configuration["JWTSettings:TokenKey"] ) )

                    };
                } );
            services.AddAuthorization();
            services.AddScoped<TokenService>();
           // services.AddScoped<ImageService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {

            app.UseMiddleware<MiddlewareExeptions>();


            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI( c => c.SwaggerEndpoint( "/swagger/v1/swagger.json", "API v1" ) );
            }

            //  app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors( opt =>
             {
                 opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins( "http://localhost:3001" );
             } );

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints( endpoints =>
             {
                 endpoints.MapControllers();
             } );
        }
    }
}



