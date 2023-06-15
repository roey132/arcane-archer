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
    [SerializeField] public Transform ObjectCollector;


    public GameState State;
    public static event Action<GameState> OnGameStateChange;

    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.RoomSelection:
                print("RoomSelection state");
                break;
            case GameState.InCombat:
                print("InCombat state");
                break;
            case GameState.ShopRoom:
                print("ShopRoom state");
                break;
            case GameState.BuffSelection:
                ShowItemPickItemUI();
                print("BuffSelection state");
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
        Application.targetFrameRate = 144;
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
