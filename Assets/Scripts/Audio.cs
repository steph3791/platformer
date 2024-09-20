using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Audio : MonoBehaviour
{
    [Header("Jump")] [SerializeField] private AudioClip jumpSound;
    [SerializeField] private float volume = 0.5f;

    private AudioSource _jumpSource;
    private GameObject _player;

    private void Start()
    {
        if (jumpSound == null)
        {
            Debug.LogError("jumpSound is null");
            enabled = false;
        }
        {
            
        }
        _jumpSource = gameObject.AddComponent<AudioSource>();
        _jumpSource.clip = jumpSound;
        _jumpSource.volume = volume;
        _jumpSource.Play();
        _jumpSource.Pause();
        
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            _jumpSource.Play();
        }

    }
    
    
}