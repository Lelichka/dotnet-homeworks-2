using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Hw7.ErrorMessages;
using Hw7.Models;

namespace Hw7;

[ExcludeFromCodeCoverage]
public class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

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
            pattern: "{controller=Home}/{action=UserProfile}/{id?}");

        app.Run();
    }
}