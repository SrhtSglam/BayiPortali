using Microsoft.EntityFrameworkCore;
using project.data;

var builder = WebApplication.CreateBuilder(args);
var service = builder.Services;
var connectionString = builder.Configuration.GetConnectionString("Default");

// Add services to the container.
service.AddControllersWithViews();

service.AddDbContext<AppDbContext>(option => 
    option.UseSqlServer(connectionString)
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}")
//     .WithStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
