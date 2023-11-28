using System;
using System.Collections.Generic;
using System.Linq;

public static class ListExtensions
{
    public static List<T> ChooseRandom<T>(this List<T> list, int amount)
    {
        List<T> result = new List<T>(list);

        Random random = new Random();
        result = result.OrderBy(x => random.Next()).ToList();

        return result.GetRange(0, amount);
    }
}