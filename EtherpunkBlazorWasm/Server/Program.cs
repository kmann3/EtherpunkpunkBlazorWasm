using EtherpunkBlazorWasm.Server.Auth;
using EtherpunkBlazorWasm.Server.Data;
using EtherpunkBlazorWasm.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IUserDatabase, UserDatabase>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidAudience = AppSettings.ValidAudience,
        ValidateIssuer = true,
        ValidIssuer = AppSettings.ValidIssuer,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(AppSettings.SecretKey)),
    };
});

#if DEBUG
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    builder.WithOrigins("https://localhost:44338")
            .AllowAnyMethod()
            .AllowAnyHeader());
});
#endif

builder.Services.AddScoped<DbContext, EpunkDbContext>();
builder.Services.AddDbContext<EpunkDbContext>();


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

#if DEBUG
// Firefox will throw fits because of localhost stuffs.
// DO NOT USE THIS IN PRODUCTION. THIS DISABLES AN IMPORTANT SECURITY MEASURE
app.UseCors();
#endif

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
