using IdentityService.Business.DependencyResolver;
using IdentityService.Business.Policy;
using IdentityService.DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
         .AddJsonOptions(options =>
         {
             options.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamingPolicy();
             options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
         });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<AppDbContext>();



builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfiguration:SecretKey").Value);
    var issuer = "ComparAcademy";
    var audience = "ComparAcademy";

    option.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        RequireExpirationTime = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidAudience = audience,
        ValidIssuer = issuer,
    };
});
builder.Services.AddBusinessRegistration();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();