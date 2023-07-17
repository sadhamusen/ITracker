using InitiativeTracker.DataBaseConnection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ITracker.Services;
using Microsoft.Extensions.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseAccess>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DbConnection")
    ));

builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>{
 options.TokenValidationParameters = new TokenValidationParameters
{
ValidateIssuerSigningKey = true,
     ValidateAudience = false,
ValidateIssuer = false,
IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(
    builder.Configuration.GetSection("AppSettings:Token").Value!))

};

});


var app = builder.Build();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
