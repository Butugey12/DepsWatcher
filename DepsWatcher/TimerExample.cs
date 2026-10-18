using System.Threading;

namespace DepsWatcher;
public class TimerExample
{
    public static Timer _timer;

    public static void Main(string[] args)
    {
        // create a timer that invokes the callback method every minute, after the initial delay of 10 seconds
        _timer = new Timer(new TimerCallback(CallBackFunc), null, 5000, 10000);
        
        Console.WriteLine("Press [Enter] to exist the Program");
        Console.ReadLine();
    }

    // this is the callback unction executed by the timer, for now it just logs a random number 
    // and it logs the current time to the console
    private static void CallBackFunc(object? state)
    {
        var random = new Random();
        Console.WriteLine($"The Random number is {random.Next(100)}");
        Console.WriteLine($"Callback Executed at {DateTime.Now.ToString("HH:m:s zzz")}");
        
    }
}