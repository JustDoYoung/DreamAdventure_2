using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public GameObject targetLock;
    public Transform model;

    private Animator anim;
    private Camera mainCamera;
    private Vector3 stickDirection;

    [Range(20f, 80f)]
    public float rotationSpeed = 30f;

    private bool isWeaponEquipped = false;
    private bool isTargetLocked = false;

    void Start()
    {
        mainCamera = Camera.main;
        anim = model.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        stickDirection = new Vector3(h, 0, v);

        HandlerInputData();
        if (isTargetLocked) HandlerTargetLockedLocomotionRotation();
        else HandlerStandardLocomotionRotation();
    }


    private void HandlerStandardLocomotionRotation()
    {
        Vector3 rotationOffset = /*vodoo magic*/ mainCamera.transform.TransformDirection(stickDirection);
        rotationOffset.y = 0;
        model.forward += Vector3.Lerp(model.forward, rotationOffset, Time.deltaTime * rotationSpeed);
    }
    private void HandlerTargetLockedLocomotionRotation()
    {
        Vector3 rotationOffset = /*vodoo magic*/ targetLock.transform.position - model.position;
        rotationOffset.y = 0;
        model.forward += Vector3.Lerp(model.forward, rotationOffset, Time.deltaTime * rotationSpeed);
    }
    void HandlerInputData()
    {
        anim.SetFloat("Speed", Vector3.ClampMagnitude(stickDirection, 1).magnitude);
        anim.SetFloat("Horizontal", stickDirection.x);
        anim.SetFloat("Vertical", stickDirection.z);

        isWeaponEquipped = anim.GetBool("isWeaponEquipped");
        isTargetLocked = anim.GetBool("isTargetLocked");

        if (isWeaponEquipped && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("isTargetLocked", !isTargetLocked);
            isTargetLocked = !isTargetLocked;
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetBool("isWeaponEquipped", !isWeaponEquipped);
            isWeaponEquipped = !isWeaponEquipped;
            if (false == isWeaponEquipped)
            {
                anim.SetBool("isTargetLocked", false);
                isTargetLocked = false;
            }
        }
    }
}
