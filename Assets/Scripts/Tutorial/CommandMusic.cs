using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMusic : TutorialCommand
{
	public string musicName;
	private AudioSource audioSource;

	private void Start()
	{
		audioSource = FindObjectOfType<AudioSource>();
	}

	public override void Excute()
	{
		AudioClip music = Resources.Load<AudioClip>("Music/" + musicName);
		audioSource.PlayOneShot(music);
	}
}
