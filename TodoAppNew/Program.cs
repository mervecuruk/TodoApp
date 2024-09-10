using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoAppNew.Contexts;
using TodoAppNew.Maps;
using TodoAppNew.Models;
using TodoAppNew.Services;

namespace TodoAppNew
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConn")).UseLazyLoadingProxies());

            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric= false;


                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters= "abcdefghijklmnopqrstuvwxyz0123456789_@.";

            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddAutoMapper(typeof(Mapping));


            //service i ayaða kaldýrdýk.
            builder.Services.AddScoped<IToDoItemService, ToDoItemService>();


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

            app.UseAuthentication();    //Kimliklendirme
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}