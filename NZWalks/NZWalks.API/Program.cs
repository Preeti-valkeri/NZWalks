using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NZWalks.API.Data;
using NZWalks.API.Repository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(Options=>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Name="JWT Authentication",
        Description="Enter a valid JWT bearer token",
        In=ParameterLocation.Header,
        Type=SecuritySchemeType.Http,
        Scheme="bearer",
        BearerFormat="Jwt",
        Reference=new OpenApiReference
        {
            Id=JwtBearerDefaults.AuthenticationScheme,
            Type=ReferenceType.SecurityScheme
        }
    };
    Options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    Options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme,new string[] { } }
    });
});

builder.Services.AddFluentValidation(Options => Options.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddDbContext<NZWalksDBContext>(Options => {
    Options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalks"));
});
builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<IWalkRepository, WalkRepository>();
builder.Services.AddScoped<IWalkdifficultyRepository,WalkdifficultyRepository>();
builder.Services.AddScoped<ITokenHandler, TokenHandlerRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();


builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
    Options =>
    Options.TokenValidationParameters = new 
    Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
         ValidateIssuer= true,
         ValidateAudience= true,
         ValidateLifetime= true,
         ValidateIssuerSigningKey= true,
         ValidIssuer = builder.Configuration["Jwt:Issuer"],
         ValidAudience= builder.Configuration["Jwt:Audience"],
         IssuerSigningKey= new 
         SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
