using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public PlayerController player;

    public float distance;

    public float height;
    // Update is called once per frame
    void Update()
    {
        transform.position = new  Vector3( player.transform.position.x,height,player.transform.position.z);
    }
}
