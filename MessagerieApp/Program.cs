using MessagerieApp.Data;
using MessagerieApp.Models;
using MessagerieApp.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<IGenericRepository<Ressource>, RessourceRepository>();

builder.Services.AddScoped<DatabaseConnection>();
builder.Services.AddScoped<RessourceRepository>();
// Add other repositories as needed
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
