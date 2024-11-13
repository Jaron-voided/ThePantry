//using Microsoft.Data.SqlClient;
using ThePantry.Data;
using ThePantry.Data.Repositories;
using ThePantry.Data.SqlServer;
using ThePantry.Services.Ingredient;
using ThePantry.Services.Measurement;
using ThePantry.Services.Recipe;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin() // Allow all origins
            .AllowAnyMethod() // Allow all HTTP methods (GET, POST, etc.)
            .AllowAnyHeader()); // Allow all headers
});


// CORS policy
/*builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder => builder
            .AllowAnyOrigin()
            //.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
            //.AllowCredentials());
});*/

/*builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://pantry-frontend-webapp.azurewebsites.net")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});*/

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories and services for dependency injection
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IMeasurementRepository, MeasurementRepository>();
builder.Services.AddScoped<IMeasurementService, MeasurementService>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IRecipeService, RecipeService>();

// Register SqlConnection as a scoped dependency
//builder.Services.AddScoped(provider => 
builder.Services.AddSingleton(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DevelopmentDatabase");
    /*var connection = new SqlConnection(connectionString);

    connection.Open(); // Ensure the connection is open when the scope is created

    return connection;*/
    return new DB(connectionString);
});

var app = builder.Build();

//app.UseCors("AllowFrontend");
app.UseCors("AllowAll");

// Create tables when the application starts**
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    /*var configuration = services.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DevelopmentDatabase");

    using (var connection = new SqlConnection(connectionString))
    {
        connection.Open();
        var sqlPantryDatabase = new SqlPantryDatabase();
        sqlPantryDatabase.CreateAllTables();
    }*/
    var db = services.GetRequiredService<DB>();
    var sqlPantryDatabase = new SqlPantryDatabase(db);
    sqlPantryDatabase.CreateAllTables();
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();


app.UseAuthorization();
app.MapControllers();
app.Run();
