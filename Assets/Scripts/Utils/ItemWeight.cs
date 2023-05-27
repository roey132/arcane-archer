using System.Collections;
using UnityEngine;


public class ItemWeight : MonoBehaviour
{
    public WeightedItem Item;
    public int Weight;

    public ItemWeight(WeightedItem item) 
    {
        Item = item;
        Weight = item.Weight();
    }

}
