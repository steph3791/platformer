using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] [Range(5,20)] private int speed = 10;
    
    private Vector2 _movementDirection;
    private Rigidbody2D _rigidbody2D;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        _rigidbody2D.velocity = _movementDirection * speed;

    }

    void OnMove(InputValue value)
    {
            _movementDirection = value.Get<Vector2>();
    }

}
