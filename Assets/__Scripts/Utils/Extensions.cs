using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static void RemoveAllChildObjects(this GameObject parent)
    {
        int childCount = parent.transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(parent.transform.GetChild(i).gameObject);
        }
    }

    public static List<T> RandomItems<T>(this List<T> ItemList, int numberOfItems) where T: WeightedItem
    {
        List<T> items = new List<T>(ItemList);  
        List<T> newList = new();

        for (int i = 0; i < numberOfItems; i++)
        {
            List<ItemWeight> weightList = new();
            int totalWeight = 0;

            foreach (WeightedItem item in items)
            {
                totalWeight += item.Weight();
                weightList.Add(new ItemWeight(item, totalWeight));
            }
            int randomNumber = Random.Range(0, totalWeight);
            for (int j = 0; j < weightList.Count; j++)
            {
                if (weightList[j].Weight >= randomNumber)
                {
                    newList.Add((T)weightList[j].Item);
                    items.RemoveAt(j);
                    break;
                }
            }
        }
        return newList;
    }
}

public class ItemWeight
{
    public WeightedItem Item;
    public int Weight;

    public ItemWeight(WeightedItem item, int weight)
    {
        Item = item;
        Weight = weight;
    }
}
