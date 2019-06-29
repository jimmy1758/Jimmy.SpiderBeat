using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandStopMusic : TutorialCommand
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
    }

    public override void Excute()
    {
        audioSource.Stop();
    }
}
