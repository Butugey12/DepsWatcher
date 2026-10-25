using System.CommandLine;

namespace DepsWatcher.cli;

public record CliResult(string Path, string Command, int? DebounceVaue);
public static class CommandLine
{
    public static CliResult Create(string[] args)
    {
        
        Option<string> watch = new("--watch")
        {
            Description = "The path to Watch",
            Required = true,
            Aliases = { "-w" },
    
        };
        watch.Validators.Add(result =>
        {
            var path = result.GetValueOrDefault<string>();
            if (!Directory.Exists(path))
                result.AddError($"Directory '{path}' does not exist");
            if (Directory.GetCurrentDirectory() == Directory.GetDirectoryRoot(path))
                result.AddError($"You cannot watch dep-watcher itself");
        });

        Option<string> cmdOption = new("--cmd")
        {
            Description = "The command to run in the shell",
            Required = true,
            Aliases = { "-c" }
        };

        Option<int> debounce = new("--debounce")
        {
            Description = "Wait time in ms, default is 500ms",
            DefaultValueFactory = parseResult => 500,
            Aliases = {"-d"},
        };

        debounce.Validators.Add(result =>
        {
            if (result.GetValueOrDefault<int>() < 0)
                result.AddError("--debounce must be 0 or more");
        });

        RootCommand rootCommand = new("Dependency Watcher ") { watch, cmdOption, debounce };
        
        ParseResult parseResult = rootCommand.Parse(args); // dep-watcher --watch value --cmd X --debounce 300  
        
        

        var path = parseResult.GetRequiredValue(watch);
        var cmd = parseResult.GetRequiredValue<string>(cmdOption);
        int? debounceValue = parseResult.GetValue(debounce);

        Console.WriteLine($"The Path is {path}");
        Console.WriteLine($"The cmd is {cmd}");
        Console.WriteLine($"The Debounce value is {debounceValue}");

        return new CliResult(path, cmd, debounceValue);
    }
}