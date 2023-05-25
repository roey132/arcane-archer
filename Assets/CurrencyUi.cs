using TMPro;
using UnityEngine;
public class CurrencyUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyCount;

    // Update is called once per frame
    void Update()
    {
        currencyCount.text = Mathf.Floor(IngameStats.Instance.IngameCurrency).ToString();
    }
}
