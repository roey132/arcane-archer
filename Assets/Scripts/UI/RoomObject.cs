using UnityEngine;

[CreateAssetMenu(menuName = "Room Object",fileName ="NewRoomObject")]
public class RoomObject : ScriptableObject
{
    public string RoomName;
    public GameState RoomState;
    public Sprite PortalSprite;

    public void Activate()
    {
        GameManager.Instance.UpdateGameState(RoomState);
    }
}
