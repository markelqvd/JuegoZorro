using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ColumnDustTrail : MonoBehaviour
{
    public ParticleSystem dustParticles;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float speed = rb.velocity.magnitude;

        if (speed > 0.1f)
        {
            if (!dustParticles.isPlaying)
                dustParticles.Play();
        }
        else
        {
            if (dustParticles.isPlaying)
                dustParticles.Stop();
        }
    }
}
