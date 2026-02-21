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
builder.Services.AddScoped<HyperShortsCache>();
builder.Services.AddScoped<HyperShortsService>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "hypershort:";
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseForwardedHeaders();
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.MapGet("/{shortCode}", async (string shortCode, HyperShortsService service) =>
{
    var hyperShort = await service.GetLongUrl(shortCode);

    if (string.IsNullOrWhiteSpace(hyperShort))
    {
        return Results.NotFound();
    }

    return Results.Redirect(hyperShort, permanent: true);
});

app.Run();
