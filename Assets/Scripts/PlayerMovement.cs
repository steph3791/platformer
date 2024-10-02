using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Range(5, 20)] private int speed = 10;
    [SerializeField] [Range(1, 10)] private float jumpForce = 7;
    [SerializeField] [Range(1f, 1.5f)] private float fallIncreaseFactor = 1.1f;
    [SerializeField] private Audio jumpSound;
    
    private readonly float _maxJumpFloatDuration = 15f;

    private Rigidbody2D _rigidbody2D;
    private float _movementDirection;
    private float _lastMovementDirection;
    private float _initGravityScale;

    private byte _jumpCount;
    private bool _jumpReleased;
    private float _jumpFloatDuration;

    private bool _canWallJump;
    private float _wallJumpDirection;
    private float _freezeMovement;


    // Start is called before the first frame update
    void Start()
    {

        _rigidbody2D = GetComponent<Rigidbody2D>();
        _initGravityScale = _rigidbody2D.gravityScale;
        _jumpCount = 0;
        _jumpReleased = false;
        _jumpFloatDuration = 0f;
    }


    void FixedUpdate()
    {
        if (!_canWallJump && _freezeMovement < 0)
        {
            _rigidbody2D.velocity = new Vector2(_movementDirection * speed, Mathf.Max(_rigidbody2D.velocity.y, -9f));
        }
        else
        {
            _freezeMovement--;
        }
        if (_rigidbody2D.velocity.y < 0)
        {
            _rigidbody2D.gravityScale *= fallIncreaseFactor;
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
        if (other.gameObject.CompareTag("Platform"))
        {
                ResetJumpStats();
                ResetJumpReleasedStats();
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            InitializeWallJump(other.contacts[0].normal);
        }
    }

    private void InitializeWallJump(Vector3 normal)
    {
        Debug.Log("Colliding with Wall");
        _canWallJump = true;
        _freezeMovement = 20f;
        _wallJumpDirection = normal.normalized.x;
    }


    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
                ResetJumpStats();
                ResetJumpReleasedStats();
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            InitializeWallJump(other.contacts[0].normal);
        }
    }
    
    
    void OnMove(InputValue value)
    {
        _movementDirection = value.Get<Vector2>().x;
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            ResetJumpReleasedStats();
            _jumpCount++;
            if (_canWallJump)
            {
                _canWallJump = false;
                HandleWallJump();
                return;
            }
            if (_jumpCount == 1)
            {
                _rigidbody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                jumpSound.Play();
            }
            else if (_jumpCount == 2)
            {
                if (_rigidbody2D.velocity.y < 0)
                {
                    _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0); //get full impact of second jump without downwards velocity deminishing the jump;
                    _rigidbody2D.gravityScale = _initGravityScale;
                }
                _rigidbody2D.AddForce(new Vector2(0, jumpForce / 1.1f), ForceMode2D.Impulse);
                jumpSound.Play();

            }
            else if (_jumpCount >= 2)
            {
                Debug.LogWarning("Cannot Jump anymore");
            }
        }
        else
        {
            _jumpReleased = true;
            
        }
    }

    void HandleWallJump()
    {
        Debug.Log("Handling Wall Jump with direction: " +_wallJumpDirection);
        ResetAllPhysics();
        _rigidbody2D.position = new Vector2(_rigidbody2D.position.x + (0.1f * _wallJumpDirection), _rigidbody2D.position.y);
        _rigidbody2D.AddForce(new Vector2(5 * _wallJumpDirection, jumpForce), ForceMode2D.Impulse);
    }

    private void ResetAllPhysics()
    {
        _rigidbody2D.totalForce = Vector2.zero;
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.totalForce = Vector2.zero;
        ResetJumpStats();
        ResetJumpReleasedStats();
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
}