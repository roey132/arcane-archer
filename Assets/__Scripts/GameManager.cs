using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    RoomSelection,
    InCombat,
    ShopRoom,
    BuffSelection,
}
public enum RoomType
{
    TimerRoom,
    ValueRoom,
    ShopRoom,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool UiISActive;
    [SerializeField] public Transform ObjectCollector;
    [SerializeField] public GameState _startState;

    public GameState State;
    public static event Action<GameState> OnGameStateChange;
    public static event Action<RoomType> OnRoomTypeChange;

    [SerializeField] public int CurrentFloor;
    [SerializeField] public int CurrentRoomDifficulty;

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

    public void UpdateRoomType(RoomType roomType)
    {
        switch (roomType)
        {
            case RoomType.TimerRoom:
                print("switching to timer room");
                break;
            case RoomType.ValueRoom:
                print("switching to value room");
                break;
            case RoomType.ShopRoom:
                print("switching to shop room");
                break;
        }
        OnRoomTypeChange?.Invoke(roomType);
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
        CurrentFloor = 1;
        UpdateGameState(_startState);
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
    public void PassFloor()
    {
        CurrentFloor++;
        print($"Current floor is {CurrentFloor}");
    }
    public void SetCurrDifficulty(int difficulty)
    {
        CurrentRoomDifficulty = difficulty;
    }
}
