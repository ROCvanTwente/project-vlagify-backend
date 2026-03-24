using API.Data; // Zorg dat dit naar jouw DbContext map verwijst
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Database configuratie ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));

// --- 2. Identity configuratie ---
// Hiermee koppel je de Identity tabellen aan je DbContext
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
	.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllers();

// Add CORS to allow requests from your frontend
var allowedOrigins = builder.Configuration.GetSection("AllowedCorsOrigins").Get<string[]>() ?? Array.Empty<string>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(allowedOrigins) 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// --- 3. Middleware volgorde ---
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

// Apply the CORS policy
app.UseCors("AllowFrontend");

// Belangrijk: UseAuthentication moet VÓÓR UseAuthorization staan!
app.UseAuthentication();
app.UseAuthorization();

// --- 4. Identity Endpoints mappen ---
// Dit zorgt voor de /register en /login routes in je API
app.MapIdentityApi<IdentityUser>();

app.MapControllers();

app.Run();