using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Enumberables
{
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
    {
        if (enumerable == null)
            return true;

        return !enumerable.Any();
    }
}

