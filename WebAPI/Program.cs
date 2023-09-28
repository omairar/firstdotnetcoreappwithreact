using BOL;
using DAL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;
//Step 1: DAL And BOL 

//Step 2: DAL And BOL 

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddDbContext<SSDBContext>(
   opt => opt.UseSqlServer(configuration.GetSection("SS:ssConStr").Value)
    );

//configuring identity
builder.Services.AddIdentity<SSUser, IdentityRole>()
                .AddEntityFrameworkStores<SSDBContext>()
                .AddDefaultTokenProviders();

//step 3 validating Token
//Step-3.1: Create signingKey from Secretkey
var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("SS:JWTKey").Value));

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


using (var scope = app.Services.CreateScope())
{
    #region Creating Roles
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roleNames = { "Admin", "User", "Editor" };
    IdentityResult roleResult;
    foreach (var roleName in roleNames)
    {
        var roleExist = roleManager.RoleExistsAsync(roleName).Result;
        if (!roleExist)
        {
            roleResult = roleManager.CreateAsync(new IdentityRole(roleName)).Result;
        }
    }
    #endregion
    #region Creating Admin
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<SSUser>>();
    var userExist = userManager.FindByNameAsync("Superuser").Result;
    if (userExist == null)
    {
        var user = new SSUser() { UserName = "Superuser", Email = "admin@ss.com" };
        var userResult = userManager.CreateAsync(user, "Superuser@123").Result;
        var assignRoleResult = userManager.AddToRoleAsync(user, "Admin").Result;
    }
    #endregion
}


app.MapGet("/", () => "Hello World!");

//Step 3: Add Map Controllers

//enable settings for cors
app.UseCors(x => x.WithOrigins(configuration.GetSection("SS:Origins").GetChildren().Select(x => x.Value).ToArray())
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              .AllowCredentials());

//enalbe auth
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.Run();
