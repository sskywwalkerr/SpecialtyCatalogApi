using SpecialtyCatalog.Client.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var apiBaseUrl = builder.Configuration["ApiBaseUrl"]!;

builder.Services.AddHttpClient<DirectionApiClient>(client => client.BaseAddress = new Uri(apiBaseUrl));
builder.Services.AddHttpClient<SpecialtyApiClient>(client => client.BaseAddress = new Uri(apiBaseUrl));
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
