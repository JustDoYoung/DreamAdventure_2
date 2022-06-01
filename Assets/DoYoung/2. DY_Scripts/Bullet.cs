using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20.0f;

    void Update()
    {
        Vector3 dir = transform.forward;
        transform.position += dir * Time.deltaTime * speed;
    }
}
