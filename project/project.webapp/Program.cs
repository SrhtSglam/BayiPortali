using Microsoft.EntityFrameworkCore;
using project.data;
using project.data.Abstract;
using project.data.Concrete;
using project.webapp.Filters;

var builder = WebApplication.CreateBuilder(args);
var service = builder.Services;
var connectionString = builder.Configuration.GetConnectionString("Default");

// Add services to the container.
service.AddControllersWithViews();

service.AddScoped<IAccountRepository, AccountRepository>();
service.AddScoped<IFunctionRepository, FunctionRepository>();
service.AddScoped<IOtherRepository, OtherRepository>();
service.AddScoped<IOrderRepository, OrderRepository>();

#region Session
    service.AddDistributedMemoryCache();
    service.AddSession(option =>
    {
        option.IdleTimeout = TimeSpan.FromMinutes(60);
        option.Cookie.HttpOnly = true;
        option.Cookie.IsEssential = true;
    });
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSession();
app.UseRouting();
app.UseHttpsRedirection();

app.UseStaticFiles(); 

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.Run();
