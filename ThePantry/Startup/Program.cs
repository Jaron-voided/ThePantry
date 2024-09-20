using ThePantry.Data.SqlServer;

class Program
{
    static void Main(string[] args)
    {
        // Create an instance of the database class
        var pantryDatabase = new SqlPantryDatabase();
        
        // Call the method to create all tables
        pantryDatabase.CreateAllTables();
        
        Console.WriteLine("All tables created successfully.");
    }
}