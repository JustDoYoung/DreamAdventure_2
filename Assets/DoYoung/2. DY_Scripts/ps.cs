using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ps : MonoBehaviour
{
    ParticleSystem pts;
    // Start is called before the first frame update
    void Start()
    {
        pts = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pts.isPlaying)
        {
            play();
        }
    }

    void play()
    {
        pts.Play();
    }
}
