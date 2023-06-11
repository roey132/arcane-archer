using UnityEngine;

[CreateAssetMenu]
public class WaveData : ScriptableObject
{
    public int MinNumberOfEnemies;
    public int MaxNumberOfEnemies;
    public float MinEnemySpeed;
    public float MaxEnemySpeed;
    public int MinEnemyHealth;
    public int MaxEnemyHealth;
    public int MinEnemyValue;
    public int MaxEnemyValue;
}