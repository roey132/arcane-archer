using UnityEngine;

[CreateAssetMenu(menuName = "Room Object",fileName ="NewRoomObject")]
public class RoomObject : ScriptableObject, WeightedItem
{
    public string RoomName;
    public GameState RoomState;
    public Sprite PortalSprite;
    public int BaseWeight;

    public void Activate()
    {
        GameManager.Instance.UpdateGameState(RoomState);
    }
    public int Weight()
    {
        return BaseWeight;
    }
}
