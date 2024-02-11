using Appointment_Scheduler.Data;
using Appointment_Scheduler.Models;
using Appointment_Scheduler.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppointmentSchedulerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:AppointmentSchedulerDbContextConnection"]);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppointmentSchedulerDbContext>();

    DbInitializer.Seed(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapDefaultControllerRoute();

app.Run();
