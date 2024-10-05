using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionParticleEffect : MonoBehaviour
{
    
    private ParticleSystem _ps;
    private AudioSource _audio;
    private bool _collided = false;
    private void OnEnable()
    {
        _ps = GetComponent<ParticleSystem>();
        if (_ps == null)
        {
            Debug.LogError("Particle System for Collision Effect is null");
            enabled = false;
        }
        
        _audio = GetComponent<AudioSource>();
        _ps.Stop();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_collided)
        {
            _collided = true;
            _ps.Play();
            if (_audio != null)
            {
                _audio.Play();
            }
            StartCoroutine(DestroyEffectWhenFinished()); //The particles should only play on first collision and then never again.
        }
    }
    
    private IEnumerator DestroyEffectWhenFinished()
    {
        while (!_ps.isStopped || _audio.isPlaying)
        {
            yield return null; // Wait for the next frame
        }
        Destroy(gameObject);
    }
}
