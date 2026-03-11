using System.Security.Cryptography.X509Certificates;
using KucniSavetBackend.Interfaces.Repositories;
using KucniSavetBackend.Interfaces.Services;
using KucniSavetBackend.Repositories.RavenDB;
using KucniSavetBackend.Services;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IDocumentStore>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    X509Certificate2 clientCertificate = new("./Secret/cert.pfx");

    var store = new DocumentStore
    {
        Certificate = clientCertificate,
        Urls = configuration.GetSection("RavenDb:Urls").Get<string[]>(),
        Database = configuration["RavenDb:Database"]
    };

    store.Initialize();
    return store;
});

builder.Services.AddScoped<IAsyncDocumentSession>(sp =>
{
    var store = sp.GetRequiredService<IDocumentStore>();
    return store.OpenAsyncSession();
});

builder.Services.AddScoped<IHouseholdRepository, HouseholdRepository>();
builder.Services.AddScoped<IHouseholdService, HouseholdService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
