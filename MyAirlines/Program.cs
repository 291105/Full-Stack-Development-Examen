using EmployeeAPI.AutoMapper;
using FlightProject.Domain.DataDB;
using FlightProject.Domain.EntitiesDB;
using FlightProject.Repositories;
using FlightProject.Repositories.Interfaces;
using FlightProject.Services;
using FlightProject.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyAirlines.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//in welke map zitten de resources
builder.Services.AddLocalization(
    options => options.ResourcesPath = "Resources");

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();
var supportedCultures = new[] { "nl", "en", "fr", "es" };

//Dit configureert de standaard instellingen voor RequestLocalizationOptions en slaat ze op in de dependency injection-container (DI).
builder.Services.Configure<RequestLocalizationOptions>(options => {
    options.SetDefaultCulture(supportedCultures[0])
      .AddSupportedCultures(supportedCultures)
      .AddSupportedUICultures(supportedCultures);
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<FullStackDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

// SwaggerGen produces JSON schema documents that power Swagger UI.By default, these are served up under / swagger
//{ documentName}/ swagger.json, where { documentName} is usually the API version.
//provides the functionality to generate JSON Swagger documents that describe the objects, methods, return types, etc.
//eerste paramter, is de naam van het swagger document
//
// Register the Swagger generator, defining 1 or more Swagger documents
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API Employee",
        Version = "version 1",
        Description = "An API to perform Employee operations",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "CDW",
            Email = "christophe.dewaele@vives.be",
            Url = new Uri("https://vives.be"),
        },
        License = new OpenApiLicense
        {
            Name = "Employee API LICX",
            Url = new Uri("https://example.com/license"),
        }
    });
});


// Add AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// DI
builder.Services.AddTransient<IService<Place>, AirportService>();
builder.Services.AddTransient<IDAO<Place>, AirportDAO>();
builder.Services.AddTransient<IFlightService, FlightService>();
builder.Services.AddTransient<IFlightDAO, FlightDAO>();
builder.Services.AddTransient<IService<ArrivalPlace>, ArrivalPlaceService>();
builder.Services.AddTransient<IDAO<ArrivalPlace>, ArrivalPlaceDAO>();
builder.Services.AddTransient<IService<DeparturePlace>, DeparturePlaceService>();
builder.Services.AddTransient<IDAO<DeparturePlace>, DeparturePlaceDAO>();


//jwt enzo
/*builder.Services
.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtConfig:JwtIssuer"],
        ValidAudience = builder.Configuration["JwtConfig:JwtIssuer"],
        IssuerSigningKey = new
    SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:JwtKey"])),
        ClockSkew = TimeSpan.Zero // remove delay of token when expire
    };
});*/




//session
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "be.VIVES.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(3);
}
    );

var app = builder.Build();

//add session
app.UseSession();

// Culture from the HttpRequest
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


var swaggerOptions = new MyAirlines.Options.SwaggerOptions();
builder.Configuration.GetSection(nameof(MyAirlines.Options.SwaggerOptions)).Bind(swaggerOptions);
// Enable middleware to serve generated Swagger as a JSON endpoint.
//RouteTemplate legt het path vast waar de JSON-file wordt aangemaakt
app.UseSwagger(option => { option.RouteTemplate = swaggerOptions.JsonRoute; });
//// By default, your Swagger UI loads up under / swagger /.If you want to change this, it's thankfully very straight-forward.
//Simply set the RoutePrefix option in your call to app.UseSwaggerUI in Program.cs:
app.UseSwaggerUI(option =>
{
    option.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
});
app.UseSwagger();





app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
