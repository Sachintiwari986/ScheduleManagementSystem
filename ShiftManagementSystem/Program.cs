using Microsoft.EntityFrameworkCore;
using ShiftManagementSystem.Models;
using ShiftManagementSystem.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ShiftManagementCoreDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ShiftManagementDefaultConnection")));
builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddScoped<IEmailServices, EmailServices>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Client}/{action=UserLogin}");

app.Run();
