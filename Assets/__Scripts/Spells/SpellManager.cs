using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public static SpellManager Instance;
    [SerializeField] private Transform _spellsInventory;

    private Transform _playerTransform;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        GameManager.OnGameStateChange += OnGameStateChange;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= OnGameStateChange;
    }
    void Start()
    {

        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void AddSpell(GameObject spellObject)
    {
        Instantiate(spellObject, _spellsInventory);
    }

    private void EnableAllSpells()
    {
        int childCount = _spellsInventory.childCount;

        for (int i= 0; i < childCount; i++)
        {
            _spellsInventory.GetChild(i).gameObject.SetActive(true);
        }
    }
    private void DisableAllSpells()
    {
        int childCount = _spellsInventory.childCount;

        for (int i = 0; i < childCount; i++)
        {
            _spellsInventory.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void OnGameStateChange(GameState newState)
    {
        if (newState != GameState.InCombat)
        {
            DisableAllSpells();
            return;
        } 
        EnableAllSpells();
    }
}
