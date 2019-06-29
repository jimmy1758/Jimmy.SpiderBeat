using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour {

	public GameObject pauseImage;
	public GameObject dialogueFrame;

	public void OnPauseButton()
	{
		pauseImage.SetActive(true);
		CommandManager.instance.SwitchStats(CommandStates.diabled);
        AudioManager.instance.PlaySFX(SFXAudio.SFX_BtnClick);
        dialogueFrame.transform.position += new Vector3(0, -800, 0);

	}

	public void OnContinueButton()
	{
		pauseImage.SetActive(false);
		CommandManager.instance.SwitchStats(CommandStates.interactable);
        AudioManager.instance.PlaySFX(SFXAudio.SFX_BtnClick);
        dialogueFrame.transform.position += new Vector3(0, 800, 0);
	}
}
