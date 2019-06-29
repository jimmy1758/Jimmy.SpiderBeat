using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TutorialManager : GameManager
{
    public static TutorialManager _instance;
    [System.Serializable]
    public struct TutorialBeats
    {
        public GameObject insectPrefab;
        public int targetID;
    }

    public TutorialBeats[] tutorialBeats;
    public Text txt_dialogue;
    private int beatIndex = 0;

    public GameObject endNotice;
    public Text txt_Score;
    public Text txt_Combo;
    public Text txt_Hp;
    public Image img_Hp;
    public Image img_Combo;

    protected override void Awake()
    {
        PlayerPrefs.DeleteAll();
        base.Awake();
        if (_instance == null)
        {
            _instance = this;
        }
    }

    protected override void Start()
    {
        base.Start();
        TutorialStart();
        BindEvent();
    }

    public IEnumerator CreateTutorialBeat()
    {
        GameObject go = PoolManager.instance.Spawn(tutorialBeats[beatIndex].insectPrefab.name, startPos.position);
        go.GetComponent<SpriteRenderer>().flipX = tutorialBeats[beatIndex].targetID > 3;
        Tweener tweener = go.transform.DOMove(targetsObj[tutorialBeats[beatIndex].targetID].transform.position, 2.0f);
        tweener.SetLoops(-1, LoopType.Incremental);
        tweener.SetEase(Ease.Linear);
        beatIndex++;
        yield return new WaitUntil(()=> !go.activeSelf);
        tweener.Kill();
    }

    private void TutorialStart()
    {
        PoolManager.instance.Init();
        GameEffectManager.instance.Init();
        currentHealth = PlayerModel.GetMaxHpData();
    }

    private void BindEvent()
    {
        EventManager.instance.AddListener(EventConst.EVENT_ENTER, OnTargetEnter);
        EventManager.instance.AddListener(EventConst.EVENT_MISS, OnMissBeat);
        EventManager.instance.AddListener(EventConst.EVENT_CLICK, OnClickRightBeat);
    }

    public void OnMissBeat(object[] data)
    {
        Time.timeScale = 1.0f;
        txt_dialogue.text = "You missed!\n You need to click the note on time when it reaches the corner!";
        if(beatIndex > 1)
        {
            txt_Hp.text = currentHealth.ToString();
            img_Hp.fillAmount = (float)currentHealth / (float)PlayerModel.GetMaxHpData();
        }

        beatIndex--;
        CommandManager.instance.RetralCmd(2);
    }

    public void OnClickRightBeat(object[] data)
    {
        CommandManager.instance.SwitchStats(CommandStates.interactable);
        if (beatIndex > 2)
            txt_Score.text = score.ToString();
        if(beatIndex > 3)
            OnGetCombo();
        Time.timeScale = 1.0f;
        txt_dialogue.text = "Good job, go ahead~";
    }

    public void OnTargetEnter(object[] data)
    {
        Time.timeScale = 0.3f;
        txt_dialogue.text = "Seize the moment, just now!";
    }

    private void OnGetCombo()
    {
        txt_Combo.text = comboNum.ToString() + "x!";
        img_Combo.DOFade(1, 0.3f);
        txt_Combo.DOFade(1, 0.3f).onComplete = delegate
        {
            txt_Combo.DOFade(0, 1f);
            img_Combo.DOFade(0, 0.3f);
        };
        txt_Combo.gameObject.transform.DOScale(2, 0.3f).onComplete = delegate
        {
            txt_Combo.gameObject.transform.DOScale(1, 1f);
        };
    }

    public void OnOKButton()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
