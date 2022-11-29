using _0_Framework.Application;
using _0_Framework.Application.ZarinPal;
using AccountManagement.Infrastructure.Configuration;
using BlogManagement.Infrastructure.Configuration;
using CommentManagement.Infrastructure.Configuration;
using DiscountManagement.Configuration;
using InventoryManagement.Infrastructure.Configuration;
using ServiceHost.Extension;
using ShopManagement.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRazorPages();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Exclusive_OnlineShopDb");
ShopManagementBootstrapper.Configure(builder.Services, connectionString);
DiscountManagementBootstrapper.Configure(builder.Services, connectionString);
InventoryManagementBootstrapper.Configure(builder.Services, connectionString);
BlogManagementBootstrapper.Configure(builder.Services, connectionString);
CommentManagementBootstrapper.Configure(builder.Services, connectionString);
AccountManagementBootstrapper.Configure(builder.Services, connectionString);

builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddTransient<IFileUploader, FileUploader>();
builder.Services.AddTransient<IAuthHelper, AuthHelper>();
builder.Services.AddTransient<IViewRenderService, RenderViewToString>();
builder.Services.AddTransient<IZarinPalFactory, ZarinPalFactory>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
