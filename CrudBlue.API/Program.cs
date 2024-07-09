using FluentValidation;
using CrudBlue.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using CrudBlue.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AgendaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AgendaDbConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueDevOrigin",
        builder => builder.WithOrigins("http://localhost:8080")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

var currentAssembly = Assembly.GetExecutingAssembly();
var referencedAssemblies = currentAssembly.GetReferencedAssemblies()
    .Select(Assembly.Load)
    .Where(a => a.FullName.Contains("CrudBlue"));

var configuration = new ConfigurationDbContext
{
    Assemblies = referencedAssemblies.Union(new[] { currentAssembly }).ToList()
};

builder.Services.AddAutoMapper(typeof(ContactsProfile));
builder.Services.AddControllers();

builder.Services.AddLibs(configuration);


builder.Services.AddValidatorsFromAssemblyContaining<Program>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowVueDevOrigin");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();