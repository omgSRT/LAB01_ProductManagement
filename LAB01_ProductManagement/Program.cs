var builder = WebApplication.CreateBuilder(args);

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

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("MyPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();