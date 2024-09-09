using Microsoft.AspNetCore.OData;
using Repositories.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddOData(options => options.Select().Filter().OrderBy().Count().SetMaxTop(null).Expand());

builder.Services.AddScoped<ICategoryRepository, ICategoryRepository>();
builder.Services.AddScoped<IProductRepository, IProductRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5021/")
             .AllowAnyMethod()
             .AllowAnyHeader()
             .SetIsOriginAllowedToAllowWildcardSubdomains()
             .AllowCredentials()
             .SetIsOriginAllowed(_ => true);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("MyPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
