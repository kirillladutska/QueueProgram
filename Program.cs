using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using QueueProgram.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Logging.ClearProviders();
builder.Services.AddLogging();
builder.Logging.AddConsole();
builder.Services.AddScoped<QueueProgram.Popup.IPopupService, QueueProgram.Popup.PopupService>();
builder.Services.AddScoped<QueueProgram.Visit.IValidationService, QueueProgram.Visit.ValidationService>();
builder.Services.AddScoped<QueueProgram.Visit.IVisitService, QueueProgram.Visit.VisitService>();
builder.Services.AddScoped<QueueProgram.Admin.IAutorisationService, QueueProgram.Admin.AutorisationService>();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
app.UseRouting();
app.MapControllers();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Pages}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "api",
        pattern: "api/{controller}/{action}/{id?}");

    endpoints.MapControllerRoute(
        name: "LiveQueue",
        pattern: "livequeue",
        defaults: new { controller = "Pages", action = "LiveQueue" });

    endpoints.MapControllerRoute(
        name: "Admin",
        pattern: "admin",
        defaults: new { controller = "Pages", action = "Admin" });
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.Run();
