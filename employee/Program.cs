using Microsoft.EntityFrameworkCore;
using employee.data;

var builder = WebApplication.CreateBuilder(args);//comand line arguement

builder.Services.AddControllers();//API

builder.Services.AddDbContext<AppDb>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection") //appsettings.json
    ));

// Swagger
builder.Services.AddEndpointsApiExplorer();//get,post,put,delete
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();