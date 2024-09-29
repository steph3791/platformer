using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 _initPos;
    // Start is called before the first frame update

    private void Awake()
    {
        if (player == null)
        {
            Debug.LogError("Camera has no Player Reference set");
            EditorApplication.isPlaying = false;
        }

    }

    void Start()
    {
        _initPos = transform.position;
        
    }

    void LateUpdate()
    {
        if (player.transform.position.x > _initPos.x)
        {
            transform.position = new Vector3(player.transform.position.x, Mathf.Max(player.transform.position.y, _initPos.y), transform.position.z);
        }
    }
}
