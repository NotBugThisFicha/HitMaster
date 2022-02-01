using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController Player;
    private Vector3 offset;

    private void Start()
    {
        Player = FindObjectOfType<PlayerController>();
        offset = transform.position - Player.transform.position;
    }

    private void LateUpdate()
    {
        if (Player != null)
        {
            Vector3 newPosition = new Vector3(Player.transform.position.x, offset.y + Player.transform.position.y, offset.z + Player.transform.position.z);
            transform.position = newPosition;
            
        }
    }
}
