using Appointment_Scheduler.Data;
using Appointment_Scheduler.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppointmentSchedulerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:AppointmentSchedulerDbContextConnection"]);
});



var app = builder.Build();

app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapDefaultControllerRoute();

app.Run();
