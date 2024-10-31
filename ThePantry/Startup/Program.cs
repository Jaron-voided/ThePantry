//using Microsoft.Data.SqlClient;
using ThePantry.Data;
using ThePantry.Data.Repositories;
using ThePantry.Data.SqlServer;
using ThePantry.Services.Ingredient;
using ThePantry.Services.Measurement;
using ThePantry.Services.Recipe;

var builder = WebApplication.CreateBuilder(args);

// CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder
            .WithOrigins("http://localhost:5173")
            //.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
            //.AllowCredentials());
});

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

app.UseCors("AllowReactApp");

app.UseAuthorization();
app.MapControllers();
app.Run();
