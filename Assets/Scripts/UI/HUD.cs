using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HUD : UIScreen {

	public Text highScoreText;
	public Text comboText;
	public Text scoreText;
	public Text healthText;
	public Image healthImage;
	public GenericTimer gameStartTimer;

	private Camera mainCamera;

	private void Start ()
    {
		mainCamera = Camera.main;
		gameStartTimer.OnTimeOut.AddListener(CallOnGameStart);
        EventManager.instance.AddListener(EventConst.EVENT_CLICK, EffectOnGetCombo);
        EventManager.instance.AddListener(EventConst.EVENT_SCORE, EffectOnGetScore);
    }

	void Update ()
    {
		healthText.text = "Health: " + GameManager.instance.currentHealth.ToString();
        healthImage.fillAmount = (float)GameManager.instance.currentHealth / (float)PlayerModel.GetMaxHpData();
	}

    public override void OnScreenEnter()
    {
        base.OnScreenEnter();
        highScoreText.text = PlayerModel.GetHighScoreData(PlayerModel.currentSong, PlayerModel.difficulty).ToString();
        scoreText.text = "Score: 0";
        comboText.text = "1 x!";
    }

    public void CallOnGameStart()
	{
		GameManager.instance.gameStart = true;
		gameStartTimer.gameObject.SetActive(false);
	}

	public void OnPauseButton()
	{
		UIManager.instance.Push(typeof(PausePopup));
		GameManager.instance.GamePauseToggle();
	}

	public void EffectOnGetCombo(object[] data)
	{
        comboText.text = GameManager.instance.comboNum.ToString() + "x!";
        comboText.DOFade(1, 0.3f).onComplete = delegate
		{
			comboText.DOFade(0, 1f);
		};
		comboText.gameObject.transform.DOScale(2, 0.3f).onComplete = delegate
		{
			comboText.gameObject.transform.DOScale(1, 1f);
		};
	}

    public void EffectOnGetScore(object[] data)
    {
        int tmpModifier = (int)data[0];
        StartCoroutine(OnGetScore(tmpModifier));
    }

    private IEnumerator OnGetScore(int scoreModifier)
    {
        int s = scoreModifier;
        while(s > 0)
        {
            s -= 1;
            scoreText.text = "Score: " + (GameManager.instance.score - s).ToString();
            yield return null;
        }
    }

	public Vector3 GetUIWorldPos(GameObject UIObj)
	{
		Vector3 screenPos = mainCamera.WorldToScreenPoint(UIObj.transform.position);
		//mainCamera.WorldToScreenPoint(UIObj.transform.position);
		print(screenPos);
		Vector3 worldPos = mainCamera.ScreenToWorldPoint(screenPos);
		print(worldPos);
		return worldPos;
	}
}
