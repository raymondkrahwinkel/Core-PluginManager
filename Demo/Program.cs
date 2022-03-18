using CorePluginManager;
using CorePluginManager.Plugins.Alert;
using CorePluginManager.Plugins.Breadcrumb.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder
    .BuildPluginManager(typeof(Program).Assembly)
    .SetPluginManagerOptions(new BreadcrumbOptions() { ActiveCssClass = "active" })
    .SetPluginManagerOptions(new AlertOptions()
    {
        ErrorMessages = new AlertOptions.MessageOptions()
        {
            CssClass = "alert-danger",
            Dismissible = true
        }
    });
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UsePluginManager();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();