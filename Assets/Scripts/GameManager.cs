using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [SerializeField] private GameObject buffsUi;
    public void showItemPickItemUI()
    {
        buffsUi.SetActive(true);
    }
    public void hideItemPickItemUI() 
    {
        buffsUi.SetActive(false);
    }
    public void endScene()
    {
        SceneManager.LoadScene(0);
    }
}
