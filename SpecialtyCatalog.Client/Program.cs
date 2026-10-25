using SpecialtyCatalog.Client.Services;

var builder = WebApplication.CreateBuilder(args);

// контроллеры и страницы
builder.Services.AddControllersWithViews();
var apiBaseUrl = builder.Configuration["ApiBaseUrl"]!;

builder.Services.AddHttpClient<DirectionApiClient>(client => client.BaseAddress = new Uri(apiBaseUrl));
builder.Services.AddHttpClient<SpecialtyApiClient>(client => client.BaseAddress = new Uri(apiBaseUrl));

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
