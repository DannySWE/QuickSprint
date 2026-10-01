using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SprintPlannerDashboard.Domain.Entites;
using SprintPlannerDashboard.Domain.Interfaces;
using SprintPlannerDashboard.Server.Data;
using SprintPlannerDashboard.Server.Endpoints;
using SprintPlannerDashboard.Server.Repositories;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<SprintPlannerDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration["ConnectionStrings:SprintPlannerAppConnectionString"]);
});
builder.Services.AddDbContext<SprintPlannerUserDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:SprintPlannerAppConnectionStringUsers"]);
});


builder.Services
    .AddIdentityCore<SprintPlannerUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<SprintPlannerUserDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Prevents the application from saving the access token in the AuthenticationProperties.
        // Disabling this reduces memory usage since the token is already read from the request headers.
        options.SaveToken = false;

        // Defines the specific rules for validating incoming JWT tokens
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Forces the application to verify that the token was signed by a trusted key
            ValidateIssuerSigningKey = true,
            // Specifies the secret key used to decrypt and verify the token's digital signature
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:JWTSecret"]!)),
            ValidateIssuer = true,
            ValidateAudience = true,

            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
            ValidAudience = builder.Configuration["AppSettings:Audience"]
        };
    });

builder.Services.AddAuthorization()
    .AddAuthorizationBuilder()
        .AddPolicy("RequireUserFromAmerica", policy =>
        {
            policy.RequireAuthenticatedUser()
            .RequireClaim("Country", "America");
        })
        .AddPolicy("RequireAdmin", policy =>
        {
            policy.RequireAuthenticatedUser()
            .RequireClaim(ClaimTypes.Role, "Admin");
        });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<SprintProjectRepository>();
builder.Services.AddScoped<SprintProjectBoardRepository>();
builder.Services.AddScoped<SprintRepository>();
builder.Services.AddScoped<BacklogItemRepository>();
builder.Services.AddScoped<ProjectTaskRepository>();

var app = builder.Build();

app.UseCors(options =>
{
    options.WithOrigins(["https://localhost:7127", "http://localhost:5091"])
    .AllowAnyMethod()
    .AllowAnyHeader();
});


app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//will cause authorization using http to fail because the redirect
//does not carry the header, use https 
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//app.MapIdentityApi<SprintPlannerUser>();
app.MapSprintProjectEndpoints()
    .MapProjectBoardEndpoints()
    .MapSprintEndpoints()
    .MapBacklogItemEndpoints()
    .MapProjectTaskEndpoints()
    .MapUserAuthEndpoints();


app.MapFallbackToFile("/index.html");

app.Run();

