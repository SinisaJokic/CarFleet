using CarFleetAPI.DbContexts;
using CarFleetAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: MyAllowSpecificOrigins,
//                      builder =>
//                      {
//                          builder.WithOrigins("http://example.com",
//                                              "http://www.contoso.com");
//                      });
//});

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson()
.AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    //var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

    //setupAction.IncludeXmlComments(xmlCommentsFullPath);
    setupAction.SwaggerDoc("v1",new OpenApiInfo { Title="CarFleet", Version = "v1" });

    setupAction.AddSecurityDefinition("CarFleetApiBearerAuth", new OpenApiSecurityScheme()
    {
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "Input a valid token to access this API"
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "CarFleetApiBearerAuth" }
            }, new List<string>() }
    });
});

builder.Services.AddDbContext<VehicleInfoContext>(
    dbContextOptions => dbContextOptions.UseSqlite("DataSource=VehicleInfo.db"));
//builder.Configuration["ConnectionStrings:VehicleInfoDBConnectionString"]));

builder.Services.AddScoped<IVehicleInfoRepository, VehicleInfoRepository>();
builder.Services.AddScoped<IDriverInfoRepository, DriverInfoRepository>();
builder.Services.AddScoped<IVehicleAssignRepository, VehicleAssignRepository>();
builder.Services.AddScoped<IUserModelInfoRepository, UserModelInfoRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddAuthentication();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            //ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            //IssuerSigningKey = new SymmetricSecurityKey(
            //    Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    }
    );

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MustBeAdmin", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("Roles", "Administrator");
    });
    options.AddPolicy("OnlyView", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("Roles", "Viewer");
    });
    options.AddPolicy("Editor", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("Roles", "Editor");
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

app.UseRouting();

app.UseCors("Open");

app.UseAuthentication();

app.UseAuthorization();

app.UseCors();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
