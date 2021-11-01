using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] private Transform player;
    [SerializeField] private float horizontalOffset;

    void Update()
    {
        if (!PlayerData.GAME_OVER)
        {
            var newPos = transform.position;
            newPos.x = player.position.x + horizontalOffset;
            transform.position = newPos;
        }
    }
}
