using System.Text;
using API.Data;
using API.Extensions;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddApplicationServices(builder.Configuration); 
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();


app.UseHttpsRedirection();

// app.UseAuthorization();
app.UseCors(x => x.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins("https://localhost:4200","http://localhost:4200"));
app.UseAuthentication();
app.UseAuthorization(); 

app.MapControllers();

app.Run();
