using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu]
public class DebuffData : ScriptableObject
{
    public DebuffElement Element;
    public float DebuffTime;
    public float DebuffTickTime;
    public List<string> DebuffValueKeys;
    public List<float> DebuffValues;
    public Color debuffColor;

    public Dictionary<string, float> DebuffEffects = new();

    public void OnValidate()
    {
        for (int i =0; i < DebuffValueKeys.Count; i++)
        {
            DebuffEffects.Add(DebuffValueKeys[i], DebuffValues[i]);
        }
    }
}
