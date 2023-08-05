using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ArrowButton : MonoBehaviour
{
    public ArrowData Arrow;
    private TextMeshProUGUI _text;
    private Image _image;
    void Awake()
    {

    }
    public void InitButton(ArrowData arrow, int ammoCount)
    {
        _text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _image = transform.GetChild(1).GetComponent<Image>();
        gameObject.SetActive(true);
        Arrow = arrow;
        _image.sprite = Arrow.Sprite;
        _text.text = ammoCount.ToString();

        Button button = GetComponent<Button>();
        UnityAction setAmmo = () => IngameStats.Instance.SetEquippedArrow(arrow) ;//ProjectileShooter.Instance.SetEquippedAmmo(arrow);
        button.onClick.AddListener(setAmmo);
    }
    public void UpdateAmmoCount(int ammoCount)
    {
        _text.text = ammoCount.ToString();
    }
    public void ResetButton()
    {
        Arrow = null;
        _text.text = null;
        _image.sprite = null;
        gameObject.SetActive(false);
        GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
