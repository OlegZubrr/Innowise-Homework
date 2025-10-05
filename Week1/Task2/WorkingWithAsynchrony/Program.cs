namespace WorkingWithAsynchrony;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("=== Synchronous processing ===");
        var watchSync = System.Diagnostics.Stopwatch.StartNew();
        
        Console.WriteLine(ProcessData("File 1"));
        Console.WriteLine(ProcessData("File 2"));
        Console.WriteLine(ProcessData("File 3"));
        
        watchSync.Stop();
        
        Console.WriteLine($"Synchronous execution took: {watchSync.ElapsedMilliseconds / 1000.0} sec.\n");


        Console.WriteLine("=== Asynchronous processing ===");
        var watchAsync = System.Diagnostics.Stopwatch.StartNew();
        
        Task<string> t1 = ProcessDataAsync("File 1");
        Task<string> t2 = ProcessDataAsync("File 2");
        Task<string> t3 = ProcessDataAsync("File 3");
        
        
        var results = await Task.WhenAll(t1, t2, t3);
        
        foreach (var result in results)
        {
            Console.WriteLine(result);
        }
        
        watchAsync.Stop();
        Console.WriteLine($"Asynchronous execution took: {watchAsync.ElapsedMilliseconds / 1000.0} sec.\n");

    }

    static string ProcessData(string dataName)
    {
        Thread.Sleep(3000);
        return $"Processing {dataName} completed in 3 seconds";
    }

    static async Task<string>  ProcessDataAsync(string dataName)
    {
        await Task.Delay(3000);
        return $"Async processing {dataName} completed in 3 seconds";
    }
}