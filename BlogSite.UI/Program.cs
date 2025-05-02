using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BlogSite.UI.Entities;
using BlogSite.UI.Context;
using Microsoft.AspNetCore.Identity.UI.Services;
using BlogSite.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Connection String
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

//  DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

//  Identity
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

//email sender
builder.Services.AddTransient<IEmailSender, EmailSender>();


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); // Identity Razor Pages

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // Identity 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); //  Identity Scaffolding 

app.Run();
