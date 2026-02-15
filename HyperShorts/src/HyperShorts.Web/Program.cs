using HyperShorts.Web.Core;
using HyperShorts.Web.Data;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var connString = builder.Configuration.GetConnectionString("HyperShortsDb");
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite(connString));

builder.Services.AddScoped<HyperShortsRepository>();
builder.Services.AddScoped<HyperShortsService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseForwardedHeaders();
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.MapGet("/{shortCode}", async (string shortCode, AppDbContext dbContext) =>
{
    var x = await dbContext.HyperShorts.FirstOrDefaultAsync(hs => hs.Code == shortCode);

    if (x == null)
    {
        return Results.NotFound();
    }

    return Results.Redirect(x.LongUrl, permanent: true);
});

app.Run();
