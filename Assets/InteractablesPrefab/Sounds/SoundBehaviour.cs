using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBehaviour : MonoBehaviour
{
    ParticleSystem particles;
    public float duration;
    bool shouldbesestroyed = true;

    public float lifetimeAddition;
    // Start is called before the first frame update
    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();

        if (duration == 0)
        {
            shouldbesestroyed = false;
        }

    }

    private void Update()
    {
        if (particles.isEmitting)
        {
            var tmp = particles.main;
            if(particles.startLifetime < 10)
            {
                tmp.startLifetimeMultiplier += lifetimeAddition * Time.deltaTime;
            }

        }


        if(duration > 0 && shouldbesestroyed)
        {
            duration -= Time.deltaTime;
            if(duration < 0)
            {
                particles.Stop();
            }
        }
    }
}
