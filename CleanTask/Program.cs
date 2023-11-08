using CleanTask.Domains;
using CleanTask.Tools;
using Core.Interfaces.Repository;
using Microsoft.Extensions.Options;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Infrastructure.Data.Entitties;
using Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);







builder.Services.AddDbContext<MyDBContex>(optionsAction: Options =>
Options.UseSqlServer(builder.Configuration.GetConnectionString(name: "DefaultConnection")));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IEmailSend, EmailSend>();
builder.Services.AddScoped<IViewRenderService, ViewRenderService>();
builder.Services.AddScoped<ProductRepository,ProductRepositories>();
builder.Services.AddScoped<UserRepository,UserRepositories>();

builder.Services.AddIdentityCore<CleanTask.Domains.userapp>()
    .AddEntityFrameworkStores<DbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityCore<CleanTask.Domains.userapp>()
    .AddEntityFrameworkStores<DbContext>()
    .AddDefaultTokenProviders();




// Add services to the container.
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddRazorPages()
    .AddMicrosoftIdentityUI();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
