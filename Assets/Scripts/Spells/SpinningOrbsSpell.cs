using UnityEngine;

[CreateAssetMenu(fileName = "BaseSpell", menuName = "Spells/SpinningOrbsSpell")]
public class SpinningOrbsSpell : SpellData
{
    public int NumOfOrbs;
    public float SpinRadius;
    public float SpinSpeed;
    public GameObject OrbPrefab;
    private GameObject[] _orbs;

    public override GameObject Activate(Vector2 center, Transform playerTransform)
    {
        _orbs = new GameObject[NumOfOrbs];
        GameObject orbHolder = Instantiate(new GameObject("orbHolder"), playerTransform); ;

        for (int i = 0; i < NumOfOrbs; i++)
        {
            float angle = i * Mathf.PI * 2f / NumOfOrbs;
            Vector3 spawnPos = playerTransform.position + new Vector3(Mathf.Cos(angle) * SpinRadius, Mathf.Sin(angle) * SpinRadius, 0f);
            _orbs[i] = Instantiate(OrbPrefab, spawnPos, Quaternion.identity, orbHolder.transform);
            _orbs[i].GetComponent<OrbSpell>().InitOrb(playerTransform, SpinSpeed, SpinRadius, this.SpellDamage);
        }

        return orbHolder;
    }
    public override void DeleteSpell(GameObject spellObject)
    {
        Destroy(spellObject);
    }
}
