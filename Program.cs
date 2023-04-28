// Add this using statement
using Microsoft.EntityFrameworkCore;
using BJHRApp.Data;
// You will need access to your models for your context file
// Builder code from before
var builder = WebApplication.CreateBuilder(args);
// Create a variable to hold your connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DBContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor(); 
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();   

app.UseAuthorization();

app.MapControllerRoute(
    name: "TestReactView",
    pattern: "client/test",
    defaults: new {controller = "React", action="TestReactView"}
);

app.MapControllerRoute(
    name: "CatchAll",
    pattern: "{*url}",
    defaults: new { controller = "React", action = "CatchRoute" }
);

app.Run();