using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameObject gameOverPanel;

    private SavePointManager _saveManager;
    private Audio _audio;

    void Start()
    {
        _saveManager = GetComponent<SavePointManager>();
        _audio = GetComponent<Audio>();
        if (_saveManager == null)
        {
            Debug.LogError("Unable to find SavePointManager Component on player instance");
            GameFlowManager.Instance.QuitApplication();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Void"))
        {
            Damage();
        }

        if (other.CompareTag("Finish"))
        {
            GameFlowManager.Instance.ManageWinLevel(transform.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Damage();
        }
    }

    private void Damage()
    {
        _audio.PlayDamageSound();   
        if (playerHealth.RemoveHeart())
        {
            _saveManager.Respawn();
        }
        else
        {
            Debug.Log("GAME OVER");
            GameFlowManager.Instance.ManageGameOver(transform.position);
        }
    }
}
