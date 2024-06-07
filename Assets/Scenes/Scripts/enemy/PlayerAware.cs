using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAware : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; }
    public Vector2 DirectionToPlayer { get; private set; }

    [SerializeField]
    private float _playerAwarenessDistance;

    private Transform _player;

    private void Awake()
    {
        // Find the player object
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            _player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Player object not found.");
        }
    }

    private void Update()
    {
        // Check if the player reference is valid before accessing its position
        if (_player != null)
        {
            Vector2 enemyToPlayerVector = _player.position - transform.position;
            DirectionToPlayer = enemyToPlayerVector.normalized;

            if (enemyToPlayerVector.magnitude <= _playerAwarenessDistance)
            {
                AwareOfPlayer = true;
            }
            else
            {
                AwareOfPlayer = false;
            }
        }
        else
        {
            // Player reference is null, handle this case if needed
            AwareOfPlayer = false;
        }
    }
}
