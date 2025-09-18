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

// Bind MongoDbSettings from environment variable
builder.Services.Configure<MongoDbSettings>(options =>
{
    options.ConnectionString = builder.Configuration["MONGO_URI"];
    options.DatabaseName = "PortfolioDb";           // You can change DB name if needed
    options.ContactCollectionName = "Contacts";     // Collection name
});

// Register repository
builder.Services.AddSingleton<ContactRepository>();

// Add controllers
builder.Services.AddControllers();

// Enable CORS for all origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// Swagger for API testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger in development (optional)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Render sets dynamic port in environment variable PORT
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://*:{port}");

// Use CORS
app.UseCors("AllowAll");

// Authorization (if used)
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Run the app
app.Run();
