using Carter;

using FluentValidation;

using SweaTeam;
using SweaTeam.extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var assembly = typeof(Program).Assembly;

builder.Services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCarter();

builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
#pragma warning disable S125 // Sections of code should not be commented out
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();

    app.SeedData();
}
#pragma warning restore S125 // Sections of code should not be commented out


app.MapCarter();
app.UseHttpsRedirection();

app.Run();
