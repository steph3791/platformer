using System;
using UnityEngine;

public static class PersistentDataManager
{
    private const string Speed = "speed";
    public static readonly int DefaultSpeed = 10;


    public static event EventHandler DataChangedEvent;


    private static void OnDataChanged()
    {
        DataChangedEvent?.Invoke(null, EventArgs.Empty);
    }

    public static int MovementSpeed
    {
        get => PlayerPrefs.GetInt(Speed, DefaultSpeed);
        set
        {
            PlayerPrefs.SetInt(Speed, value);
            Debug.Log("Changed Player Speed to: " + value);
            OnDataChanged();
        }
    }

    public static void Reset()
    {
        Debug.Log("Resetting Movement Speed");
        MovementSpeed = DefaultSpeed;
    }
}