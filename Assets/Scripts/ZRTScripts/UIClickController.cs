using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClickController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void PlayClip()
    {
        audioSource.Play();
    }
}


