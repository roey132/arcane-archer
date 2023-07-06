using UnityEngine;

[CreateAssetMenu(menuName = "Room Object",fileName ="NewRoomObject")]
public class RoomObject : ScriptableObject, WeightedItem
{
    public string RoomName;
    public GameState RoomState;
    public RoomType RoomType;
    public Sprite PortalSprite;
    public int BaseWeight;

    public void Activate(int difficulty)
    {
        GameManager.Instance.SetCurrDifficulty(difficulty);
        GameManager.Instance.UpdateGameState(RoomState);
        GameManager.Instance.UpdateRoomType(RoomType);
        GameManager.Instance.PassFloor();
    }
    public int Weight()
    {
        return BaseWeight;
    }
}
