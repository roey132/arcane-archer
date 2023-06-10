using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _teleportSound;
    private void Awake()
    {
        PlayerMovement.OnTeleportStart += _teleportSound.Play;
    }
    private void OnDestroy()
    {
        PlayerMovement.OnTeleportStart -= _teleportSound.Play;
    }
    void Start()
    {
        _teleportSound.time = 0.9f;
        _teleportSound.volume = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
