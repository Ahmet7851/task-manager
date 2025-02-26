using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TaskManager.Data;
using TaskManager.Helpers;
using TaskManager.Services;

var builder = WebApplication.CreateBuilder(args);

// URL'leri yapılandır
builder.WebHost.UseUrls("http://localhost:5246");

// JWT ayarlarını yapılandır
var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
var jwtSettings = new JwtSettings
{
    Key = jwtSettingsSection["Key"] ?? throw new InvalidOperationException("JWT Key is not configured"),
    Issuer = jwtSettingsSection["Issuer"] ?? "TaskManager",
    Audience = jwtSettingsSection["Audience"] ?? "TaskManagerClient",
    ExpirationInMinutes = int.Parse(jwtSettingsSection["ExpirationInMinutes"] ?? "60")
};
builder.Services.AddSingleton(jwtSettings);

// JWT kimlik doğrulamayı ekle
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
        };
    });

// Veritabanı bağlantısını ekle
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Servisleri ekle
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITaskService, TaskService>();

// Controller'ları ekle
builder.Services.AddControllers();

// CORS politikasını ekle
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Swagger'ı ekle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Task Manager API", Version = "v1" });

    // JWT kimlik doğrulama için Swagger yapılandırması
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// HTTP request pipeline'ını yapılandır
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Manager API V1");
        c.RoutePrefix = "swagger"; // Swagger'ı /swagger path'inde göster
    });
}

app.UseDefaultFiles(); // index.html'i varsayılan olarak sun
app.UseStaticFiles(); // wwwroot klasöründeki statik dosyaları sun

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Veritabanını otomatik olarak oluştur
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Veritabanı oluşturulurken bir hata oluştu.");
    }
}

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
