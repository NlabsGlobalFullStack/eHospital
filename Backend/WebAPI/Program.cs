using Business;
using DataAccess;
using System.Security.Claims;
using WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDefaultCors();

builder.Services.AddCors(cfr =>
{
    cfr.AddDefaultPolicy(policy =>
    {
        policy
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .SetIsOriginAllowed(policy => true);
    });
});

builder.Services.AddBusiness();
builder.Services.AddDataAccess(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//My Middleware
ExtensionsMiddleware.CreateFirstUserAsync(app);
//My Middleware


app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers()
    .RequireAuthorization(policy =>
    {
        policy.RequireClaim(ClaimTypes.NameIdentifier);
        policy.AddAuthenticationSchemes("Bearer");
    });

app.Run();
