using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData",menuName = "Enemy/BaseEnemyData")]
    public class EnemyData : ScriptableObject
    {
        public string EnemyName;
        public float MinHealth;
        public float MinMovementSpeed;
        public float DistanceFromPlayer;
        public float SpawnValue;
        public float Level;
        public float MinCurrencyValue;
        public float MaxCurrencyValue;
        public Sprite EnemySprite;
        public Animator Animator;

        public virtual void EnemyBehaviour()
        {

        }

    }
