using System;
using System.Threading;

namespace Hw3;

public class SingleInitializationSingleton
{
    private static readonly object Locker = new();

    private static volatile bool _isInitialized = false;

    public const int DefaultDelay = 3_000;
    
    public int Delay { get; }

    private SingleInitializationSingleton(int delay = DefaultDelay)
    {
        Delay = delay;
        Thread.Sleep(delay);
    }

    public static void Reset()
    {
        if (!_isInitialized) return;
        lock (Locker)
        {
            if (!_isInitialized) return;
            _instance = new Lazy<SingleInitializationSingleton>(() => new SingleInitializationSingleton());
            _isInitialized = false;
        }
    }

    public static void Initialize(int delay)
    {
        if (!_isInitialized)
        {
            lock (Locker)
            {
                if (!_isInitialized)
                {
                    _instance = new Lazy<SingleInitializationSingleton>(() => new SingleInitializationSingleton(delay));
                    _isInitialized = true;
                }
                else throw new InvalidOperationException();
            }
        }
        else throw new InvalidOperationException();
    }
    private static Lazy<SingleInitializationSingleton> _instance =
        new Lazy<SingleInitializationSingleton>(() => new SingleInitializationSingleton());

    public static SingleInitializationSingleton Instance => _instance.Value;
}