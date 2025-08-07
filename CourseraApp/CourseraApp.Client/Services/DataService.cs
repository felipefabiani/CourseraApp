namespace CourseraApp.Client.Services;

public class DataService
{
    public async Task<List<string>> GetProductAsync()
    {
        
        await Task.Delay(500);
        
        // Return some dummy data
        return new List<string> { "Product 1", "Product 2", "Product 3" };
    }
}
