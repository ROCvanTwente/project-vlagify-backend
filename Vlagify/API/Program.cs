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
builder.Services.AddOpenApi();

var app = builder.Build();

// --- 3. Middleware volgorde ---
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

// Belangrijk: UseAuthentication moet VÓÓR UseAuthorization staan!
app.UseAuthentication();
app.UseAuthorization();

// --- 4. Identity Endpoints mappen ---
// Dit zorgt voor de /register en /login routes in je API
app.MapIdentityApi<IdentityUser>();

app.MapControllers();

app.Run();