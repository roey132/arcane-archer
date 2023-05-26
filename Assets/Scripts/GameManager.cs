using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    RoomSelection,
    InCombat,
    ShopRoom,
    BuffSelection
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool UiISActive;



    public GameState State;
    public static event Action<GameState> OnGameStateChange;

    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.RoomSelection:
                break;
            case GameState.InCombat:
                break;
            case GameState.ShopRoom:
                break;
            case GameState.BuffSelection:
                ShowItemPickItemUI();
                break;
        }

        OnGameStateChange?.Invoke(newState);
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        UpdateGameState(GameState.BuffSelection);
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
        UpdateGameState(GameState.ShopRoom);
    }
    public void EndScene()
    {
        SceneManager.LoadScene(1);
    }
}
