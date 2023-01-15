using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ptuswph_backend.Database;
using ptuswph_backend.Models;
using ptuswph_backend.Models.Settings;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Settings
var jwtSettings = builder.Configuration.GetSection("JWT").Get<JWTSettings>() ?? new();

// Auth
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
    };
});
builder.Services.AddAuthorization();

// Service Container
builder.Services.AddDbContext<ApiContext>();   
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Wypo¿yczalnia filmów", Version= "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Description = "Wpisz token JWT w formacie \"Bearer [jwt]\"",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();
app.MapFallbackToFile("index.html");


// SEEDING
using var scope = app.Services.CreateScope();
using var context = scope.ServiceProvider.GetRequiredService<ApiContext>();
if(!context.Movies.Any())
{
    string jsonText = File.ReadAllText("Database/movies.json");
    Movie[]? movies = JsonSerializer.Deserialize<Movie[]>(jsonText);
    if(movies != null)
    {
        context.Movies.AddRange(movies);
        context.SaveChanges();
    }
}
if(!context.Users.Any())
{
    string jsonText = File.ReadAllText("Database/users.json");
    User[]? users = JsonSerializer.Deserialize<User[]>(jsonText);
    if(users != null)
    {
        foreach (var user in users)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        }
        context.Users.AddRange(users);
        context.SaveChanges();
    } 
}

app.Run();
