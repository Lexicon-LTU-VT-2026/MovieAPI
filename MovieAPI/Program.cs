using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;
using MovieAPI.Data;
using MovieAPI.Extensions;
using MovieAPI.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// 1) AddDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
));

// 2) Identity
builder.Services
    .AddIdentityCore<ApplicationUser>(options =>
    {
        // Valfria lösenordsregler
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
    })
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//3) JWT - inställningar
// Hämtar hela JWT-delen från appsettings.json
var jwtSettings = builder.Configuration.GetSection("Jwt");

// Hämtar JWT: key som text och omvandlar den till en byte-array
// Nyckeln används för att kontrollera JWT-signaturen
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);  // MovieApiDevelopmentSecretKey2026AtLeast32Characters

builder.Services.AddAuthentication(options =>
{
    // Vilken autentiseringsmetod som används som standard
    // Bearer = JWT skickas i Authorization-headern
    // Det här styr hur API: t försöker identfiera användaren 
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

    // Vad ska hända när en användare saknar/ har ogiltig token?
    // API: t svarar normalt med HTTP 401 Unauthorized
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Reglerna som varje inkommande JWT måste klara
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],    // MovieAPI
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Registrerar auktoriseringstjänster i DI
// Behövs innan innan man börjar använda .RequireAuthorization eller [Authorize] på endpoints
// Ska ligga efter Identity
builder.Services.AddAuthorization();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.SeedData();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.SeedData();
app.Run();
