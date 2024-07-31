using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class EventObserver
{
    private Dictionary<Enum, Action<object>> callbacks = new Dictionary<Enum, Action<object>>();
    public void AddEvent(Enum e, Action<object> callback)
    {
        if (!callbacks.TryAdd(e, callback))
        {
            callbacks[e] = callback;
        }
    }
    public void RemoveEvent(Enum e)
    {
        callbacks.Remove(e);
    }
    public void ReceiveEvent(Enum e, object args)
    {
        if (callbacks.ContainsKey(e))
        {
            callbacks[e](args);
        }
    }
    private EventManager manager;
    public EventObserver(EventManager manager)
    {
        this.manager = manager;
        manager.AddObserver(this);
    }
    ~EventObserver()
    {
        manager.RemoveObserver(this);
    }
}

public class EventManager
{
    private List<EventObserver> observers = new List<EventObserver>();
    public void AddObserver(EventObserver observer)
    {
        observers.Add(observer);
    }
    public void RemoveObserver(EventObserver observer)
    {
        observers.Remove(observer);
    }
    public void PostEvent(Enum e, object args)
    {
        foreach (var observer in observers)
        {
            observer.ReceiveEvent(e, args);
        }
    }
    private static EventManager instance = null;
    public static EventManager GetInst()
    {
        if (instance == null)
        {
            instance = new EventManager();
        }
        return instance;
    }
}