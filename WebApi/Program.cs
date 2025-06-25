using Aplicación.Maper;
using Infraestructura.DependenciaInjenction;
using Aplicación.DependenciaInjenction;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    // authenticacion del token 
    options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description =
            "Autenticación JWT usuando el esquema Bearer. \r\n\r\n" +
            "Ingresa la palabra 'Bearer' seguido de un [espacio] y después su token en el campo de abajo. \r\n\r\n" +
            "Ejemplo: \"Bearer tkjdadr323nda\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Scheme = "Bearer"

        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement()
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

        });
    }
);




builder.Services.AplicacionServices(builder.Configuration);

var key = builder.Configuration.GetValue<string>("ApiSettings:Secreta");
builder.Services.AddAuthentication(
    x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    }).AddJwtBearer(
     x =>
     {
         x.RequireHttpsMetadata = false;
         x.SaveToken = true;
         x.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuerSigningKey = true,
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
             ValidateIssuer = false,
             ValidateAudience = false
         };

     });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Soporte para CORS
//app.UseCors("PoliticaCors");

// soporte para autentication
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
