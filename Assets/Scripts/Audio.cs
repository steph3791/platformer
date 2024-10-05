using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Audio : MonoBehaviour
{
    [Header("Jump")] [SerializeField] private AudioClip jumpSound;
    [SerializeField] private float volume = 0.5f;
    
    [Header("Damage")] [SerializeField] private AudioClip damageSound;
    [SerializeField] private float damageVolume = 0.5f;

    private AudioSource _jumpSource;
    private AudioSource _damageSource;
    private AudioSource _winSource;

    private void Start()
    {
        if (jumpSound == null)
        {
            Debug.LogError("jumpSound is null");
            enabled = false;
        }
        if (damageSound == null)
        {
            Debug.LogError("damageSound is null");
            enabled = false;
        }
        _jumpSource = gameObject.AddComponent<AudioSource>();
        _jumpSource.clip = jumpSound;
        _jumpSource.volume = volume;
        
        _damageSource = gameObject.AddComponent<AudioSource>();
        _damageSource.clip = damageSound;
        _damageSource.volume = damageVolume;
    }

    public void PlayJumpSound()
    {
        _jumpSource.Play();
    }

    public void PlayDamageSound()
    {
        // _damageSource.Play();
    }
    
}