using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool UiISActive;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        UiISActive = true;
    }

    [SerializeField] private GameObject buffsUi;
    public void showItemPickItemUI()
    {
        UiISActive = true;
        buffsUi.SetActive(true);
    }
    public void hideItemPickItemUI() 
    {
        UiISActive = false;
        buffsUi.SetActive(false);
    }
    public void endScene()
    {
        SceneManager.LoadScene(0);
    }
}
