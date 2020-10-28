using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Extensions
{
    public static T[] SubArray<T>(this T[] array, int offset, int length)
    {
        T[] result = new T[length];
        Array.Copy(array, offset, result, 0, length);
        return result;
    }

    /*
    public static int NormalNext(this Random rnd, int Steps, int MinValue, int MaxValue)
    {
        int count = 0;
        int val = MinValue;

        if (Steps < 1) return 0;

        while (++count * Steps <= MaxValue) val += rnd.Next(Steps);

        return val;
    }
    */

    public static int NormalNext(this System.Random rnd, int minVal, int maxVal)
    {
        int averageTimes = 2;
        int val = 0;
        for (int i = 0; i<averageTimes; i++)
        {
            val += rnd.Next(minVal, maxVal);
        }
        return val / averageTimes;
    }

    public static T GetComponentInChildWithTag<T>(this GameObject parent, string tag) where T : Component
    {
        Transform t = parent.transform;
        foreach (Transform tr in t)
        {
            if (tr.CompareTag(tag))
            {
                return tr.GetComponent<T>();
            }
        }
        return null;
    }

    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }


    //----------Order canvas elements----------

    public static void SortChildren(this GameObject o)
    {
        var children = o.GetComponentsInChildren<Transform>(true).ToList();
        children.Remove(o.transform);
        children.Sort(Compare);
        for (int i = 0; i < children.Count; i++)
            children[i].SetSiblingIndex(i);
    }

    private static int Compare(Transform lhs, Transform rhs)
    {
        if (lhs == rhs) return 0;
        var test = rhs.gameObject.activeInHierarchy.CompareTo(lhs.gameObject.activeInHierarchy);
        if (test != 0) return test;
        if (lhs.localPosition.z > rhs.localPosition.z) return 1;
        if (lhs.localPosition.z < rhs.localPosition.z) return -1;
        return 0;
    }

}