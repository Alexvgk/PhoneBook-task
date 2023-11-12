using PhoneBook_task.Models.BaseModel;
using PhoneBook_task.Repository;
using PhoneBook_task.Services;
using Microsoft.EntityFrameworkCore;
using PhoneBook_task.Models;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(ContactsMappingProfile));
builder.Services.AddDbContext<ContactDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);
builder.Services.AddScoped<IBaseRepository<Contact>, BaseRepository<Contact>>();


var app = builder.Build();

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
    pattern: "{controller=Contact}/{action=All}");

app.Run();
