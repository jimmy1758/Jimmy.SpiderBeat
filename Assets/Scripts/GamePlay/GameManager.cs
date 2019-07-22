using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager> {

	[HideInInspector]
	public string currentSong;
	[HideInInspector]
	public string difficulty;

	public BaseUnit[] BugsPrefab;
	public GameObject[] targetsObj;
	public Transform startPos;
	public bool gamePaused;
	protected int beatsCount = 0;
	public float timeOfMusic = 0;

	[Header("PlayerInfo")]
	public int comboNum;
	public int score;
	public int currentHealth;


	public int perfectScore = 3;
	public int greatScore = 2;
	public int goodScore = 1;

	public Text tutorialText;

    internal bool gameStart;

	[SerializeField]
	private GameObject comboIcon;

    protected virtual void Start()
    {
        EventManager.instance.AddListener(EventConst.EVENT_CLICK, ComboNumIncrease);
        EventManager.instance.AddListener(EventConst.EVENT_CLICK, AddScore);
        EventManager.instance.AddListener(EventConst.EVENT_MISS, OnHurt);
        EventManager.instance.AddListener(EventConst.EVENT_MISS, ComboReset);
    }

    protected virtual void Update ()
    {
        if (gameStart && !gamePaused)
        {
            GenerateBeats();
        }
	}

	public void GamePauseToggle()
	{
		gamePaused = !gamePaused;
		AudioManager.instance.TogglePauseMusic();
	}

	public void GameStart()
	{
        currentSong = PlayerModel.currentSong;
        difficulty = PlayerModel.difficulty;
		PlayerInfoInit();
		PoolManager.instance.Init();
		GameEffectManager.instance.Init();
		LevelEditor.instance.beats.ParseXML(currentSong, difficulty);
		AudioManager.instance.PlayMusic((MusicAudio)(System.Enum.Parse(typeof(MusicAudio), currentSong)));
		UIManager.instance.hUD.gameStartTimer.StartTimer();
	}

	public void GameEnd()
	{
		GamePauseToggle();
        AudioManager.instance.PlaySFX(SFXAudio.SFX_GameEnd);
		UIManager.instance.Push(typeof(GameEnd));
	}

	public void PauseGame(bool condition)
	{
		gamePaused = condition;
		Time.timeScale = gamePaused == true ? 0 : 1;
	}

	public void PlayerInfoInit()
	{
		gameStart = false;
		gamePaused = false;
		timeOfMusic = 0;
		beatsCount = 0;
        currentHealth = PlayerModel.GetMaxHpData();
        comboNum = PlayerModel.GetInitialComboData();
		score = 0;
	}

	//根据leveleditor生成Unit节拍
	protected virtual void GenerateBeats()
	{
		timeOfMusic += Time.deltaTime;
		while (beatsCount < LevelEditor.instance.beats.beatsTimingList.Count && timeOfMusic > LevelEditor.instance.beats.spawnTimingList[beatsCount])
		{
			//GameObject unitClone = Instantiate(baseUnitPrefab, startPos.position, transform.rotation);
			GameObject unitClone = PoolManager.instance.Spawn(GetRandomUnit().name, startPos.position);
			int targetNum = LevelEditor.instance.beats.targetsNumList[beatsCount];
			unitClone.GetComponent<SpriteRenderer>().flipX = targetNum > 4;
			unitClone.GetComponent<BaseUnit>().dir = (targetsObj[targetNum].transform.position - startPos.position).normalized;
			unitClone.GetComponent<BaseUnit>().speed = LevelEditor.instance.beats.beatsSpeed[beatsCount];
			beatsCount++;
		}
	}


	#region InfoSet
	public void ComboNumIncrease(object[] data)
	{
		comboNum++;
		GameEffectManager.instance.AddWorldEffect("CFX_Combo", comboIcon.transform.position, 3.5f, 1);
	}

	public void ComboReset(object[] data)
	{
		comboNum = 0;
	}

	private void AddScore(object[] data)
	{
        string eva = (string)data[0];
        Vector3 effPos = (Vector3)data[1];
		int tempCombo = Mathf.Clamp(comboNum, 0, 5);
        int scoreModifier = 0;
		switch (eva)
		{
			case "Perfect":
                scoreModifier = perfectScore * tempCombo;
				GameEffectManager.instance.AddWorldEffect("CFX_Wooh", effPos + GetCirclePoint(5), 4, 1);
				break;
			case "Great":
				scoreModifier = greatScore * tempCombo;
				GameEffectManager.instance.AddWorldEffect("CFX_Slash", effPos + GetCirclePoint(5), 4, 1);
				break;
			case "Good":
				scoreModifier = goodScore * tempCombo;
				GameEffectManager.instance.AddWorldEffect("CFX_Poof", effPos + GetCirclePoint(5), 4, 1);
				break;
		}
        score += scoreModifier;
        EventManager.instance.Dispatch(EventConst.EVENT_SCORE, scoreModifier);
    }

	/// <summary>
	/// 玩家受到伤害损失damage的血量
	/// </summary>
	/// <param name="damage"> 损失血量 </param>
	public void OnHurt(object[] data)
	{
        int damage = (int)data[0];
		currentHealth -= damage;
		if(currentHealth <= 0)
		{
			GameEnd();
		}
	}
	#endregion


	public BaseUnit GetRandomUnit()
	{
		return BugsPrefab[Random.Range(0, BugsPrefab.Length)];
	}
	public Vector3 GetCirclePoint(int m_Radius)
	{
		//随机获取弧度
		float radin = (float)GetRandomValue(0, 2 * Mathf.PI);
		float x = m_Radius * Mathf.Cos(radin);
		float y = m_Radius * Mathf.Sin(radin);
		Vector3 endPoint = new Vector3(x, y, 0);
		return endPoint;
	}
	public float GetRandomValue(float min, float max)
	{
		//System.Random random = new System.Random(1000);
		float v = Random.Range(min, max);
		return v;
	}

}
