using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinParticles : MonoBehaviour
{
    
    private ParticleSystem _ps;
    private void OnEnable()
    {
        _ps = GetComponent<ParticleSystem>();
        _ps.Stop();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _ps.Play();
            StartCoroutine(DestroyEffectWhenFinished()); //The particles should only play on first collision and then never again.
        }
    }
    
    private IEnumerator DestroyEffectWhenFinished()
    {
        // Get the ParticleSystem component from the instance

        // Wait until the particle system has stopped
        while (_ps != null && !_ps.isStopped)
        {
            yield return null; // Wait for the next frame
        }

        // Destroy the game object after the particle system has stopped
        Destroy(gameObject);
    }
}
