using Appointment_Scheduler.Data;
using Appointment_Scheduler.Models;
using Appointment_Scheduler.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Appointment_Scheduler.Configuration;
using Appointment_Scheduler.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AppointmentSchedulerDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AppointmentSchedulerDbContextConnection' not found.");

builder.Services.AddDbContext<AppointmentSchedulerDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<AppointmentSchedulerDbContext>();

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

builder.Services.AddTransient<IMailService, MailService>();

builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();

builder.Services.AddControllersWithViews();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddRazorPages();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppointmentSchedulerDbContext>();

    DbInitializer.Seed(context);
}

app.Run();
