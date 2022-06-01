using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator anim;
    float velocity = 0.0f;
    public float acceleration = 0.1f;
    public float deceleration = 0.5f;
    int velocityHash;
    void Start()
    {
        anim = GetComponent<Animator>();
        velocityHash = Animator.StringToHash("Velocity");
    }


    void Update()
    {
        //성능 이점
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");

        if (forwardPressed && velocity < 1.0f)
        {
            velocity += Time.deltaTime * acceleration;
        }
        else if (!forwardPressed && velocity > 0.0f)
        {
            velocity -= Time.deltaTime * deceleration;
        }
        else if (!forwardPressed && velocity < 0.0f)
        {
            velocity = 0.0f;
        }


        anim.SetFloat(velocityHash, velocity);
    }
}
