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
    options.DatabaseName = "PortfolioDb";
    options.ContactCollectionName = "Contacts";
});

builder.Services.AddSingleton<ContactRepository>();

builder.Services.AddControllers();

// CORS for all origins
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

// Disable HTTPS redirection on Render
// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
