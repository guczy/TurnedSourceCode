using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasGlock : MonoBehaviour
{
    GameObject player;
    bool followPlayer = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player Glock");
        if (player == null)
        {
            Debug.LogError("Player GameObject not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer)
        {
            camFollowPlayer();
        }
    }

    public void setFollowPlayer(bool val)
    {
        followPlayer = val;
    }

    public void UpdatePlayerReference(GameObject newPlayer)
    {
        player = newPlayer;
        if (player == null)
        {
            Debug.LogError("New player GameObject not found!");
        }
    }

    void camFollowPlayer()
    {
        if (player != null)
        {
            Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);
            this.transform.position = newPos;
        }
        else
        {
            Debug.LogWarning("Player is null. Camera cannot follow.");
        }
    }
}
