using project.data;
using project.data.Abstract;
using project.data.Concrete;
using System.Configuration;
using project.webapp.Filters;
using project.webapp.Services;

var builder = WebApplication.CreateBuilder(args);
var service = builder.Services;
// var connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
var connectionString = builder.Configuration.GetConnectionString("Default");

service.AddControllersWithViews();

service.AddScoped<IAccountRepository, AccountRepository>();
service.AddScoped<IFunctionRepository, FunctionRepository>();
service.AddScoped<IOtherRepository, OtherRepository>();
service.AddScoped<IOrderRepository, OrderRepository>();
service.AddScoped<ISMTPRepository, SMTPRepository>();

service.AddScoped<IMailHelper, MailHelper>();
service.AddScoped<NAVServer>();

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
