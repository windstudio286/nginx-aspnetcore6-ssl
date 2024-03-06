using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NginxDocker.Api.Models;
using NginxDocker.Api.Services;
using Microsoft.OpenApi.Models;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
// Add services to the container.
var conn = builder.Configuration.GetConnectionString("WebApiDatabase");
builder.Services.AddDbContext<WebApiDbContext>(options =>
    options.UseNpgsql(conn)
);
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:5001",
            ValidAudience = "https://localhost:5001",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenService.Secret))
        };
        //method 1
        //custom 401 content
        //options.Events = new JwtBearerEvents
        //{
        //    OnAuthenticationFailed = async (context) =>
        //    {
        //        Console.WriteLine("Printing in the delegate OnAuthFailed");
        //    },
        //    OnChallenge = async (context) =>
        //    {
        //        Console.WriteLine("Printing in the delegate OnChallenge");

        //        // this is a default method
        //        // the response statusCode and headers are set here
        //        context.HandleResponse();

        //        // AuthenticateFailure property contains 
        //        // the details about why the authentication has failed
        //        if (context.AuthenticateFailure != null)
        //        {
        //            context.Response.StatusCode = 401;

        //            // we can write our own custom response content here
        //            await context.HttpContext.Response.WriteAsJsonAsync("Token Validation Has Failed. Request Access Denied");
        //        }
        //    }
        //};
    });
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "weplaywefun.online", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//method 2
//custom 401 content
app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
    {
        await context.Response.WriteAsync("Token Validation Has Failed. Request Access Denied");
    }
});
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

