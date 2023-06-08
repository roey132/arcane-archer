using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleManager : MonoBehaviour
{
    [SerializeField] private GameObject _teleportParticles;
    private Transform _player;
    private Transform _bow;
    private void Awake()
    {
        _player = transform.parent;
        _bow = transform.parent.Find("Bow");
        PlayerMovement.OnTeleportStart += TeleportParticles;
        BowAnimations.ShootAction += RestartBowParticles;
        PlayerMovement.OnTeleportEnd += TeleportParticles;
    }
    private void OnDisable()
    {
        PlayerMovement.OnTeleportStart -= TeleportParticles;
        PlayerMovement.OnTeleportEnd -= TeleportParticles;
    }
    public IEnumerator InstantiateParticles(GameObject particles)
    {
        GameObject newParticles = Instantiate(original:particles,position:_player.position,Quaternion.identity);
        print(particles.GetComponent<ParticleSystem>().main.startLifetime);
        yield return new WaitForSeconds(particles.GetComponent<ParticleSystem>().main.duration);
        Destroy(newParticles);
    }
    public void TeleportParticles() 
    {
        StartCoroutine(InstantiateParticles(_teleportParticles));
    }
    public void RestartBowParticles()
    {
        ParticleSystem particles = _bow.Find("ShootParticles").GetComponent<ParticleSystem>();
        particles.Clear();
        particles.Play();
    }
    
}
