using System;
using System.Collections.Generic;
using System.Linq;

public static class SystemCollectionExtension
{
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source)
        {
            action(item);
        }
    }

    public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            collection.Add(item);
        }
    }

    public static T RandomElement<T>(this IList<T> list)
    {
        if (list.Count == 0) throw new InvalidOperationException("The list is empty.");
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public static T RandomElement<T>(this T[] array)
    {
        if (array.Length == 0) throw new InvalidOperationException("The array is empty.");
        return array[UnityEngine.Random.Range(0, array.Length)];
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        return source.OrderBy(item => UnityEngine.Random.value);
    }
}