using BOL;
using DAL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
//Step 1: DAL And BOL 

//Step 2: DAL And BOL 

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddDbContext<SSDBContext>();

//configuring identity
builder.Services.AddIdentity<SSUser, IdentityRole>()
                .AddEntityFrameworkStores<SSDBContext>()
                .AddDefaultTokenProviders();

//configuring cookies
builder.Services.ConfigureApplicationCookie(opt =>
{

    // opt.ExpireTimeSpan = new TimeSpan(0, 30, 0); // (H, M, S)
    opt.Events = new CookieAuthenticationEvents()
    {
        //authentications
        OnRedirectToLogin = redirectContext =>
        {
            redirectContext.HttpContext.Response.StatusCode = 403;
            return Task.CompletedTask;
        },

        //authorization
        OnRedirectToAccessDenied = redirectContext =>
        {
            redirectContext.HttpContext.Response.StatusCode = 401;
            return Task.CompletedTask;
        }
    };
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
});


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//Step 3: Add Map Controllers

//enable settings for cors
app.UseCors(x => x.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader().AllowCredentials());

//enalbe auth
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.Run();
