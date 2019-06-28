using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public bool bouncedOn;
    public bool keepX;

    private void Awake()
    {
        if (keepX) { return; }
        transform.position = new Vector3(Random.Range(-8f, 8f), transform.position.y);
    }
}
