using LogisticsService.BLL.Interfaces;
using LogisticsService.BLL.Services;
using LogisticsService.Core.DbModels;
using LogisticsService.DAL;
using LogisticsService.DAL.Interfaces;
using LogisticsService.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Repositories
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
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>();
builder.Services.AddScoped<ILogisticCompaniesAdministratorService, LogisticCompaniesAdministratorService>();
builder.Services.AddScoped<ILogisticCompaniesDriverService, LogisticCompaniesDriverService>();
builder.Services.AddScoped<ILogisticCompanyService, LogisticCompanyService>();
builder.Services.AddScoped<ILogisticCompanyService, LogisticCompanyService>();
builder.Services.AddScoped<IPrivateCompanyService, PrivateCompanyService>();
builder.Services.AddScoped<IRateService, RateService>();
builder.Services.AddScoped<ISubscriptionTypeService, SubscriptionTypeService>();
builder.Services.AddScoped<ISystemAdminService, SystemAdminService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();




builder.Services.AddControllers();
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
