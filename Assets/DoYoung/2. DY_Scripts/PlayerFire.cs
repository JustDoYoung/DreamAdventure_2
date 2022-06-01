using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public Transform firePosition;
    public GameObject bulletFactory;
    Transform player;

    void Start()
    {
        player = GetComponent<Transform>();
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = Instantiate(bulletFactory);
            bullet.transform.position = firePosition.position;
            bullet.transform.forward = player.forward;

            Destroy(bullet, 2);
        }
    }
}
