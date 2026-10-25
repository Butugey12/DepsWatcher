using System.CommandLine;
using System.Runtime.CompilerServices;
using DepsWatcher.cli;

var commandResult = CommandLine.Create(args);
var (path, command, debounce) = commandResult;

Console.WriteLine($"The path is {path}");
Console.WriteLine($"The command is {command}");
Console.WriteLine($"The debounce value is {debounce}");

Timer? timer = null;
using var watcher = new FileSystemWatcher(path);

watcher.NotifyFilter = NotifyFilters.LastWrite;
watcher.EnableRaisingEvents = true;
watcher.IncludeSubdirectories = true;

// watcher.Created += OnCreated;
watcher.Changed += OnChanged;

Console.WriteLine($"Watching {path}. Press Enter to Exit");
Console.WriteLine($"Main Thread: {Environment.CurrentManagedThreadId}");
Console.ReadLine();

[MethodImpl(MethodImplOptions.NoInlining)]
static void OnCreated(object sender, FileSystemEventArgs e)
{
    var value = $"created {e.FullPath}";
    Console.WriteLine(value);
}

[MethodImpl(MethodImplOptions.NoInlining)]
void OnChanged(object sender, FileSystemEventArgs e)
{
    Console.WriteLine($"OnChanged: {Environment.CurrentManagedThreadId}");
    Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff"));
    if (timer == null)
        timer = new Timer(new TimerCallback(WriteToConsole), e, 300, Timeout.Infinite);
    else
        timer.Change(300, Timeout.Infinite);
}

[MethodImpl(MethodImplOptions.NoInlining)]
void WriteToConsole(object? state)
{
    Console.WriteLine($"Timer: {Environment.CurrentManagedThreadId}");
    var e = state as FileSystemEventArgs;
    
    Console.WriteLine("Changed!!");
    Console.WriteLine(e?.Name);
    Console.WriteLine(e?.ChangeType);
    Console.WriteLine(e?.FullPath);
    // maybe send a command?
    timer?.Dispose();
}



