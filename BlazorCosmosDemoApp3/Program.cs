using BlazorCosmosDemoApp3.Components;
using BlazorCosmosDemoApp3.Services;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//To do : impliment throuth interface
builder.Services.AddScoped<EmployeeService>(sp =>
{
    var conn = builder.Configuration["CosmosDB:Endpoint"];
    var key = builder.Configuration["CosmosDB:Key"];
    var dataBaseName = builder.Configuration["CosmosDB:Database"];
    var containerName = builder.Configuration["CosmosDB:Container"];
    
    return new EmployeeService(conn, key, dataBaseName, containerName);
});

builder.Services.AddRadzenComponents();

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
