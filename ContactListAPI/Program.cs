using ContactListAPI.Repositories;
using ContactListAPI.Repositories.Data.Implementations;
using ContactListAPI.Repositories.Data.Interfaces;
using ContactListAPI.Validation.Implementations;
using ContactListAPI.Validation.Interfaces;
using EFCoreDataAccess.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
    });
builder.Services.AddDbContextPool<ContactListContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICategoryValidator, CategoryValidator>();
builder.Services.AddScoped<IContactValidator, ContactValidator>();
builder.Services.AddScoped<IPersonValidator, PersonValidator>();
builder.Services.AddScoped<ISubcategoryValidator, SubcategoryValidator>();
builder.Services.AddScoped<IUserValidator, UserValidator>();

builder.Services.AddScoped<IJWTRepository, JWTRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

string allowedOrigins = "_ContactListSPA";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowedOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7132",
                              "http://localhost:5132")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials()
                          .WithExposedHeaders();
                      });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(allowedOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
