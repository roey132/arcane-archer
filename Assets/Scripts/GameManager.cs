using System;
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
    }
    private void Start()
    {
        ShowItemPickItemUI();
    }

    [SerializeField] private GameObject buffsUi;
    public void ShowItemPickItemUI()
    {
        UiISActive = true;
        buffsUi.SetActive(true);
        buffsUi.GetComponent<BuffSelectionManager>().SetAllButtons();
    }
    public void HideItemPickItemUI() 
    {
        UiISActive = false;
        buffsUi.SetActive(false);
    }
    public void EndScene()
    {
        SceneManager.LoadScene(1);
    }
}
