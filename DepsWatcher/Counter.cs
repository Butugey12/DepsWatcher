namespace DepsWatcher;


public class ThresholdReachedEventArgs : EventArgs
{
    public int Threshold { get; set; }
    public DateTime TimeReached { get; set; }
}
public class Counter(int passedThreshold)
{
    private int _threshold = passedThreshold;
    
    public event EventHandler<ThresholdReachedEventArgs>ThresholdReached;
    // similar to

    private int _count = 0;

    public void Increment(int value)
    {
        _count =+ value;
        if (_count >= _threshold)
        {
            var args = new ThresholdReachedEventArgs();
            args.Threshold = _threshold;
            args.TimeReached = DateTime.Now;
            OnThresholdReached((args));
        }
        
        
    }

    protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
    {
        ThresholdReached?.Invoke(this, e);
    }
}

public class Subscriber
{
    public void subscriber(object sender, ThresholdReachedEventArgs e)
    {
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"The threshold of {e.Threshold} was reached at {e.TimeReached}.");
        Environment.Exit(0);
    }
}