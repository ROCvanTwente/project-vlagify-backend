using API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- DATABASE ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// --- IDENTITY ---
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// --- CORS (BELANGRIJK) ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// --- MIDDLEWARE ---
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// 👇 CORS hier (VOOR authentication)
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

// --- ENDPOINTS ---
app.MapIdentityApi<IdentityUser>();
app.MapControllers();

app.Run();