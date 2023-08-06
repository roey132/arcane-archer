using System.Collections;
using UnityEngine;


public class FireCycloneSpell : AbstractSpell
{
    [SerializeField] private SpellData _spellData;
    [SerializeField] private float _cycloneDuration;
    [SerializeField] private GameObject _cycloneObject;
    [SerializeField] private Transform _player;

    private float _spellTimer;

    public override void SpellBehaviour()
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        _spellTimer = 0f;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        _spellTimer -= Time.deltaTime;

        if (_spellTimer < 0)
        {
            _spellTimer = _spellData.Cooldown;
            StartCoroutine(SpawnCyclone());
        }
    }

    private IEnumerator SpawnCyclone()
    {
        GameObject currCyclone = Instantiate(_cycloneObject,_player.transform.position,Quaternion.identity,GameManager.Instance.SpellCollector);
        currCyclone.GetComponent<FireCycloneBehaviour>().SetDamage(_spellData.SpellDamage);
        yield return new WaitForSeconds(_cycloneDuration);
        Destroy(currCyclone);
    }
}
