if (args.Length == 0)
{
    Console.WriteLine("Usage: DepsWatcher <path> [options]");
    return;
}

Timer timer = null;
var path = args[0];
using var watcher = new FileSystemWatcher(path);

watcher.NotifyFilter = NotifyFilters.LastWrite;
watcher.EnableRaisingEvents = true;
watcher.IncludeSubdirectories = true;

// watcher.Created += OnCreated;
watcher.Changed += OnChanged;

Console.WriteLine($"Watching {path}. Press Enter to Exit");
Console.WriteLine($"Main: {Environment.CurrentManagedThreadId}");
Console.ReadLine();


static void OnCreated(object sender, FileSystemEventArgs e)
{
    var value = $"created {e.FullPath}";
    Console.WriteLine(value);
}

void OnChanged(object sender, FileSystemEventArgs e)
{
    Console.WriteLine($"OnChanged: {Environment.CurrentManagedThreadId}");
    Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff"));
    if (timer == null)
        timer = new Timer(new TimerCallback(WriteToConsole), e, 300, Timeout.Infinite);
    else
        timer.Change(300, Timeout.Infinite);
}

void WriteToConsole(object? state)
{
    Console.WriteLine($"Timer: {Environment.CurrentManagedThreadId}");
    var e = state as FileSystemEventArgs;
    
    Console.WriteLine("Changed!!");
    Console.WriteLine(e?.Name);
    Console.WriteLine(e?.ChangeType);
    Console.WriteLine(e?.FullPath);
    timer?.Dispose();
    
}