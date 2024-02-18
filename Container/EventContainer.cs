using System.Collections.Generic;
using My.Event;

//public interface IEventContainer
//{
//    CustomEvent GetEvent(string key);
//}

//public class NullEventContainer : IEventContainer
//{
//    public CustomEvent GetEvent(string key)
//    {
//        return null;
//    }
//}

/// <summary>
/// ittan
/// </summary>

public static class EventContainer
{
#if UNITY_WEBGL
    static Dictionary<string, CustomEvent<long>> events = new Dictionary<string, CustomEvent<long>>();
#else
    static Dictionary<string, CustomEvent<long>> events = new Better.Dictionary<string, CustomEvent<long>>();
#endif

    static public CustomEvent<long> GetEvent(string key)
    {
        if (!events.TryGetValue(key, out var value))
        {
            value = new CustomEvent<long>();
            value.AddListener((_) => YDebugger.Log($"{key} : {_}"));
            events.Add(key, value);
        }
        return value;
    }
}
