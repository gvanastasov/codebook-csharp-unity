using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static void InsertChild(this Transform parent, Transform newChild, int idx)
    {
        // cache
        var children = new List<Transform>();
        for (int i = 0; i < parent.childCount; i++)
        {
            var child = parent.GetChild(i);
            children.Add(child);
        }

        // unparent
        foreach (var child in children)
        {
            child.SetParent(null);
        }

        // insert
        if (idx >= children.Count)
        {
            children.Add(newChild.transform);
        }
        else
        {
            children.Insert(idx, newChild.transform);
        }

        // re-parent
        for (int i = 0; i < children.Count; i++)
        {
            var child = children[i];
            child.SetParent(parent);
            child.SetSiblingIndex(i);
        }
    }
}