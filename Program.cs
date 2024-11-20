
using Microsoft.AspNetCore.Http;
using MicroTransation.Data;
using MicroTransation.Middleware;
using MicroTransation.Services.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var Origins = "AuthorizedApps";

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<TokenMappers>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Origins,
         policy =>
         {
             policy.WithOrigins("http://localhost:5173")
               .WithMethods("GET", "POST", "PUT", "DELETE", "OPTION")
              .AllowAnyHeader();
         });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors(Origins);


//app.UseWhen(context => !context.Request.Path.StartsWithSegments("/auth"), appBuilder => { 
//    appBuilder.UseMiddleware<AuthMiddleware>(); 
//});

app.Run();
