using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Range(5, 20)] private int speed = 10;
    [SerializeField] [Range(1, 10)] private float jumpForce = 7;
    [SerializeField] [Range(1f, 1.5f)] private float fallIncreaseFactor = 1.1f;

    private readonly float _maxJumpFloatDuration = 15f;
    
    private float _movementDirection;
    private Rigidbody2D _rigidbody2D;
    private byte _jumpCount = 0;
    private float _initGravityScale;
    private bool _jumpReleased = false;
    private float _jumpFloatDuration = 0f;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _initGravityScale = _rigidbody2D.gravityScale;
    }


    void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(_movementDirection * speed, Mathf.Max(_rigidbody2D.velocity.y, -9f));
        if (_rigidbody2D.velocity.y < 0)
        {
            _rigidbody2D.gravityScale *=fallIncreaseFactor;
        }

        if (_jumpReleased)
        {
            if (_jumpFloatDuration < _maxJumpFloatDuration)
            {
                float t = _jumpFloatDuration / _maxJumpFloatDuration;
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Mathf.Lerp(_rigidbody2D.velocity.y, 0, t));
                _jumpFloatDuration++;
            }
            else
            {
               ResetJumpReleasedStats();
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Colliding with: " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Platform"))
        {
            if (Mathf.Abs(Vector2.Angle(other.contacts[0].normal, Vector2.left) - 90f) < 1)
            {
                ResetJumpStats();
                ResetJumpReleasedStats();
            }
            else
            {
                //TODO handle wall jump
            }

        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            Debug.Log(Vector2.Angle(other.contacts[0].normal, Vector2.left));
            if (Mathf.Abs(Vector2.Angle(other.contacts[0].normal, Vector2.left) - 90f) < 1 )
            {
                ResetJumpStats();
                ResetJumpReleasedStats();
            }
            else
            {
                //TODO handle wall slide
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colliding with: " + other.gameObject.tag);
        if (other.gameObject.CompareTag("EdgeTrigger"))
        {
            ResetJumpStats();
            ResetJumpReleasedStats();
        } else if (other.gameObject.CompareTag("Obstacle"))
        {
            //TODO handle Death
            EditorApplication.isPlaying = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EdgeTrigger"))
        {
            ResetJumpStats();
            ResetJumpReleasedStats();
        }
    }

    private void ResetJumpStats()
    {
        _jumpCount = 0;
        _rigidbody2D.gravityScale = _initGravityScale;
    }

    private void ResetJumpReleasedStats()
    {
        _jumpReleased = false;
        _jumpFloatDuration = 0;
    }
    

    void OnMove(InputValue value)
    {
        _movementDirection = value.Get<Vector2>().x;
    }

    void OnJump(InputValue value)
    {
        Debug.Log("OnJump");
        if (value.isPressed)
        {
            ResetJumpReleasedStats();
            _jumpCount++;
            if (_jumpCount<=2)
            {
                _rigidbody2D.gravityScale = _initGravityScale;
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
        else
        {
            Debug.Log("OnJumpRelease");
            _jumpReleased = true;
        }
        

    }
}