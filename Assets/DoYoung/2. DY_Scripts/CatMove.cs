using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatMove : MonoBehaviour
{
    Animator anim;
    NavMeshAgent nav;
    public Transform[] Targets;

    int index;
    int layHash;
    int LookBackHash;

    float LayFloat;
    float LookBackFloat;

    bool isChase;
    bool isWaiting;
    bool isLookBackEnd;

    void Start()
    {

        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        // nav.updateRotation = false;
        index = 0;
        layHash = Animator.StringToHash("LayFloat");
        LookBackHash = Animator.StringToHash("LookBack");


    }


    void Update()
    {

        if (isChase)
        {
            anim.SetBool("Waiting", false);
            Walk();
        }

        if (isWaiting)
        {

            LayFloat -= Time.deltaTime;
            if (LayFloat < 0.11f) LayFloat = 0.11f;
            anim.SetFloat(layHash, LayFloat);

            anim.SetBool("Waiting", true);

            if (isLookBackEnd) LookBack_Waiting();
        }
        else
        {
            LookBackFloat -= Time.deltaTime;
            if (LookBackFloat < 0) LookBackFloat = 0;
            anim.SetFloat(LookBackHash, LookBackFloat);
        }


        if (Input.GetKeyDown("space"))
        {

            LayFloat = 0.011f;
            anim.SetFloat(layHash, LayFloat);

            isWaiting = isLookBackEnd = false;
            anim.SetTrigger("Stretching");

        }

        if (Input.GetKeyDown("left shift"))
        {
            nav.isStopped = isWaiting = true;
            isChase = false;
        }
    }

    void Walk()
    {
        float distance = Vector3.Distance(transform.position, Targets[index].position);

        if (distance < nav.stoppingDistance + 0.1f)
        {
            if (LayFloat > 0)
            {
                LayFloat -= Time.deltaTime;
            }
            else
            {
                LayFloat = 0;
                isChase = false;
                index++;
            }
            anim.SetFloat(layHash, LayFloat);

        }
        else
        {
            LayFloat += Time.deltaTime * 0.5f;
            if (LayFloat > 1)
            {
                LayFloat = 1;
            }
            anim.SetFloat(layHash, LayFloat);
        }
    }

    void NavOn()
    {
        nav.isStopped = false;
        nav.SetDestination(Targets[index].position);
        isChase = true;
    }

    void LookBack_Waiting()
    {
        LookBackFloat += Time.deltaTime;
        if (LookBackFloat > 1) LookBackFloat = 1;
        anim.SetFloat(LookBackHash, LookBackFloat);
    }

    void LookBack_End()
    {
        isLookBackEnd = true;
    }
}
