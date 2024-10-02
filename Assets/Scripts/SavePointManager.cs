using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointManager : MonoBehaviour
{
    
    private Vector3 lastCheckpoint;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            Debug.Log("Colliding with Respawn");
            lastCheckpoint = gameObject.transform.position;
        }

        // if (other.gameObject.CompareTag("Finish"))
        // {
        //     Debug.Log("Finished Level");
        //     //TODO handle finish logic
        // } 
        // if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Void"))
        // {
        //     Debug.Log("SavePOINT collided with obstacle or void");
        //     gameObject.transform.position = lastCheckpoint;
        //     if (!playerHealth.RemoveHeart())
        //     {
        //         //TODO add GameOver;
        //     }
        // }
    }

    public void Respawn()
    {
        Debug.Log("Respawning");
        gameObject.transform.position = lastCheckpoint;
    }
    
}
