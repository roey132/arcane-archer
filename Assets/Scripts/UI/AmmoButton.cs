using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AmmoButton : MonoBehaviour
{
    public AmmoData Ammo;
    private TextMeshProUGUI _text;
    private Image _image;
    void Awake()
    {

    }
    public void InitButton(AmmoData ammo, int ammoCount)
    {
        _text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _image = transform.GetChild(1).GetComponent<Image>();
        gameObject.SetActive(true);
        Ammo = ammo;
        _image.sprite = Ammo.Sprite;
        _text.text = ammoCount.ToString();

        Button button = GetComponent<Button>();
        UnityAction setAmmo = () => IngameStats.Instance.SetEquippedAmmo(ammo) ;//ProjectileShooter.Instance.SetEquippedAmmo(ammo);
        button.onClick.AddListener(setAmmo);
    }
    public void UpdateAmmoCount(int ammoCount)
    {
        _text.text = ammoCount.ToString();
    }
    public void ResetButton()
    {
        Ammo = null;
        _text.text = null;
        _image.sprite = null;
        gameObject.SetActive(false);
        GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
