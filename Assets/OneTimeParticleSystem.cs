using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeParticleSystem : MonoBehaviour
{
    [SerializeField] private new ParticleSystem particleSystem;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Play();
    }

    private void Update()
    {
        if(particleSystem.isStopped)
            Destroy(gameObject);
    }
}
