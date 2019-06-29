using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PausePopup : UIScreen {

	private RectTransform rectTransform;
    private RectTransform imgBg;
    private Button btn_Back;
    private Button btn_Restart;
    private Button btn_MainMenu;

    protected override void Init()
    {
        rectTransform = GetComponent<RectTransform>();
        imgBg = transform.Find("Img_BGBlock").GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.one;
        btn_Back = transform.Find("Img_BGBlock/Panel_ButtonList/Button_Back").GetComponent<Button>();
        btn_Restart = transform.Find("Img_BGBlock/Panel_ButtonList/Button_Restart").GetComponent<Button>();
        btn_MainMenu = transform.Find("Img_BGBlock/Panel_ButtonList/Button_MainMenu").GetComponent<Button>();
        btn_Back.onClick.AddListener(OnBackButton);
        btn_Restart.onClick.AddListener(OnRestartButton);
        btn_MainMenu.onClick.AddListener(OnMainMenuButton);
    }


	private void OnBackButton()
	{
		UIManager.instance.Pop();
		GameManager.instance.GamePauseToggle();
        AudioManager.instance.PlaySFX(SFXAudio.SFX_BtnClick);
    }

	private void OnRestartButton()
	{
		UIManager.instance.Pop();
		GameManager.instance.GameStart();
        AudioManager.instance.PlaySFX(SFXAudio.SFX_BtnClick);
    }

	private void OnMainMenuButton()
	{
		UIManager.instance.Push(typeof(MainMenu));
        AudioManager.instance.PlaySFX(SFXAudio.SFX_BtnClick);
    }

	public override void OnScreenEnter()
	{
		base.OnScreenEnter();
        btn_Back.interactable = false;
        imgBg.DOScale(Vector3.one, 1.0f).SetEase(Ease.OutSine).onComplete = () =>
        {
            btn_Back.interactable = true;
        };
	}
	public override void OnScreenQuit()
	{
		imgBg.DOScale(Vector3.zero, 1.0f).SetEase(Ease.OutSine).onComplete = delegate
		{
			gameObject.SetActive(false);
		};
	}


}
