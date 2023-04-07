using Web.CounterQueueExample.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Configuration
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName.ToLower()}.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

// Add service options
builder.Services.AddOptions<CounterQueueOptions>()
    .Bind(builder.Configuration.GetRequiredSection(CounterQueueOptions.CounterQueue))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<CounterServiceOptions>()
    .Bind(builder.Configuration.GetRequiredSection(CounterServiceOptions.CounterService))
    .ValidateDataAnnotations()
    .ValidateOnStart();

// Add counter services
builder.Services.AddHostedService<CounterService>();
builder.Services.AddSingleton<IHostedServiceAccessor<CounterService>, HostedServiceAccessor<CounterService>>();
builder.Services.AddSingleton<ICounterQueue, CounterQueue>();

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
