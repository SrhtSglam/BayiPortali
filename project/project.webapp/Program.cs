using Microsoft.EntityFrameworkCore;
using project.data;
using project.data.Abstract;
using project.data.Concrete;

var builder = WebApplication.CreateBuilder(args);
var service = builder.Services;
var connectionString = builder.Configuration.GetConnectionString("Default");

// Add services to the container.
service.AddControllersWithViews();

service.AddScoped<IUserRepository, UserRepository>();
service.AddScoped<IItemRepository, ItemRepository>();
service.AddScoped<IBasketRepository, BasketRepository>();
service.AddScoped<IAccountRepository, AccountRepository>();
service.AddScoped<IFunctionRepository, FunctionRepository>();
service.AddScoped<IOtherRepository, OtherRepository>();
// service.AddScoped<ICategoryRepository, CategoryRepository>();

#region Session
    service.AddDistributedMemoryCache();
    service.AddSession(option => {
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
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseRouting();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();



app.Run();
