namespace DepsWatcher;

public class FileWatcher
{
    private static void OnCreated(object sender, FileSystemEventArgs e)
    {
        string value = $"created {e.FullPath}";
        Console.WriteLine(value);
    }
    
    
    

}