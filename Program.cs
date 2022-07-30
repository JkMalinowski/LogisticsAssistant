using LogisticsAssistant.Data;
using LogisticsAssistant.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LogisticsAssistantContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LogisticsAssistantContext") ?? throw new InvalidOperationException("Connection string 'LogisticsAssistantContext' not found.")));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ILorriesRepository, LorriesRepository>();
builder.Services.AddTransient<IScheduledTripRepository, ScheduledTripRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<LogisticsAssistantContext>();
    context.Database.Migrate();
}

app.UseRequestLocalization("pl-PL");

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Lorries}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
