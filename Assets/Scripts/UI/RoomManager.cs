using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private List<RoomObject> _roomObject;
    
    void Awake()
    {
        GameManager.OnGameStateChange += InitRooms;
    }
    void OnDestroy()
    {
        GameManager.OnGameStateChange -= InitRooms;
    }
    private void Start()
    {
        GameManager.Instance.UpdateGameState(GameState.RoomSelection);
    }
    public void InitRooms(GameState state)
    {
        if (state != GameState.RoomSelection)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
        List<RoomObject> selectedRooms = _roomObject.RandomItems(3);

        for (int i = 0; i < 3; i++)
        {
            RoomSelector room = transform.Find($"RoomSelector{i+1}").GetComponent<RoomSelector>();
            room.InitPortal(selectedRooms[i]);
        }
    }
}
