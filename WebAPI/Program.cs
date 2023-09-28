using BOL;
using DAL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

//step 3 validating Token
//Step-3.1: Create signingKey from Secretkey
var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is the JWT Security Token Authentication"));

//Step-3.2:Create Validation Parameters using signingKey
var tokenValidationParameters = new TokenValidationParameters()
{
    IssuerSigningKey = signingKey,
    ValidateIssuer = false,
    ValidateAudience = false,
    ClockSkew = TimeSpan.Zero
};

//Step-3.3: Install Microsoft.AspNetCore.Authentication.JwtBearer
//Step-3.4: Set Authentication Type as JwtBearer
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
//Step-4: Set Validation Parameter created above
.AddJwtBearer(jwt =>
{
    jwt.TokenValidationParameters = tokenValidationParameters;
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
