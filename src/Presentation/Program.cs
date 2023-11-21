using Microsoft.AspNetCore.Authentication.JwtBearer;
using BusinessLogic;
using DataAccess;
using System.Configuration;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection(nameof(JwtConfig)));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

builder.Services.AddControllers();
builder.Services.AddBusinessServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<JwtMiddlware>();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
