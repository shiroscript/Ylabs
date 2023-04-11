using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballTrigger : MonoBehaviour
{
    public ParticleSystem particle1;
    public ParticleSystem particle2;
    public ParticleSystem particle3;
    private void Awake()
    {
        if (particle1)
        {
            particle1.Stop();
        }
        if (particle2)
        {
            particle2.Stop();
        }
        if (particle3)
        {
            particle3.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Football"))
        {
            if (particle1)
            {
                particle1.Play();
            }
            if (particle2)
            {
                particle2.Play();
            }
            if (particle3)
            {
                particle3.Play();
            }
        }
    }
}
