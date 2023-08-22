using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using URLShort;
using URLShort.Models;

var builder = WebApplication.CreateBuilder(args);

string dbConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UrlContext>(options => options.UseSqlServer(dbConnection));
builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(dbConnection));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LogoutPath = new PathString("/");
                });

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<UrlContext>();
        var userContext = services.GetRequiredService<UserContext>();
        SampleData.Initialize(context, userContext);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error ocurred seeding to DB.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallback(async (UrlContext context, HttpContext ctx) =>
{
    string path = ctx.Request.Path.ToUriComponent().Trim('/');
    var url = await context.Urls.FirstOrDefaultAsync(
        x => x.ShortUrl.Trim() == path.Trim());

    if (url == null)
    {
        return Results.BadRequest("Invalid URL!");
    }

    return Results.Redirect(url.LongUrl);
});

app.Run();
