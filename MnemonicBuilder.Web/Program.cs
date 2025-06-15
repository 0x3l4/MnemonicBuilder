using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MnemonicBuilder.Application.Services;

using MnemonicBuilder.Domain.Interfaces;


//using Microsoft.EntityFrameworkCore;
using MnemonicBuilder.Infrastructure.Data;
using MnemonicBuilder.Infrastructure.Entities;
using MnemonicBuilder.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddMemoryCache();
builder.Services.AddScoped<IWordSearchService, WordSearchService>();
builder.Services.AddScoped<IWordRepository>(sp =>
{
    var filePath = Path.Combine(builder.Environment.ContentRootPath, "data", "all_russian_words.txt");
    return new TextFileWordRepository(filePath);
});
builder.Services.AddSingleton<WordSearchCacheService>();
//builder.Services.AddScoped<IWordRepository>(provider => 
//    new TextFileWordRepository(Path.Combine(builder.Environment.WebRootPath, "data", "all_russian_words.txt")));

//builder.Services.AddScoped<SearchWordsByPatternHandler>();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.User.RequireUniqueEmail = false;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapRazorPages();

app.Run();
