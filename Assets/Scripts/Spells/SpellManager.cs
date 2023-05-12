using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public static SpellManager instance;
    [SerializeField] private SpellData _spellData;
    private List<ActiveSpell> _activeSpellsList = new List<ActiveSpell>();
    private List<PassiveSpell> _passiveSpellsList = new List<PassiveSpell>();

    private Transform _playerTransform;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        _playerTransform = transform.parent;

    }

    // Update is called once per frame
    void Update()
    {
        if (_activeSpellsList.Count == 0 && _passiveSpellsList.Count == 0) return;

        foreach (ActiveSpell activeSpell in _activeSpellsList)
        {
            activeSpell.Timer -= Time.deltaTime;
            if (activeSpell.Timer <= 0)
            {
                GameObject spell = activeSpell.Spell.Activate(_playerTransform.position, _playerTransform);

                StartCoroutine(DestroyEffect(spell, activeSpell.Spell._effectDurationMilliSeconds));
                activeSpell.Timer = activeSpell.Spell.Cooldown;
            }
        }

        foreach (PassiveSpell passiveSpell in _passiveSpellsList)
        {
            if (!passiveSpell.IsActive)
            {
                passiveSpell.IsActive = true;
                passiveSpell.Spell.Activate(_playerTransform.position, _playerTransform);
            }
        }


    }
    public void addSpell(SpellData spell)
    {
        if (spell.isActiveSpell)
        {
            _activeSpellsList.Add(new ActiveSpell(spell));
        }
        else
        {
            _passiveSpellsList.Add(new PassiveSpell(spell));
        }
    }
    private IEnumerator DestroyEffect(GameObject effect, float duration)
    {
        yield return new WaitForSeconds(duration / 1000);
        print("destroy");
        Destroy(effect);
    }
}

// class to manage active spells, spells that create and destroy particles and hitboxes
public class ActiveSpell
{
    public float Timer;
    public SpellData Spell;
    public string SpellName;

    public ActiveSpell(SpellData spell)
    {
        Spell = spell;
        Timer = spell.Cooldown;
        SpellName = spell.name;
    }
}
// class to manage passive spells, spells that are created and stay active until disactivated
public class PassiveSpell
{
    public bool IsActive;
    public string SpellName;
    public SpellData Spell;

    public PassiveSpell(SpellData spell)
    {
        Spell = spell;
        IsActive = false;
        SpellName = spell.name;
    }
}