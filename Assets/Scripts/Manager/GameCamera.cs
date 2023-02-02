using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    PlayerController player;

    public float distance;

    public float height;

    // Update is called once per frame
    void Update()
    {
        if (player != null)
            transform.position = new Vector3(player.transform.position.x, height, player.transform.position.z - distance);
    }

    public void SetCameraFollowTarget(PlayerController player)
    {
        this.player = player;
    }
}