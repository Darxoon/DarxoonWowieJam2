using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAudioSource : MonoBehaviour
{
    public string clipName;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        Debug.LogWarning(clipName);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = GameManager.Instance.GetClip(clipName);
        audioSource.Play();
    }
}
