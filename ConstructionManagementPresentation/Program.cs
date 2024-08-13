using ConstructionManagementPresentation.HttpClientConfiguration;
using ConstructionManagementPresentation.Services;
using System.Net.Http.Headers;


var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddHttpClient<ActivityService>(client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7026/");
//});


//builder.Services.AddHttpClient<WorkerService>(client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7026/");
//});



builder.Services.AddCustomHttpClient<ActivityService>(new HttpClientSettings().BaseAddress);
builder.Services.AddCustomHttpClient<WorkerService>(new HttpClientSettings().BaseAddress);

builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7026/");
    client.DefaultRequestHeaders.Accept.Clear(); 
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
});


builder.Services.AddScoped< AuthService>();

//sesion
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
    options.Cookie.HttpOnly = true; 
    options.Cookie.IsEssential = true; 
});


// Add services to the container.
builder.Services.AddControllersWithViews();

 



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

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=auth}/{action=login}/{id?}")
    ;


app.Run();
