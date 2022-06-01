using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAt : MonoBehaviour
{
    Animator _anim;
    Camera mainCamera;
    void Start()
    {
        _anim = GetComponent<Animator>();
        mainCamera = Camera.main;
    }


    private void OnAnimatorIK(int layerIndex)
    {
        _anim.SetLookAtWeight(1f, 0.2f, 1f, 0.5f, 0.5f);
        Ray lookAtRay = new Ray(transform.position, mainCamera.transform.forward);
        _anim.SetLookAtPosition(lookAtRay.GetPoint(25));
    }
}
