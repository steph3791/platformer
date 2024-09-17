using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Range(5, 20)] private int speed = 10;
    [SerializeField] [Range(1, 10)] private float jumpForce = 7;
    [SerializeField] [Range(1f, 1.5f)] private float fallIncreaseFactor = 1.1f;

    private float _movementDirection;
    private Rigidbody2D _rigidbody2D;
    private bool _canJump;
    private byte _jumpCount = 0;
    private float initGravityScale;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        initGravityScale = _rigidbody2D.gravityScale;
    }


    void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(_movementDirection * speed, Mathf.Max(_rigidbody2D.velocity.y, -9f));
        if (_rigidbody2D.velocity.y < 0)
        {
            _rigidbody2D.gravityScale *=fallIncreaseFactor;
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Colliding with: " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Platform"))
        {
            if (Mathf.Approximately(Vector2.Angle(other.contacts[0].normal, Vector2.left), 90f))
            {
                ResetJumpStats();
            }
            else
            {
                Debug.Log("Colliding with wall");
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colliding with: " + other.gameObject.tag);
        if (other.gameObject.CompareTag("EdgeTrigger"))
        {
            ResetJumpStats();
        } else if (other.gameObject.CompareTag("Obstacle"))
        {
            //TODO handle Death
            EditorApplication.isPlaying = false;
        }
    }

    private void ResetJumpStats()
    {
        _jumpCount = 0;
        _canJump = true;
        _rigidbody2D.gravityScale = initGravityScale;
    }
    

    void OnMove(InputValue value)
    {
        _movementDirection = value.Get<Vector2>().x;
    }

    void OnJump(InputValue value)
    {
        Debug.Log("OnJump");
        _jumpCount++;
        if (_jumpCount<=2)
        {
            _rigidbody2D.gravityScale = initGravityScale;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
            if (_jumpCount == 1)
            {
                _rigidbody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
            else if (_jumpCount == 2)
            {
                _rigidbody2D.AddForce(new Vector2(0, jumpForce / 1.25f), ForceMode2D.Impulse);
            }
        }
        else
        {
            Debug.LogWarning("Cannot Jump anymore");
        }

    }
}