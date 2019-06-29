using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventConst
{
    public const string EVENT_MISS = "eventMiss";
    public const string EVENT_ENTER = "eventEnter";
    public const string EVENT_CLICK = "eventClick";
    public const string EVENT_SCORE = "eventScore";
}

public class EventManager : MonoSingleton<EventManager> {

    private Dictionary<string, List<Action<object[]>>> eventPool = new Dictionary<string, List<Action<object[]>>>();

    public void AddListener(string eventName, Action<object[]> action)
    {
        if (eventPool.ContainsKey(eventName))
        {
            eventPool[eventName].Add(action);
        }
        else
        {
            eventPool.Add(eventName, new List<Action<object[]>>() { action });
        }
    }

    public void RemoveListener(string eventName, Action<object[]> action)
    {
        if (eventPool.ContainsKey(eventName))
        {
            eventPool[eventName].Remove(action);
        }
    }

    public void Dispatch(string eventName, params object[] data)
    {
        if (eventPool.ContainsKey(eventName))
        {
            foreach (var item in eventPool[eventName])
            {
                item.Invoke(data);
            }
        }
    }


}
