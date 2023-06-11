using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private PlayerInputs _playerInputs = null;

    public Vector2 Movement = Vector2.zero;
    public bool Interact = false;

    // Use this for initialization
    void Awake()
    {
        _playerInputs = new PlayerInputs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
