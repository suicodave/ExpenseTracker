using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using NSwag;
using NSwag.Generation.Processors.Security;

using Server.Data;
using Server.Data.Extensions;
using Server.Users;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var config = builder.Configuration;

// Add services to the container.

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


services.AddScoped<DatabaseInitializer>();


services.AddOpenApiDocument(configure =>
            {
                configure.Title = "Expense Tracker API";
                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });



services.AddControllersWithViews();

services.AddRazorPages();

services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(config.GetConnectionString("DefaultConnection")));

services.AddDefaultIdentity<User>()
.AddRoles<Role>()
.AddEntityFrameworkStores<ApplicationDbContext>();

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = config["JwtIssuer"],
        ValidAudience = config["JwtAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSecurityKey"])),

    };
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();

    app.UseDeveloperExceptionPage();

}

await app.UseDatabaseInitializer();

app.UseOpenApi();

app.UseSwaggerUi3(options =>
{
    options.Path = "/api";


});

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
