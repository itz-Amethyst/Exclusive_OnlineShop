using _0_Framework.Application;
using BlogManagement.Infrastructure.Configuration;
using ShopManagement.Configuration;
using DiscountManagement.Configuration;
using InventoryManagement.Infrastructure.Configuration;
using ServiceHost.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//!WiredUp ShopManagementBootstrapper Dependencies
var connectionString = builder.Configuration.GetConnectionString("Exclusive_OnlineShopDb");
ShopManagementBootstrapper.Configure(builder.Services, connectionString);
DiscountManagementBootstrapper.Configure(builder.Services , connectionString);
InventoryManagementBootstrapper.Configure(builder.Services , connectionString);
BlogManagementBootstrapper.Configure(builder.Services, connectionString);


builder.Services.AddTransient<IFileUploader, FileUploader>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapDefaultControllerRoute();

app.Run();
