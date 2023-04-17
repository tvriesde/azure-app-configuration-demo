using appConfigDemo;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

//retrieve connection string
string connectionString = builder.Configuration.GetConnectionString("AppConfig");

//load configiation from Azure App Configuration
//builder.Configuration.AddAzureAppConfiguration(connectionString);

// Add services to the container.
builder.Services.AddRazorPages();

// Load configuration from Azure App Configuration
builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect(connectionString)
           // Load all keys that start with `TestApp:` and have no label
           .Select("TestApp:*",  LabelFilter.Null)
           // Configure to reload configuration if the registered sentinel key is modified
           .ConfigureRefresh(refreshOptions =>
                refreshOptions.Register("TestApp:Settings:Sentinel", refreshAll: true));

    // Load all feature flags with no label
    options.UseFeatureFlags(featureFlagOptions =>
    {
        featureFlagOptions.Select("*", "dev");
    });
});

// Add feature management to the container of services.
builder.Services.AddFeatureManagement();

// Bind config
builder.Services.Configure<Settings>(builder.Configuration.GetSection("TestApp:Settings"));

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
