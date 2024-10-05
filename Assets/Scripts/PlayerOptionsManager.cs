using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOptionsManager : MonoBehaviour
{
    public static PlayerOptionsManager Instance;

    private const int DefaultMovementSpeed = 10;
    
    private int _movementSpeed = 10;
    public int MovementSpeed
    {
        get => _movementSpeed;
        set => _movementSpeed = value;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Reset()
    {
        _movementSpeed = DefaultMovementSpeed;
    }
}
