using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using complainSystem.models.Users;
using complainSystem.Services.AuthenticationService;
using complainSystem.Services.ComplainService;
using ComplainSystem.Data;
using ComplainSystem.Services.CategoryService;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

// using ComplainSystem.Services.CategoryService;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = """Standard Authorization header using the Bearer scheme. Example. "bearer {Token}" """,
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();

});
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services
    .AddIdentity<User, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = true;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    }).AddDefaultTokenProviders()
    .AddEntityFrameworkStores<DataContext>();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(opt =>
{
    opt.SaveToken = true;
    // opt.Authority = builder.Configuration.GetSection("Jwt:Issuer").Value;
    // opt.Audience = builder.Configuration.GetSection("Jwt:Audience").Value;

    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        RoleClaimType = ClaimTypes.Role,
        NameClaimType = ClaimTypes.NameIdentifier,
        ValidateActor = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        RequireExpirationTime = true,
        // ValidateLifetime = true,
        ValidateIssuerSigningKey = true,


        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"])),


        ValidIssuer = builder.Configuration.GetSection("AppSettings:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("AppSettings:Audience").Value,

    };
}

);

// builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy("User", policy => policy.RequireClaim("User"));
// });


builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IComplainService, ComplainService>();
builder.Services.AddScoped<IAuthenticateUserService, AuthenticateUserService>();
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
