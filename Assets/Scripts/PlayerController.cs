using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;

    private Audio _audio;

    private Vector3 _lastCheckpoint = Vector3.zero;

    void Start()
    {
        _audio = GetComponent<Audio>();
        if (_audio == null)
        {
            Debug.LogError("Unable to find Audio Component on player instance");
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
            _audio.PlayWinSound();
            GameFlowManager.Instance.ManageWinLevel(transform.position);
        }
        
        if (other.gameObject.CompareTag("Respawn"))
        {
            Debug.Log("Colliding with Respawn. Distance: " +Mathf.Abs(transform.position.x - _lastCheckpoint.x));
            if (Mathf.Abs(transform.position.x - _lastCheckpoint.x) > 2)
            {
                _audio.PlayWinSound();
            }
            _lastCheckpoint = gameObject.transform.position;
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
            Respawn();
            _audio.PlayWinSound();
        }
        else
        {
            Debug.Log("GAME OVER");
            GameFlowManager.Instance.ManageGameOver(transform.position);
        }
    }
    
    public void Respawn()
    {
        Debug.Log("Respawning");
        gameObject.transform.position = _lastCheckpoint;
    }
    

}
