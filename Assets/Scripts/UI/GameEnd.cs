using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEnd : UIScreen {

	private Text playerScoreText;
	private Text highScoreText;
    private Button btn_Restart;
    private Button btn_MainMenu;

    protected override void Init()
    {
        base.Init();
        playerScoreText = transform.Find("Img_BackGroud/Panel_Text/Text_Score").GetComponent<Text>();
        highScoreText = transform.Find("Img_BackGroud/Text_HighScore").GetComponent<Text>();
        btn_Restart = transform.Find("Img_BackGroud/Panel_ButtonList/Button_Restart").GetComponent<Button>();
        btn_MainMenu = transform.Find("Img_BackGroud/Panel_ButtonList/Button_MainMenu").GetComponent<Button>();
        btn_MainMenu.onClick.AddListener(OnMainMenuButton);
        btn_Restart.onClick.AddListener(OnRestartButton);
    }

    public override void OnScreenEnter()
    {
        base.OnScreenEnter();
        int currentSocre = GameManager.instance.score;
        playerScoreText.text = currentSocre.ToString();
        PlayerModel.SaveHighScoreData(currentSocre);
        PlayerModel.SaveCoinsData(currentSocre/10);
        highScoreText.text = "HighScore: " + PlayerModel.GetHighScoreData().ToString();
        if(currentSocre > 1000)
        {
            PlayerModel.SaveUnlockStatsData(PlayerModel.currentSong, "Normal");
        }
        if(currentSocre > 1500)
        {
            PlayerModel.SaveUnlockStatsData(PlayerModel.currentSong, "Crazy");
        }
    }

    private void OnRestartButton()
	{
        AudioManager.instance.PlaySFX(SFXAudio.SFX_BtnClick);
        UIManager.instance.Pop();
		GameManager.instance.GameStart();
	}

	private void OnMainMenuButton()
	{
        AudioManager.instance.PlaySFX(SFXAudio.SFX_BtnClick);
        UIManager.instance.Push(typeof(MainMenu));
	}
}
