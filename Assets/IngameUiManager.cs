using TMPro;
using UnityEngine;

public class IngameUiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentHealth;
    void Start()
    {
        
    }

    void Update()
    {
        _currentHealth.text = IngameStats.Instance.CurrHp.ToString();
    }
}
