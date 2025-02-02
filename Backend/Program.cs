using Backend.Database;
using Backend.Interfaces;
using Backend.Models;
using Backend.Repositories;
using Backend.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConneciton"));
});


builder.Services.AddIdentity<AppUser,IdentityRole>(option =>{
    option.Password.RequireDigit = false;
    option.Password.RequireLowercase = false;
    option.Password.RequireUppercase = false;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequiredLength = 5;
}).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthentication(options=> {
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme = 
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
        ),
    };
});


builder.Services.AddScoped<ITokenService,TokenService>();
builder.Services.AddScoped<IProjectRepositories,ProjectRepositories>();
builder.Services.AddScoped<IProjectPortfolioRepositories,ProjectPortfolioRepositories>();
builder.Services.AddScoped<IAuditRepository,AuditRepository>();
builder.Services.AddScoped<IUserValidationService,UserValidationService>();
builder.Services.AddScoped<IAuditCategoryRepository,AuditCategoryRepository>();
builder.Services.AddScoped<IAuditQuestionRepository,AuditQuestionRepository>();
builder.Services.AddScoped<IUserRequsetRepoistory,UserRequestRepository>();
builder.Services.AddScoped<IRiskCalculationService,RiskCalculationService>();
builder.Services.AddScoped<CsvService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    app.Logger.LogInformation("Development environment detected");

}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using(var scope = app.Services.CreateScope()){
    var rolemanger = 
    scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new [] {"Admin", "User"};

    foreach(var role in roles){
        if(!await rolemanger.RoleExistsAsync(role))
            await rolemanger.CreateAsync(new IdentityRole(role));
    }
}
app.Run();


// Goals for 22/01/2025 Complete Inivation 
// Get Inivation Requests Project Sided
// Accept Project Invites
// Delete Project Invites


//Goals for 23/01/2025 - 26/01/2025  
//Finialize Risk
//Attempt to a import to csv 


//Goals for 26/01/2025 - 30/01/2025 
//Attempt to Start the Frontend and make minimal frontend

