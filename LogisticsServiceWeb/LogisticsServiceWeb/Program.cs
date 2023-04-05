using LogisticsService.BLL.Interfaces;
using LogisticsService.BLL.Services;
using LogisticsService.Core.DbModels;
using LogisticsService.DAL;
using LogisticsService.DAL.Interfaces;
using LogisticsService.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Globalization;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Repositories
builder.Services.AddScoped<IDataRepository<Address>, AddressRepository>();
builder.Services.AddScoped<IDataRepository<CancelledOrder>, CancelledOrderRepository>();
builder.Services.AddScoped<IDataRepository<Cargo>, CargoRepository>();
builder.Services.AddScoped<IDataRepository<LogisticCompaniesAdministrator>, LogisticCompaniesAdministratorRepository>();
builder.Services.AddScoped<IDataRepository<LogisticCompaniesDriver>, LogisticCompaniesDriverRepository>();
builder.Services.AddScoped<IDataRepository<LogisticCompany>, LogisticCompanyRepository>();
builder.Services.AddScoped<IDataRepository<Order>, OrderRepository>();
builder.Services.AddScoped<IDataRepository<OrderTracker>, OrderTrackerRepository>();
builder.Services.AddScoped<IDataRepository<PrivateCompany>, PrivateCompanyRepository>();
builder.Services.AddScoped<IDataRepository<Rate>, RateRepository>();
builder.Services.AddScoped<IDataRepository<Sensor>, SensorRepository>();
builder.Services.AddScoped<IDataRepository<SmartDevice>, SmartDeviceRepository>();
builder.Services.AddScoped<IDataRepository<Subscription>, SubscriptionRepository>();
builder.Services.AddScoped<IDataRepository<SubscriptionType>, SubscriptionTypeRepository>();
builder.Services.AddScoped<IDataRepository<SystemAdmin>, SystemAdminRepository>();
builder.Services.AddScoped<IDataRepository<Transaction>, TransactionRepository>();

// Services
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<ICancelledOrderService, CancelledOrderService>();
builder.Services.AddScoped<ICargoService, CargoService>();

builder.Services.AddScoped<IEncryptionService, EncryptionService>();

builder.Services.AddScoped<IFormatterService, FormatterService>();

builder.Services.AddScoped<IGoogleMapsApiGeocodeService, GoogleMapsApiGeocodeService>();
builder.Services.AddScoped<IGoogleMapsApiDirectionsService, GoogleMapsApiDirectionsService>();

builder.Services.AddScoped<ILanguageService, LanguageService>();

builder.Services.AddScoped<ILogisticCompaniesAdministratorService, LogisticCompaniesAdministratorService>();
builder.Services.AddScoped<ILogisticCompaniesDriverService, LogisticCompaniesDriverService>();
builder.Services.AddScoped<ILogisticCompanyService, LogisticCompanyService>();

builder.Services.AddScoped<IOrderService, OrderService>(); // TODO
builder.Services.AddScoped<IOrderTrackerService, OrderTrackerService>(); // TODO

builder.Services.AddScoped<IPrivateCompanyService, PrivateCompanyService>();

builder.Services.AddScoped<IRateService, RateService>();

builder.Services.AddScoped<ISensorService, SensorService>();

builder.Services.AddScoped<ISmartDeviceService, SmartDeviceService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<ISubscriptionTypeService, SubscriptionTypeService>();
builder.Services.AddScoped<ISystemAdminService, SystemAdminService>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddScoped<IUserService, UserService>();




builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
}); ;
// Can be added to remove json cycles
//.AddJsonOptions(options =>
// {
//     options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
// });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    option.OperationFilter<SecurityRequirementsOperationFilter>();
});



// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("Jwt:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// DB
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Localization
IList<CultureInfo> supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("en-US"),
        new CultureInfo("uk-UA"),
    };

builder.Services.AddLocalization(o => { o.ResourcesPath = "Resources"; });

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllHeaders",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Localization
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Authentication
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
// Cors
app.UseCors("AllowAllHeaders");

app.Run();
