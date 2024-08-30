using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float followSpeed = 1;

    void LateUpdate()
    {
        if (!player) { return; }
        if (transform.position.y < player.transform.position.y - 2f)
        {
            transform.position = Vector3.Slerp(transform.position, new Vector3(0, player.transform.position.y, -10), Time.deltaTime * followSpeed);
        }
    }
}
