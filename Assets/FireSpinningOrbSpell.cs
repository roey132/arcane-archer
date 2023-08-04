using UnityEngine;

public class FireSpinningOrbSpell : AbstractSpell
{
    [SerializeField] private SpellData _spellData;
    [SerializeField] private int _numOfOrbs;
    [SerializeField] private float _spinRadius;
    [SerializeField] private float _spinSpeed;
    [SerializeField] private GameObject _orbPrefab;
    [SerializeField] private GameObject[] _orbs;

    private bool isActive;
    private Transform _player;
    private GameObject _orbHolder;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        if (!isActive) return;
        _orbHolder.transform.position = _player.transform.position;
    }
    public override void SpellBehaviour()
    {
        _orbs = new GameObject[_numOfOrbs];
        _orbHolder = new GameObject("orbHolder");
        _orbHolder.transform.SetParent(GameManager.Instance.SpellCollector);

        for (int i = 0; i < _numOfOrbs; i++)
        {
            float angle = i * Mathf.PI * 2f / _numOfOrbs;
            Vector3 spawnPos = _orbHolder.transform.position + new Vector3(Mathf.Cos(angle) * _spinRadius, Mathf.Sin(angle) * _spinRadius, 0f);
            _orbs[i] = Instantiate(_orbPrefab, spawnPos, Quaternion.identity, _orbHolder.transform);
            _orbs[i].GetComponent<FireOrbSpellObject>().InitOrb(_orbHolder.transform, _spinSpeed, _spinRadius, _spellData.SpellDamage);
        }
    }

    private void OnEnable()
    {
        SpellBehaviour();
        isActive = true;
    }
    private void OnDisable()
    {
        isActive = false;
    }
}
