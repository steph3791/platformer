using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject[] hearts;
    private int lostLives;

    private void OnEnable()
    {
        lostLives = 0;
        if (hearts.Length < 3)
        {
            Debug.LogError("PlayerHealth needs at least 3 hearts");
            this.enabled = false;
        }
    }

    public bool RemoveHeart()
    {
        lostLives++;
        if (lostLives < hearts.Length)
        {
            hearts[^lostLives].gameObject.SetActive(false);
            return true;
        }
        return false;
    }
}