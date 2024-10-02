using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    public static OptionsManager Instance;

    private int _movementSpeed = 10;
    public int MovementSpeed
    {
        get => _movementSpeed;
        set => _movementSpeed = value;
    }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("OptionsManager Awake");
    }
}
