using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController cc;
    public float speed = 5;
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    public float jumpPower = 7f;
    public float gravity = -15f;
    float yVelocity;
    // 다중 점프를 뛰고싶다.
    int jumpCount;
    public int maxJumpCount = 1;
    void Update()
    {
        if (cc.isGrounded)
        {
            jumpCount = 0;
        }
        else
        {
            yVelocity += gravity * Time.deltaTime;
        }
      
        if (jumpCount < maxJumpCount && Input.GetButtonDown("Jump"))
        {
            yVelocity = jumpPower;
            jumpCount++;
        }



        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);


        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        dir.Normalize();
        Vector3 velocity = dir * speed;
        velocity.y = yVelocity;

        cc.Move(velocity * Time.deltaTime);
    }
}

