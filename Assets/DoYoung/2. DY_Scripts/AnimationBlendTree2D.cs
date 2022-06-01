using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBlendTree2D : MonoBehaviour
{
    Animator anim;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;

    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maxiumWalkVelocity = 0.5f;
    public float maxiumRunVelocity = 2.0f;

    int velocityXHash;
    int velocityZHash;

    void Start()
    {
        anim = GetComponent<Animator>();
        //성능보완
        velocityXHash = Animator.StringToHash("VelocityX");
        velocityZHash = Animator.StringToHash("VelocityZ");
    }


    void Update()
    {
        //성능보완
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        //걷기상태, 뛰는상태일 때 최대속력
        float currentMaxVelocity = runPressed ? maxiumRunVelocity : maxiumWalkVelocity;


        //걷기, 뛰기 상태일 때 버튼입력 시 가속하기
        //전진하면서 최대속력보다 작으면
        if (forwardPressed && velocityZ < currentMaxVelocity)
        {
            //가속한다.
            velocityZ += Time.deltaTime * acceleration;
        }
        //왼쪽이동을 하면서 최대속력의 음수보다 크면
        if (leftPressed && velocityX > -currentMaxVelocity)
        {
            // -x 방향으로 가속
            velocityX -= Time.deltaTime * acceleration;
        }
        //오른쪽이동을 하면서 최대속력보다 작으면
        if (rightPressed && velocityX < currentMaxVelocity)
        {
            // +x 방향으로 가속
            velocityX += Time.deltaTime * acceleration;
        }



        //걷기 -> 뛰기 or 뛰기 -> 걷기 상태전환일어날 때 갱신된 최대속력까지 감속하기
        //z방향 속력이 최대속력을 넘으면
        if (velocityZ > currentMaxVelocity)
        {
            //감속한다.
            velocityZ -= Time.deltaTime * deceleration;
            //감속하다가 현재최대속력보다 작아지면 전진속력을 현재최대속력으로 한다.
            if (velocityZ < currentMaxVelocity) velocityZ = currentMaxVelocity;
        }

        if (velocityX > currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * deceleration;
            if (velocityX < currentMaxVelocity) velocityX = currentMaxVelocity;
        }
        //x방향 속력이 현재최대속력을 넘으면
        if (velocityX < -currentMaxVelocity)
        {
            //반대방향으로 감속한다.
            velocityX += Time.deltaTime * deceleration;
            //현재최대속력의 음수보다 커지면 오른쪽이동속력을 현재최대속력으로 한다.
            if (velocityX > -currentMaxVelocity) velocityX = -currentMaxVelocity;
        }

        //전진, 좌우이동 버튼 누르지 않을 때 감속하기
        //전진 버튼을 누르지 않고 전진속력이 0보다 크면
        if ((!forwardPressed) && velocityZ > 0)
        {
            //감속한다.
            velocityZ -= Time.deltaTime * deceleration;
            //감속하다가 0보다 작아지면 전진속력을 0으로 한다.
            if (velocityZ < 0) velocityZ = 0;
        }
        //오른쪽이동버튼을 누르지 않고 x방향 속력이 양수라면
        if ((!rightPressed) && velocityX > 0)
        {
            //반대방향으로 감속한다.
            velocityX -= Time.deltaTime * deceleration;
            //감속하다 부호가 뒤집히면 그냥 0으로 한다.
            if (velocityX < 0) velocityX = 0;
        }

        if ((!leftPressed) && velocityX < 0)
        {
            velocityX += Time.deltaTime * deceleration;
            if (velocityX > 0) velocityX = 0;
        }

        if (!forwardPressed && velocityZ < 0.0f)
        {
            velocityZ = 0.0f;
        }

        anim.SetFloat(velocityXHash, velocityX);
        anim.SetFloat(velocityZHash, velocityZ);
    }
}
