using System.Reflection;
using CNA.Application.Interfaces;
using CNA.Infrastructure;
using CNA.Infrastructure.Repositories;
using CNA.WebApi.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<CNADbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CNA API",
        Version = "v1",
        Description = "CNA Web API"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }

    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

    c.TagActionsBy(api => new[] { api.ActionDescriptor.RouteValues["controller"] });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CNA API V1");
        c.RoutePrefix = "swagger";
        c.DisplayRequestDuration();
    });
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();