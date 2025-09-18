//using Portfolio;

//var builder = WebApplication.CreateBuilder(args);
//// Bind MongoDbSettings from appsettings.json
//builder.Services.Configure<MongoDbSettings>(
//    builder.Configuration.GetSection("MongoDbSettings"));

//// Register repository
//builder.Services.AddSingleton<ContactRepository>();
//// Add services to the container.

//builder.Services.AddControllers();
//// Add CORS
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", policy =>
//        policy.AllowAnyOrigin()
//              .AllowAnyMethod()
//              .AllowAnyHeader());
//});
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
using Portfolio;

var builder = WebApplication.CreateBuilder(args);

// Get MongoDB URI from environment variable
var mongoConnection = Environment.GetEnvironmentVariable("MONGO_URI")
                      ?? builder.Configuration.GetSection("MongoDbSettings:ConnectionString").Value;

builder.Services.Configure<MongoDbSettings>(options =>
{
    options.ConnectionString = mongoConnection;
    options.DatabaseName = builder.Configuration.GetSection("MongoDbSettings:DatabaseName").Value;
    options.ContactCollectionName = builder.Configuration.GetSection("MongoDbSettings:ContactCollectionName").Value;
});

// Register repository
builder.Services.AddSingleton<ContactRepository>();

// Add services to the container
builder.Services.AddControllers();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();

// Dynamic port binding for Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");

app.Run();
