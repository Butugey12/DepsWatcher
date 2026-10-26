namespace DepsWatcher.Process;
using CliWrap;
public static class Shell
{
    // what do i need?
    // 
    public static async void ExecuteInShell(string[] path, string[] commands)
    {
        var result = await Cli.Wrap(commands[0])
            .WithWorkingDirectory(path[0])
            .ExecuteAsync();
        
    }
}