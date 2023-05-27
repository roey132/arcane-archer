using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static List<WeightedItem> RandomItems<WeightedItem>(this List<WeightedItem> list, int numberOfItems)
    {
        List<WeightedItem> tempList = new List<WeightedItem>(list);
        List<WeightedItem> newList = new();
        int totalWeight = 0;

        // TODO implement item selector using weighted items (ItemWeight class)
        // give each item a weight and select a random item using the weight, remove the item each time and add it to newList that returns all selected values
        // itertate for numberOfItems.

        return newList;
    }
}