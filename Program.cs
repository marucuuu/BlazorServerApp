using BlazorServerApp.Components;
using BlazorServerApp.Data;
using BlazorServerApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorComponents()  // Blazor Server setup
       .AddInteractiveServerComponents();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AssetService>();
builder.Services.AddScoped<LogoutService>();

builder.Services.AddHttpContextAccessor();

// Authentication & Authorization setup
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/";        // Redirect to login page when not authenticated
        options.LogoutPath = "/logout"; // Logout URL
        options.ExpireTimeSpan = TimeSpan.FromHours(1); // Optional: cookie expiration
        options.SlidingExpiration = true;  // Optional: refresh cookie expiration on activity
    });

builder.Services.AddAuthorization();
builder.Services.AddAuthorizationCore();  // Required for Blazor components to support [Authorize]

// Build the app
var app = builder.Build();

// Configure middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();  // Ensure static files are served (css, js, images, etc.)

app.UseRouting(); // Required before authentication and authorization middleware

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();  // Keep if you use anti-forgery tokens

// Custom logout endpoint
app.MapGet("/logout", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    context.Response.Redirect("/");
});

// Map Blazor component routing
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
