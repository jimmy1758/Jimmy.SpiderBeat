using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager> {

	public GameObject go_Music;
	public GameObject go_UI;
	private AudioSourceInfo asi_BGM;
	private AudioSourceInfo asi_UI;

	private void Start()
	{
		asi_BGM = new AudioSourceInfo(go_Music);
		asi_UI = new AudioSourceInfo(go_UI);
		asi_BGM.musicEnd = delegate
		{
			GameManager.instance.GameEnd();
		};
	}

	public void PlayMusic(MusicAudio music)
	{
		AudioClip musicToPlay = Resources.Load<AudioClip>("Audio/Music/" + music.ToString());
		asi_BGM.Clip = musicToPlay;
		asi_BGM.PlayFromStart();
	}

	public void PlaySFX(SFXAudio SFXName)
	{
		AudioClip SFXToPlay = Resources.Load<AudioClip>("Audio/SFX/" + SFXName.ToString());
		asi_UI.Clip = SFXToPlay;
		asi_UI.Play();
	}

    public void StopMusic()
    {
        asi_BGM.Stop();
    }

    private void Update()
    {
        asi_BGM.Update();
        asi_UI.Update();
    }

    public void TogglePauseMusic()
	{
		if (asi_BGM.audioState == AudioState.IsPlaying)
		{
			asi_BGM.Pause();
		}
		else if(asi_BGM.audioState == AudioState.Pause)
		{
			asi_BGM.Play();
		}
	}

}

public enum MusicAudio
{
	
	Tutorial,
    ThisIsWhatYouCameFor,
    Maps,
    Warriors
}

public enum SFXAudio
{
    SFX_BtnClick,
    SFX_GameEnd,
    SFX_BuyItem,
    SFX_ClickRight,
    SFX_Miss,
    SFX_Dialogue,
    SFX_CoinNotEnough
}
