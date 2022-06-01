using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPlay_Fire : MonoBehaviour
{
    public GameObject bulletFactory;
    public Transform firePosition;
    Transform player;

    private void Start()
    {
        player = GetComponent<Transform>();
    }

    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletFactory, firePosition.position, player.rotation);
    }
}
