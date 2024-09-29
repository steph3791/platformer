using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointManager : MonoBehaviour
{
    public Vector3 lastCheckpoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Respawn"))
        {
            lastCheckpoint = gameObject.transform.position;
        }

        if (other.CompareTag("Finish"))
        {
            Debug.Log("Finished Level");
            //TODO handle finish logic
        } else if (other.CompareTag("Obstacle"))
        {
            //TODO handle loose live logic
            gameObject.transform.position = lastCheckpoint;
        }
    }
    
    
}
