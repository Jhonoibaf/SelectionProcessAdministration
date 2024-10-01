using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using Recruiters.Application.CandidatesAdministration.Queries;
using Recruiters.Infraestructure.Data;
using Recruiters.Application.Mappers;
using FluentValidation;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Recruiters.Application.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AplicationDbConection")) 
);

builder.Services.AddValidatorsFromAssemblyContaining<CandidateDtoValidator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetCandidateByIdQuery).Assembly));


builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dataContext.Database.Migrate();
}

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
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
