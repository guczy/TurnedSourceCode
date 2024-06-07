using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationSpeed;

    private Rigidbody2D _rigidbody;
    private PlayerAware _playerAwarnessController;
    private Vector2 _targetDirection;
    private AudioSource _walkingSound;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarnessController = GetComponent<PlayerAware>();
        _walkingSound = GetComponent<AudioSource>();

        // Check if AudioSource is attached
        if (_walkingSound == null)
        {
            Debug.LogError("No AudioSource component found on the enemy object. Please add an AudioSource component.");
        }
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        if (_playerAwarnessController.AwareOfPlayer)
        {
            _targetDirection = _playerAwarnessController.DirectionToPlayer;
        }
        else
        {
            _targetDirection = Vector2.zero;
        }
    }

    private void RotateTowardsTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        _rigidbody.SetRotation(rotation);
    }

    private void SetVelocity()
    {
        if (_targetDirection == Vector2.zero)
        {
            _rigidbody.velocity = Vector2.zero;

            // Stop walking sound when not moving
            if (_walkingSound.isPlaying)
            {
                _walkingSound.Stop();
            }
        }
        else
        {
            _rigidbody.velocity = transform.up * _speed;

            // Start walking sound when moving
            if (_walkingSound != null && !_walkingSound.isPlaying)
            {
                _walkingSound.Play();
            }
        }
    }
}
