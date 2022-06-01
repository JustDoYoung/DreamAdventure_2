using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickGrenade : MonoBehaviour
{
    public float speed = 10;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        rb.AddTorque(transform.up * 20, ForceMode.Impulse);
    }

    public float radius = 3;
    public GameObject explosionFactory;
    private void OnCollisionEnter(Collision collision)
    {

        Collider[] cols = Physics.OverlapSphere(transform.position, radius);



        GameObject explosion = Instantiate(explosionFactory);
        explosion.transform.position = transform.position;
        Destroy(explosionFactory, 2f);

    }

}
